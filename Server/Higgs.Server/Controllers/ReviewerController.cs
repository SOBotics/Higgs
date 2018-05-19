using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.Reviewer;
using Higgs.Server.Models.Responses.Reviewer;
using Higgs.Server.Models.Shared;
using Higgs.Server.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        
        [HttpGet("PendingReviews")]
        public PagingResponse<int> PendingReviews(string dashboardName, PagingRequest pagingRequest)
        {
            var query = _dbContext.Reports.Where(r => r.RequiresReview);
            if (!string.IsNullOrWhiteSpace(dashboardName))
                query = query.Where(r => r.Bot.DashboardName.ToLower() == dashboardName.ToLower());

            var result = query.Select(r => r.Id).Page(pagingRequest);
            return result;
        }

        [HttpGet("Reports")]
        public PagingResponse<ReviewerReportsResponse> Reports(PagingRequest request)
        {
            // We're writing two queries, otherwise EFCore hits the N+1 problem
            var pagedReportData = _dbContext.Reports
                .Select(r => new
                {
                    r.Id,
                    r.Title,
                    r.Bot.DashboardName,
                    r.DetectionScore,
                }).OrderByDescending(r => r.Id)
                .Page(request);

            var reports = pagedReportData.Data;

            var reportIds = reports.Select(r => r.Id).ToList();
            var feedbacks = _dbContext.ReportFeedbacks.Where(rf => reportIds.Contains(rf.ReportId))
                .Select(feedback => new
                {
                    feedback.ReportId,
                    feedback.Feedback.Icon,
                    feedback.Feedback.Colour,
                    FeedbackName = feedback.Feedback.Name,
                    UserName = feedback.User.Name
                }).GroupBy(r => r.ReportId)
                .ToDictionary(r => r.Key, r => r.ToList());

            var result =
                new PagingResponse<ReviewerReportsResponse>
                {
                    PageNumber = pagedReportData.PageNumber,
                    PageSize = pagedReportData.PageSize,
                    TotalPages = pagedReportData.TotalPages,
                    Data = reports
                        .Select(r => new ReviewerReportsResponse
                        {
                            Id = r.Id,
                            Title = r.Title,
                            DashboardName = r.DashboardName,
                            DetectionScore = r.DetectionScore,
                            Feedback = feedbacks.ContainsKey(r.Id)
                                ? feedbacks[r.Id].Select(feedback => new ReviewerReportFeedbackResponse
                                {
                                    Icon = feedback.Icon,
                                    Colour = feedback.Colour,
                                    FeedbackName = feedback.FeedbackName,
                                    UserName = feedback.UserName
                                }).ToList()
                                : new List<ReviewerReportFeedbackResponse>()
                        })
                        .OrderByDescending(r => r.Id)
                        .ToList()
                };
            
            return result;
        }

        [HttpGet("Report")]
        public ReviewerReportResponse Report(int id)
        {
            var report = _dbContext.Reports.Where(r => r.Id == id)
                .Select(r => new ReviewerReportResponse
                {
                    Id = r.Id,
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
                        Seen = reportReason.Reason.ReportReasons.Where(rr => rr.Tripped && rr.ReportId != id).Select(rr => rr.ReportId).Distinct().Count()
                    }).ToList(),

                    AllowedFeedback = r.AllowedFeedback.Where(af => af.Feedback.IsEnabled).Select(af => new ReviewerReportAllowedFeedbackResponse
                    {
                        Id = af.Feedback.Id,
                        Name = af.Feedback.Name,
                        Colour = af.Feedback.Colour
                    }).ToList(),

                    Feedback = r.Feedbacks.Where(f => f.InvalidatedDate == null).Select(feedback => new ReviewerReportFeedbackResponse
                    {
                        Id = feedback.Id,
                        UserId = feedback.UserId,
                        Icon = feedback.Feedback.Icon,
                        Colour = feedback.Feedback.Colour,
                        FeedbackName = feedback.Feedback.Name,
                        UserName = feedback.User.Name
                    }).ToList()
                }).FirstOrDefault();

            if (report == null)
                return null;

            if (report.DetectedDate.HasValue)
                report.DetectedDate = DateTime.SpecifyKind(report.DetectedDate.Value, DateTimeKind.Utc);

            if (report.ContentCreationDate.HasValue)
                report.ContentCreationDate = DateTime.SpecifyKind(report.ContentCreationDate.Value, DateTimeKind.Utc);

            return report;
        }

        [HttpGet("Check")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<ReviewerCheckResponse>))]
        public List<ReviewerCheckResponse> Check(string contentUrl)
        {
            var results = _dbContext.Reports.Where(r => r.ContentUrl == contentUrl)
                .Select(r => new ReviewerCheckResponse
                {
                    Dashboard = r.Bot.DashboardName,
                    Bot = r.Bot.Name,
                    ReportId = r.Id
                }).ToList();
            return results;
        }

        [HttpGet("NextReview")]
        [Authorize(Scopes.SCOPE_REVIEWER)]
        public ReviewerReportResponse NextReview(int? lastId)
        {
            var userId = User.GetUserId();
            if (!userId.HasValue)
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            var reportQuery = _dbContext.Reports.Where(r => r.RequiresReview);
            if (lastId.HasValue)
                reportQuery = reportQuery.Where(r => r.Id > lastId);

            var nextReportId = reportQuery
                .GroupJoin(_dbContext.ReportFeedbacks.Where(rf => rf.UserId == userId), r => r.Id, rf => rf.ReportId,
                    (report, group) => new
                    {
                        ReportId = report.Id,
                        Group = group
                    })
                .SelectMany(gj => gj.Group.DefaultIfEmpty(), (report, feedback) => new { ReportId = (int?)report.ReportId, FeedbackId = (int?)feedback.Id})
                .Where(a => a.FeedbackId == null)
                .Select(a => a.ReportId)
                .FirstOrDefault();

            return nextReportId.HasValue ? Report(nextReportId.Value) : null;
        }


        /// <summary>
        ///     Lists all pending review
        /// </summary>
        [HttpPost("SendFeedback")]
        [Authorize(Scopes.SCOPE_REVIEWER)]
        public IActionResult SendFeedback([FromBody] SendFeedbackRequest request)
        {
            var allowedFeedbacks = _dbContext.ReportAllowedFeedbacks.Where(r => r.ReportId == request.ReportId && r.Feedback.IsEnabled).Select(f => f.FeedbackId).ToList();

            var userId = User.GetUserId();
            if (!userId.HasValue)
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            if (!allowedFeedbacks.Contains(request.FeedbackId))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Invalid feedback id");

            var existingFeedback = _dbContext.ReportFeedbacks.FirstOrDefault(rf => rf.UserId == userId && rf.ReportId == request.ReportId && rf.InvalidatedDate == null);
            if (existingFeedback != null) 
            {
                // No change
                if (existingFeedback.FeedbackId == request.FeedbackId)
                    return Ok();
                    
                existingFeedback.InvalidatedByUserId = userId.Value;
                existingFeedback.InvalidatedDate = DateTime.UtcNow;
                existingFeedback.InvalidationReason = "Feedback changed";
            }
                
            _dbContext.ReportFeedbacks.Add(new DbReportFeedback
            {
                FeedbackId = request.FeedbackId,
                ReportId = request.ReportId,
                UserId = userId.Value
            });
                
            _dbContext.SaveChanges();

            _dbContext.ProcessReport(request.ReportId);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("ClearFeedback")]
        [Authorize(Scopes.SCOPE_REVIEWER)]
        public IActionResult ClearFeedback([FromBody] ClearFeedbackRequest request)
        {
            var userId = User.GetUserId();
            if (!userId.HasValue)
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            var existingFeedback = _dbContext.ReportFeedbacks.FirstOrDefault(rf => rf.Id == request.FeedbackId && rf.InvalidatedDate == null);

            if (existingFeedback == null) 
                return Ok();
            if (existingFeedback.UserId != userId && !User.HasClaim(Scopes.SCOPE_ROOM_OWNER))
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            existingFeedback.InvalidatedDate = DateTime.UtcNow;
            existingFeedback.InvalidatedByUserId = userId.Value;
            existingFeedback.InvalidationReason = "Feedback cleared";
            _dbContext.SaveChanges();

            _dbContext.ProcessReport(existingFeedback.ReportId);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}