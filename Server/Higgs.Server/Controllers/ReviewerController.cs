using System.Linq;
using System.Security.Cryptography;
using Higgs.Server.Data;
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

        [HttpGet("GetReport")]
        public ReportResponse GetReport(int id)
        {
            var query = _dbContext.Reports.Where(r => r.Id == id)
                .Select(r => new ReportResponse
                {
                    Title = r.Title,
                    ContentUrl = r.ContentUrl,
                    ContentSite = r.ContentSite,
                    ContentType = r.ContentType,
                    ContentId = r.ContentId,
                    DetectionScore = r.DetectionScore,
                    ContentFragments = r.ContentFragments.Select(contentFragment => new ReportContentFragmentResponse
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
                    Reasons = r.ReportReasons.Select(reportReason => new ReportReasonResponse
                    {
                        ReasonId = reportReason.ReasonId,
                        Name = reportReason.Reason.Name,
                        Confidence = reportReason.Confidence,
                        Seen = reportReason.Reason.ReportReasons.GroupBy(rr => rr.ReportId).Count()
                    }).ToList(),

                    AllowedFeedback = r.ReportAllowedFeedback.Select(ra => new ReportAllowedFeedbackResponse
                    {
                        Id = ra.Feedback.Id,
                        Name = ra.Feedback.Name,
                        Colour = ra.Feedback.Colour
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
        [HttpPost("SendFeedback")]
        [Authorize(Scopes.REVIEWER_SEND_FEEDBACK)]
        public IActionResult SendFeedback()
        {
            return Ok(0);
        }
    }
}