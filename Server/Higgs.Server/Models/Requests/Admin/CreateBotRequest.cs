using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class CreateBotRequest
    {
        /// <summary>
        ///     Public key of bot used to sign JWT payloads
        /// </summary>
        [Required]
        public string PublicKey { get; set; }

        /// <summary>
        ///     Name of the bot
        /// </summary>
        [Required]
        public string Name { get; set; }

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