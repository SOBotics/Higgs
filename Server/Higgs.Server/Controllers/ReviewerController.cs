using System.Linq;
using System.Security.Cryptography;
using Higgs.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public class ReviewerController : Controller
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
        public IActionResult GetReport(int id)
        {
            var query = _dbContext.Reports.Where(r => r.Id == id)
                .Select(r => new
                {
                    r.Title,
                    r.ContentUrl,
                    r.ContentSite,
                    r.ContentType,
                    r.ContentId,
                    r.DetectionScore,

                    ContentFragments = r.ContentFragments.Select(contentFragment => new
                    {
                        contentFragment.Name,
                        contentFragment.Content,
                        contentFragment.Order,
                    }).ToList(),

                    r.AuthorName,
                    r.AuthorReputation,

                    r.ContentCreationDate,
                    r.DetectedDate,

                    Reasons = r.ReportReasons.Select(reportReason => new
                    {
                        reportReason.ReasonId,
                        reportReason.Confidence,
                        reportReason.Reason.Name,
                        Seen = reportReason.Reason.ReportReasons.GroupBy(rr => rr.ReportId).Count()
                    }).ToList(),

                    AllowedFeedback = r.ReportAllowedFeedback.Select(ra => new
                    {
                        ra.Feedback.Id,
                        ra.Feedback.Name,
                        ra.Feedback.Colour
                    }).ToList()
                });
            return Json(query.FirstOrDefault());
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