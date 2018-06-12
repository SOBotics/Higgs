using System.Collections.Generic;
using Higgs.Server.Models.Shared;

namespace Higgs.Server.Models.Requests.Reviewer
{
    public class ReportsRequest : PagingRequest
    {
        public string Content { get; set; }
        public int? BotId { get; set; }
        public bool? HasFeedback { get; set; }
        public bool? Conflicted { get; set; }
        public List<int> Feedbacks { get; set; }
        public List<int> Reasons { get; set; }
    }
}