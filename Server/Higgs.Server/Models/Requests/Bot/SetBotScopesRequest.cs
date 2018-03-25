using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Bot
{
    public class SetBotScopesRequest
    {
        [Required]
        public int BotId { get; set; }
        [Required]
        public List<string> Scopes { get; set; }
    }
}
