using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class EditBotRequest : CreateBotRequest
    {
        [Required]
        public int BotId { get; set; }
    }
}