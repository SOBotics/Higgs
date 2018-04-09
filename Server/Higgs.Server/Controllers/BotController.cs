using System;
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
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(AquireTokenResponse), Description = "The authorization token")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult AquireToken([FromBody] AquireTokenRequest request)
        {
            var bot = _dbContext.Bots.Where(b => b.Id == request.BotId)
                .Select(b => new
                {
                    b.Id,
                    b.Secret,
                    AllowedScopes = b.BotScopes.Select(bs => bs.ScopeName).ToList()
                })
                .FirstOrDefault();

            
            if (bot == null)
                return BadRequest(new ErrorResponse("Bot with that id does not exist."));

            if (!BCrypt.Net.BCrypt.Verify(request.Secret, bot.Secret))
                return BadRequest(new ErrorResponse("Invalid secret provided."));

            var claims = (request.RequestedScopes?.Intersect(bot.AllowedScopes, StringComparer.OrdinalIgnoreCase) ?? bot.AllowedScopes)
                .Select(s => new Claim(s, string.Empty))
                .Concat(new[] {new Claim(BOT_ID_CLAIM, request.BotId.ToString())})
                .ToList();

            var signingKey = Convert.FromBase64String(_configuration["JwtSigningKey"]);
            var newToken = AuthenticationController.CreateJwtToken(claims, signingKey);
            return Json(new AquireTokenResponse { Token = newToken });
        }
        
        /// <summary>
        ///     Used by bots to register feedback types
        /// </summary>
        /// <returns>The access token</returns>
        [HttpPost("RegisterFeedbackTypes")]
        [Authorize(Scopes.BOT_SET_FEEDBACK_TYPES)]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult RegisterFeedbackTypes([FromBody] RegisterFeedbackTypesRequest request)
        {
            var botId = GetBotId();
            if (!botId.HasValue)
                return BadRequest("Invalid or missing botId in claim");
            
            var existingFeedbacks = _dbContext.Feedbacks.Where(f => f.Id == 1 && request.FeedbackTypes.Select(ft => ft.Name).Contains(f.Name)).ToDictionary(f => f.Name, f => f);
            foreach (var feedbackType in request.FeedbackTypes)
            {
                DbFeedback dbFeedback;
                if (existingFeedbacks.ContainsKey(feedbackType.Name))
                    dbFeedback = existingFeedbacks[feedbackType.Name];
                else
                {
                    dbFeedback = new DbFeedback
                    {
                        BotId = botId.Value,
                        Name = feedbackType.Name
                    };
                    _dbContext.Feedbacks.Add(dbFeedback);
                }

                dbFeedback.Colour = feedbackType.Colour;
                dbFeedback.Icon = feedbackType.Icon;
                dbFeedback.IsActionable = feedbackType.IsActionable;
                dbFeedback.RequiredActions = feedbackType.RequiredActions;
            }

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("RegisterUserFeedbackByContent")]
        [Authorize(Scopes.BOT_SEND_FEEDBACK)]
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
        [Authorize(Scopes.BOT_SEND_FEEDBACK)]
        public IActionResult RegisterUserFeedback([FromBody] RegisterUserFeedbackRequest request)
        {
            var botId = GetBotId();
            if (!botId.HasValue)
                return BadRequest("Invalid or missing botId in claim");

            var report = _dbContext.Reports.FirstOrDefault(r => r.Id == request.ReportId);
            if (report?.BotId != botId)
                return BadRequest("Bot is not authorized to submit feedback to this report");

            if (string.IsNullOrWhiteSpace(request.Feedback))
                return BadRequest("Feedback type must be provided");

            var allowedFeedback = _dbContext.ReportAllowedFeedbacks
                .Include(raf => raf.Feedback)
                .FirstOrDefault(raf => request.Feedback.Equals(raf.Feedback.Name, StringComparison.OrdinalIgnoreCase));

            if (allowedFeedback == null)
                return BadRequest("Feedback not allowed for report");

            var user = _dbContext.GetOrCreateUser(request.UserId);
            
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
        [Authorize(Scopes.BOT_REGISTER_POST)]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult RegisterPost([FromBody] RegisterPostRequest request)
        {
            var botId = GetBotId();
            if (!botId.HasValue)
                return BadRequest("Invalid or missing botId in claim");

            var report = new DbReport
            {
                AuthorName = request.AuthorName,
                AuthorReputation = request.AuthorReputation,
                BotId = botId.Value,
                Title = request.Title,
                
                ContentCreationDate = request.ContentCreationDate,
                ContentUrl = request.ContentUrl,
                DetectedDate = request.DetectedDate,
                DetectionScore = request.DetectionScore,
            };

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

            var feedbackTypes = _dbContext.Feedbacks.Where(f => f.BotId == botId && request.AllowedFeedback.Contains(f.Name)).ToDictionary(f => f.Name, f => f.Id);
            foreach (var allowedFeedback in request.AllowedFeedback ?? Enumerable.Empty<string>())
            {
                if (feedbackTypes.ContainsKey(allowedFeedback))
                {
                    _dbContext.ReportAllowedFeedbacks.Add(new DbReportAllowedFeedback
                    {
                        FeedbackId = feedbackTypes[allowedFeedback],
                        Report = report
                    });
                }
            }
            var reasons = _dbContext.Reasons.Where(f => f.BotId == botId && request.Reasons.Select(r => r.ReasonName).Contains(f.Name)).ToDictionary(f => f.Name, f => f);
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
                        BotId = botId.Value
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