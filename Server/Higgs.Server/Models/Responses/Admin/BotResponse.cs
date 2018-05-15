
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Responses.Admin
{
    public class BotResponse
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string DashboardName { get; set; }

        [Required]
        public string Description { get; set; }

        public int OwnerAccountId { get; set; }

        public string Homepage { get; set; }
        public string LogoUrl { get; set; }

        public string FavIcon { get; set; }
        public string TabTitle { get; set; }
    }
}
