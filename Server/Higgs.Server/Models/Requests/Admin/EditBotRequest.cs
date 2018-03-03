using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class EditCreateBotRequest : CreateBotRequest
    {
        /// <summary>
        ///     Id of bot to be edited
        /// </summary>
        [Required]
        public int BotId { get; set; }
    }
}