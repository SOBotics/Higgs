﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.Admin;
using Higgs.Server.Models.Requests.Bot;
using Higgs.Server.Models.Responses;
using Higgs.Server.Models.Responses.Bot;
using Higgs.Server.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public class BotController : Controller
    {
        private readonly HiggsDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private const string BOT_ID_CLAIM = "botId";

        public BotController(HiggsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        
        [HttpPost("AquireToken")]
        [ProducesResponseType(typeof(AquireTokenResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public IActionResult AquireToken([FromBody] AquireTokenRequest request)
        {
            var bot = _dbContext.Dashboards.Where(b => b.Id == request.DashboardId)
                .Select(b => new
                {
                    b.Id,
                    b.Secret,
                    AllowedScopes = b.Scopes.Select(bs => bs.ScopeName).ToList()
                })
                .FirstOrDefault();

            
            if (bot == null)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Dashboard with that id does not exist.");

            if (!BCrypt.Net.BCrypt.Verify(request.Secret, bot.Secret))
                throw new HttpStatusException(HttpStatusCode.Unauthorized, "Invalid secret provided.");

            var claims = (request.RequestedScopes?.Intersect(bot.AllowedScopes, StringComparer.OrdinalIgnoreCase) ?? bot.AllowedScopes)
                .Select(s => new Claim(s, string.Empty))
                .Concat(new[] {new Claim(BOT_ID_CLAIM, request.DashboardId.ToString())})
                .ToList();

            var signingKey = Convert.FromBase64String(_configuration["JwtSigningKey"]);
            var newToken = AuthenticationController.CreateJwtToken(claims, signingKey);
            return Json(new AquireTokenResponse { Token = newToken });
        }
    
        [HttpPost("RegisterUserFeedbackByContent")]
        [Authorize(Scopes.SCOPE_BOT)]
        public IActionResult RegisterUserFeedbackByContent([FromBody] RegisterUserFeedbackByContentRequest request)
        {
            var matchedReport = _dbContext.Reports.FirstOrDefault(r => r.ContentUrl == request.ContentUrl);
            if (matchedReport == null)
                return BadRequest("No report exists with that content URL");

            return RegisterUserFeedback(new RegisterUserFeedbackRequest
            {
                Feedback = request.Feedback,
                ReportId = matchedReport.Id,
                UserId = request.UserId
            });
        }

        [HttpPost("RegisterUserFeedback")]
        [Authorize(Scopes.SCOPE_BOT)]
        public OkResult RegisterUserFeedback([FromBody] RegisterUserFeedbackRequest request)
        {
            var botId = GetBotId();
            if (!botId.HasValue)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Invalid or missing botId in claim");

            var report = _dbContext.Reports.FirstOrDefault(r => r.Id == request.ReportId);
            if (report?.DashboardId != botId)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Bot is not authorized to submit feedback to this report");

            if (string.IsNullOrWhiteSpace(request.Feedback))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Feedback type must be provided");

            var allowedFeedback = _dbContext.ReportAllowedFeedbacks
                .Include(raf => raf.Feedback)
                .FirstOrDefault(raf => request.Feedback.Equals(raf.Feedback.Name, StringComparison.OrdinalIgnoreCase));

            if (allowedFeedback == null)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Feedback not allowed for report");

            var user = _dbContext.GetOrCreateUser(request.UserId);
            if (user.UserScopes.All(us => us.ScopeName != Scopes.SCOPE_REVIEWER))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "User is not authorized as a reviewer");
            
            var existingFeedback = _dbContext.ReportFeedbacks.FirstOrDefault(rf => rf.UserId == request.UserId && rf.ReportId == request.ReportId);
            if (existingFeedback == null)
            {
                _dbContext.ReportFeedbacks.Add(new DbReportFeedback
                {
                    FeedbackId = allowedFeedback.FeedbackId,
                    ReportId = request.ReportId,
                    User = user
                });
            }
            else
            {
                existingFeedback.FeedbackId = allowedFeedback.FeedbackId;
            }

            _dbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        ///     Used by bots to register a detected post
        /// </summary>
        /// <returns>
        ///     The id of the report created
        /// </returns>
        [HttpPost("RegisterPost")]
        [Authorize(Scopes.SCOPE_BOT)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult RegisterPost([FromBody] RegisterPostRequest request)
        {
            var botId = GetBotId();
            if (!botId.HasValue)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Invalid or missing botId in claim");
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Title is required");
            if (string.IsNullOrWhiteSpace(request.ContentUrl))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "ContentUrl is required");
            if (!request.ContentId.HasValue)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "ContentId is required");
            if (string.IsNullOrWhiteSpace(request.ContentSite))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "ContentSite is required");
            if (string.IsNullOrWhiteSpace(request.ContentType))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "ContentType is required");

            var report = new DbReport
            {
                AuthorName = request.AuthorName,
                AuthorReputation = request.AuthorReputation,
                DashboardId = botId.Value,
                Title = request.Title,

                ContentUrl = request.ContentUrl,
                ContentId = request.ContentId.Value,
                ContentSite = request.ContentSite,
                ContentType = request.ContentType,
                
                ContentCreationDate = request.ContentCreationDate?.ToUniversalTime(),
                DetectedDate = request.DetectedDate?.ToUniversalTime(),
                DetectionScore = request.DetectionScore,

                Feedbacks = new List<DbReportFeedback>(),
                ConflictExceptions = new List<DbConflictException>()
            };


            var dashboard = _dbContext.Dashboards.FirstOrDefault(d => d.Id == botId);
            if (dashboard == null)
                throw new HttpStatusException(HttpStatusCode.BadRequest, $"Dashboard with id {botId} does not exist");

            report.RequiredFeedback = request.RequiredFeedback ?? dashboard.RequiredFeedback;
            report.RequiredFeedbackConflicted = request.RequiredFeedbackConflicted ?? dashboard.RequiredFeedbackConflicted;

            var contentFragments = request.ContentFragments ?? Enumerable.Empty<RegisterPostContentFragment>();
            var fragments =
                string.IsNullOrWhiteSpace(request.Content)
                    ? contentFragments
                    : new[]
                    {
                        new RegisterPostContentFragment
                        {
                            Content = request.Content,
                            Name = "Original",
                            Order = 0,
                            RequiredScope = string.Empty
                        }
                    }.Concat(contentFragments.Select(cf => new RegisterPostContentFragment
                    {
                        Content = cf.Content,
                        Name = cf.Name,
                        Order = cf.Order + 1,
                        RequiredScope = cf.RequiredScope
                    }));

            foreach (var contentFragment in fragments)
            {
                var dbContentFragment = new DbContentFragment
                {
                    Order = contentFragment.Order,
                    Name = contentFragment.Name,
                    Content = contentFragment.Content,
                    RequiredScope = contentFragment.RequiredScope,
                    Report = report
                };
                
                _dbContext.ContentFragments.Add(dbContentFragment);
            }
            _dbContext.Reports.Add(report);

            if (request.AllowedFeedback?.Any() ?? false)
            {
                var feedbackTypes = _dbContext.Feedbacks.Where(f => f.DashboardId == botId && request.AllowedFeedback.Contains(f.Name)).ToDictionary(f => f.Name, f => f.Id);
                foreach (var allowedFeedback in request.AllowedFeedback)
                {
                    if (feedbackTypes.ContainsKey(allowedFeedback))
                    {
                        _dbContext.ReportAllowedFeedbacks.Add(new DbReportAllowedFeedback
                        {
                            FeedbackId = feedbackTypes[allowedFeedback],
                            Report = report
                        });
                    }
                    else
                    {
                        throw new HttpStatusException(HttpStatusCode.BadRequest, $"Feedback '{allowedFeedback}' not registered for bot");
                    }
                }
            }

            if (request.Reasons?.Any() ?? false)
            {
                var reasons = _dbContext.Reasons.Where(f => f.DashboardId == botId).ToDictionary(f => f.Name, f => f);
                foreach (var reason in request.Reasons ?? Enumerable.Empty<RegisterPostReason>())
                {
                    DbReason dbReason;

                    if (reasons.ContainsKey(reason.ReasonName))
                    {
                        dbReason = reasons[reason.ReasonName];
                    }
                    else
                    {
                        dbReason = new DbReason
                        {
                            Name = reason.ReasonName,
                            DashboardId = botId.Value
                        };
                        _dbContext.Reasons.Add(dbReason);
                    }

                    _dbContext.ReportReasons.Add(new DbReportReason
                    {
                        Confidence = reason.Confidence,
                        Tripped = reason.Tripped ?? false,
                        Reason = dbReason,
                        Report = report
                    });
                }

            }

            foreach (var attribute in request.Attributes ?? Enumerable.Empty<RegisterPostAttribute>())
            {
                _dbContext.ReportAttributes.Add(new DbReportAttribute
                {
                    Name = attribute.Key,
                    Value = attribute.Value,
                    Report = report
                });
            }
            
            _dbContext.SaveChanges();

            _dbContext.ProcessReport(report.Id);

            _dbContext.SaveChanges();

            return Json(report.Id);
        }

        private int? GetBotId()
        {
            var botIdClaim = User.Claims.Where(f => f.Type == BOT_ID_CLAIM).Select(f => f.Value).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(botIdClaim) || !int.TryParse(botIdClaim, out var botId))
                return null;
            return botId;
        }
    }
}