using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Responses.Analytics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public class AnalyticsController : Controller
    {
        private readonly HiggsDbContext _dbContext;

        public AnalyticsController(HiggsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("ReportsOverTime")]
        public List<ReportsOverTimeResponse> ReportsOverTime(string dashboardName = null)
        {
            var dateFrom = DateTime.UtcNow.AddMonths(-3);
            var dateTo = DateTime.UtcNow.Date;
            
            var reports = _dbContext.Reports.Where(r => r.DetectedDate >= dateFrom && r.DetectedDate < dateTo);
            if (!string.IsNullOrWhiteSpace(dashboardName)) 
            {
                reports = reports.Where(r => r.Dashboard.DashboardName == dashboardName);
            }
            var results = reports
                .GroupBy(r => new { r.DashboardId, r.Dashboard.DashboardName, r.DetectedDate.Value.Date })
                .Select(g => new ReportsOverTimeResponse
                {
                    DashboardId = g.Key.DashboardId,
                    DashboardName = g.Key.DashboardName,
                    Date = g.Key.Date,
                    Count = g.Count()
                }).ToList();

            return results;
        }

        [HttpGet("ReportsTotal")]
        public List<ReportsTotalResponse> ReportsTotal()
        {
            return _dbContext.Dashboards
                .Select(b => new ReportsTotalResponse
                {
                    DashboardId = b.Id,
                    DashboardName = b.DashboardName,
                    Count = b.Reports.Count()
                })
                .Where(f => f.Count > 0)
                .OrderByDescending(f => f.Count)
                .Take(15)
                .ToList();
        }

        [HttpGet("ReportsByReason")]
        public List<ReportsByReasonResponse> ReportsByReason(string dashboardName = null)
        {
            IQueryable<DbReason> reasons = _dbContext.Reasons;
            if (!string.IsNullOrEmpty(dashboardName))
                reasons = reasons.Where(r => r.Dashboard.DashboardName == dashboardName);

            return reasons
                .Select(r => new ReportsByReasonResponse
                {
                    Name = r.Name,
                    Count = r.ReportReasons.Count(rr => rr.Tripped)
                })
                .Where(f => f.Count > 0)
                .OrderByDescending(r => r.Count)
                .Take(15)
                .ToList();
        }

        [HttpGet("ReportsByFeedback")]
        public List<ReportsByFeedbackResponse> ReportsByFeedback(string dashboardName = null)
        {
            IQueryable<DbFeedback> feedbacks = _dbContext.Feedbacks;
            if (!string.IsNullOrEmpty(dashboardName))
                feedbacks = feedbacks.Where(f => f.Dashboard.DashboardName == dashboardName);

            return feedbacks
                .Select(f => new ReportsByFeedbackResponse
                {
                    Name = f.Name,
                    Count = f.ReportFeedbacks.Count(rf => rf.InvalidatedDate == null)
                })
                .Where(f => f.Count > 0)
                .OrderByDescending(f => f.Count)
                .Take(15)
                .ToList();
        }

        [HttpGet("FeedbackByUser")]
        public List<FeedbackByUserResponse> FeedbackByUser(string dashboardName = null)
        {
            if (!string.IsNullOrWhiteSpace(dashboardName))
                return _dbContext.Users
                    .Select(u => new FeedbackByUserResponse
                    {
                        Name = u.Name,
                        Count = u.ReportFeedbacks.Count(rf => rf.InvalidatedDate == null && rf.Feedback.Dashboard.DashboardName == dashboardName)
                    })
                    .Where(f => f.Count > 0)
                    .OrderByDescending(f => f.Count)
                    .Take(15)
                    .ToList();

            return _dbContext.Users
                .Select(u => new FeedbackByUserResponse
                {
                    Name = u.Name,
                    Count = u.ReportFeedbacks.Count(rf => rf.InvalidatedDate == null)
                })
                .Where(f => f.Count > 0)
                .OrderByDescending(f => f.Count)
                .Take(15)
                .ToList();
        }
    }
}
