﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Higgs.Server.Data;
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
        public List<ReportsOverTimeResponse> ReportsOverTime()
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                var dateFrom = DateTime.UtcNow.AddMonths(-3);
                var results = connection.Query<ReportsOverTimeResponse>(@"
SELECT ""r0"".""BotId"" AS ""BotId"", ""r.Bot0"".""DashboardName"", DATE_TRUNC('day', ""r0"".""DetectedDate"") AS ""Date"", COUNT(1) as ""Count""
FROM ""Reports"" AS ""r0""
INNER JOIN ""Bots"" AS ""r.Bot0"" ON ""r0"".""BotId"" = ""r.Bot0"".""Id""
WHERE ""r0"".""DetectedDate"" >= @dateFrom
GROUP BY ""r0"".""BotId"", ""r.Bot0"".""DashboardName"", DATE_TRUNC('day', ""r0"".""DetectedDate"")
", new
                {
                    dateFrom
                }).ToList();

                return results;
            }
        }

        [HttpGet("ReportsTotal")]
        public List<ReportsTotalResponse> ReportsTotal()
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                var results = connection.Query<ReportsTotalResponse>(@"
SELECT ""r0"".""BotId"" AS ""BotId"", ""r.Bot0"".""DashboardName"", COUNT(1) as ""Count""
FROM ""Reports"" AS ""r0""
INNER JOIN ""Bots"" AS ""r.Bot0"" ON ""r0"".""BotId"" = ""r.Bot0"".""Id""
GROUP BY ""r0"".""BotId"", ""r.Bot0"".""DashboardName""
").ToList();

                return results;
            }
        }
    }
}
