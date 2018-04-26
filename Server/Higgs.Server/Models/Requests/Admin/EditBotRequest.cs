using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class EditCreateBotRequest
    {
        [Required]
        public int BotId { get; set; }

        public string Secret { get; set; }

        /// <summary>
        ///     Name of the bot
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     Name of the dashboard
        /// </summary>
        [Required]
        public string DashboardName { get; set; }

        /// <summary>
        ///     Description of the bot
        /// </summary>
        [Required]
        public string Description { get; set; }

        public string Homepage { get; set; }
        public string LogoUrl { get; set; }

        public string FavIcon { get; set; }
        public string TabTitle { get; set; }
    }
}