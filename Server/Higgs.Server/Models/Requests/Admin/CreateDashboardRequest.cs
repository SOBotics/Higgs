using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class CreateDashboardRequest
    {
        public int? OwnerAccountId { get; set; }

        public string Secret { get; set; }

        /// <summary>
        ///     Name of the bot
        /// </summary>
        [Required]
        public string BotName { get; set; }

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

        public List<CreateBotRequestFeedback> Feedbacks { get; set; }
        public List<CreateBotRequestExceptions> ConflictExceptions { get; set; }

        [Required]
        public int RequiredFeedback { get; set; }

        [Required]
        public int RequiredFeedbackConflicted { get; set; }
    }

    public class CreateBotRequestFeedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Icon { get; set; }
        public bool IsActionable { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class CreateBotRequestExceptions
    {
        public int Id { get; set; }
        public bool IsConflict { get; set; }
        public bool RequiresAdmin { get; set; }
        public int RequiredFeedback { get; set; }
        public List<int> BotResponseConflictFeedbacks { get; set; }
    }

    public class CreateBotRequestConflictFeedback
    {
        public int FeedbackId { get; set; }
        public string FeedbackName { get; set; }
    }
}