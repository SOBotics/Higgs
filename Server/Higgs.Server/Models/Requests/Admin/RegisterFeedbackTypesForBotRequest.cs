using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Higgs.Server.Models.Requests.Bot;

namespace Higgs.Server.Models.Requests.Admin
{
    public class RegisterFeedbackTypesForBotRequest
    {
        [Required]
        public int BotId { get; set; }
        /// <summary>
        ///     A list of feedback types
        /// </summary>
        [Required]
        public List<FeedbackType> FeedbackTypes { get; set; }
    }
}
