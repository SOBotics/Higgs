using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Bot
{
    public class RegisterUserFeedbackRequest
    {
        [Required]
        public int ReportId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Feedback { get; set; }
    }
}
