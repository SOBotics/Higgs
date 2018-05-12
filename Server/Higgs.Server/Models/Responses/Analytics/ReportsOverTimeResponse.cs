using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Higgs.Server.Models.Responses.Analytics
{
    public class ReportsOverTimeResponse
    {
        public int BotId { get; set; }
        public string DashboardName { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
