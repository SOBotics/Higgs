using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Bot
{
    public class AquireTokenRequest
    {
        [Required]
        public int DashboardId { get; set; }
        [Required]
        public string Secret { get; set; }
        public List<string> RequestedScopes { get; set; }
    }
}
