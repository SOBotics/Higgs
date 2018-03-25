using System.Collections.Generic;

namespace Higgs.Server.Models.Requests.Bot
{
    public class AquireTokenRequest
    {
        public int BotId { get; set; }
        public string Secret { get; set; }
        public List<string> RequestedScopes { get; set; }
    }
}
