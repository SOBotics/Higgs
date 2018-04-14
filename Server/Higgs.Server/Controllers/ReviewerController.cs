using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Responses;
using Higgs.Server.Models.Responses.Reviewer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public partial class ReviewerController : Controller
    {
        private readonly HiggsDbContext _dbContext;

        public ReviewerController(HiggsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <summary>
        ///     Lists all pending reviews
        /// </summary>
        [HttpGet("PendingReviews")]
        public IActionResult PendingReviews()
        {
            return Ok(0);
        }

        [HttpGet("Reports")]
        public List<ReviewerReportsResponse> Reports()
        {
            var query = _dbContext.Reports
                .Select(r => new ReviewerReportsResponse
                {
                    Id = r.Id,
                    Title = r.Title,
                })
                .OrderByDescending(r => r.Id)
                .Take(100);
            return query.ToList();
        }

        [HttpGet("Report")]
        public ReviewerReportResponse Report(int id)
        {
            var query = _dbContext.Reports.Where(r => r.Id == id)
                .Select(r => new ReviewerReportResponse
                {
                    Title = r.Title,
                    BotLogo = r.Bot.LogoUrl,
                    BotName = r.Bot.Name,
                    DashboardName = r.Bot.DashboardName,
                    FavIcon = r.Bot.FavIcon,
                    TabTitle = r.Bot.TabTitle,
                    BotHomePage = r.Bot.Homepage,
                    ContentUrl = r.ContentUrl,
                    ContentSite = r.ContentSite,
                    ContentType = r.ContentType,
                    ContentId = r.ContentId,
                    DetectionScore = r.DetectionScore,
                    ContentFragments = r.ContentFragments.Select(contentFragment =>
                        new ReviewerReportContentFragmentResponse
                        {
                            Id = contentFragment.Id,
                            Name = contentFragment.Name,
                            Content = contentFragment.Content,
                            Order = contentFragment.Order
                        }).ToList(),
                    AuthorName = r.AuthorName,
                    AuthorReputation = r.AuthorReputation,
                    ContentCreationDate = r.ContentCreationDate,
                    DetectedDate = r.DetectedDate,
                    Reasons = r.Reasons.Select(reportReason => new ReviewerReportReasonResponse
                    {
                        ReasonId = reportReason.ReasonId,
                        Name = reportReason.Reason.Name,
                        Confidence = reportReason.Confidence,
                        Tripped = reportReason.Tripped,
                        Seen = reportReason.Reason.ReportReasons.Select(rr => rr.ReportId).Distinct().Count()
                    }).ToList(),

                    AllowedFeedback = r.AllowedFeedback.Where(af => af.Feedback.IsEnabled).Select(af => new ReviewerReportAllowedFeedbackResponse
                    {
                        Id = af.Feedback.Id,
                        Name = af.Feedback.Name,
                        Colour = af.Feedback.Colour
                    }).ToList(),

                    Feedback = r.Feedbacks.Select(feedback => new ReviewerReportFeedbackResponse
                    {
                        Icon = feedback.Feedback.Icon,
                        Colour = feedback.Feedback.Colour,
                        FeedbackName = feedback.Feedback.Name,
                        UserName = feedback.User.Name
                    }).ToList()
                });
            return query.FirstOrDefault();
        }

        /// <summary>
        ///     Lists all reviews
        /// </summary>
        [HttpGet("AllReviews")]
        public IActionResult AllReviews()
        {
            return Ok(0);
        }

        /// <summary>
        ///     Lists all pending review
        /// </summary>
        [HttpPost("feedback/sendFeedback")]
        [Authorize(Scopes.REVIEWER_SEND_FEEDBACK)]
        public IActionResult SendFeedback(int reportId, int id)
        {
            var allowedFeedbacks = _dbContext.ReportAllowedFeedbacks.Where(r => r.ReportId == reportId && r.Feedback.IsEnabled).Select(f => f.FeedbackId).ToList();

            var userIdStr = User.Claims.Where(c => c.Type == AuthenticationController.ACCOUNT_ID_CLAIM).Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(userIdStr) && int.TryParse(userIdStr, out var userId))
            {
                if (allowedFeedbacks.Contains(id))
                {
                    var existingFeedback = _dbContext.ReportFeedbacks.FirstOrDefault(rf => rf.UserId == userId && rf.ReportId == reportId);
                    if (existingFeedback == null)
                    {
                        _dbContext.ReportFeedbacks.Add(new DbReportFeedback
                        {
                            FeedbackId = id,
                            ReportId = reportId,
                            UserId = userId
                        });
                    }
                    else
                    {
                        existingFeedback.FeedbackId = id;
                    }

                    _dbContext.SaveChanges();
                    return Ok(0);
                }
            }

            return BadRequest(new ErrorResponse("Invalid feedback id"));
        }
    }
}