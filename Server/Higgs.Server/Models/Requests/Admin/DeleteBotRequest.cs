using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class DeleteBotRequest
    {
        /// <summary>
        ///     Id of bot to be deleted
        /// </summary>
        [Required]
        public int BotId { get; set; }
    }
}