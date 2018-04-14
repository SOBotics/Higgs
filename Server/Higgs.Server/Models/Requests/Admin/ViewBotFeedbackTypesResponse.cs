using System.Collections.Generic;
using Higgs.Server.Models.Responses.Admin;

namespace Higgs.Server.Models.Requests.Admin
{
    public class EditBotFeedbackTypesRequest
    {
        public int BotId { get; set; }

        public List<ViewBotFeedbackTypesResponse> FeedbackTypes { get; set; }
    }
}
