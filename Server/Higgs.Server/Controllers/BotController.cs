using System;
using System.Linq;
using System.Net;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.Admin;
using Higgs.Server.Models.Requests.Bot;
using Higgs.Server.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public class BotController : Controller
    {
        private readonly HiggsDbContext _dbContext;

        public BotController(HiggsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Used by bots to aquire an access token. 
        /// </summary>
        /// <param name="botId">The ID of the bot</param>
        /// <param name="payload">
        ///     A JWT token signed with the related key registered when creating a bot.
        ///     Expiration time must be no more than 5 minutes after issueing time.
        ///     Only scopes requested (which the bot is authorized for) will be included in the response token.
        ///     To request scopes, a claim named 'scopes' must be added, whose value is an array of scope names
        ///     For example
        ///     {
        ///         ... (iat, exp, etc)
        ///         "scopes": [ "bot:setFeedbackTypes", "bot:registerPost" ]
        ///     }
        /// </param>
        /// <returns>The access token, encryped with the bots public key</returns>
        [HttpPost("AquireToken")]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        [SwaggerResponse((int) HttpStatusCode.Unauthorized, Description = "Signature failed")]
        public IActionResult AquireToken(int botId, string payload)
        {
            return Ok(string.Empty);
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
            var botId = 1;
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
                        BotId = 1,
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
            var botId = 1;
            var report = new DbReport
            {
                AuthorName = request.AuthorName,
                AuthorReputation = request.AuthorReputation,
                BotId = botId,
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
                        BotId = botId
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
    }
}