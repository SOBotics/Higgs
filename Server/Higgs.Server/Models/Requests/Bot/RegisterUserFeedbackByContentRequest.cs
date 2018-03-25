using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Bot
{
    public class RegisterUserFeedbackByContentRequest
    {
        [Required]
        public string ContentUrl { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Feedback { get; set; }
    }
}
