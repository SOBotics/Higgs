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
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                var dateFrom = DateTime.UtcNow.AddMonths(-3);
                var dateTo = DateTime.UtcNow.Date;
                var results = connection.Query<ReportsOverTimeResponse>(@"
SELECT ""r0"".""BotId"" AS ""BotId"", ""r.Bot0"".""DashboardName"", DATE_TRUNC('day', ""r0"".""DetectedDate"") AS ""Date"", COUNT(1) as ""Count""
FROM ""Reports"" AS ""r0""
INNER JOIN ""Bots"" AS ""r.Bot0"" ON ""r0"".""BotId"" = ""r.Bot0"".""Id""
WHERE ""r0"".""DetectedDate"" >= @dateFrom AND ""r0"".""DetectedDate"" < @dateTo AND (@dashboardName IS NULL OR @dashboardName = '' OR @dashboardName = ""r.Bot0"".""DashboardName"")
GROUP BY ""r0"".""BotId"", ""r.Bot0"".""DashboardName"", DATE_TRUNC('day', ""r0"".""DetectedDate"")
", new
                {
                    dateFrom,
                    dateTo,
                    dashboardName
                }).ToList();

                return results;
            }
        }

        [HttpGet("ReportsTotal")]
        public List<ReportsTotalResponse> ReportsTotal()
        {
            return _dbContext.Bots
                .Select(b => new ReportsTotalResponse
                {
                    BotId = b.Id,
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
                reasons = reasons.Where(r => r.Bot.DashboardName == dashboardName);
            
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
                feedbacks = feedbacks.Where(f => f.Bot.DashboardName == dashboardName);

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
                        Count = u.ReportFeedbacks.Count(rf => rf.InvalidatedDate == null && rf.Feedback.Bot.DashboardName == dashboardName)
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
