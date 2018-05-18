
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

        public int RequiredFeedback { get; set; }
        public int RequiredFeedbackConflicted { get; set; }

        public List<BotResponseFeedback> Feedbacks { get; set; }
        public List<BotResponseConflictExceptions> ConflictExceptions { get; set; }
    }

    public class BotResponseFeedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Icon { get; set; }
        public bool IsActionable { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class BotResponseConflictExceptions
    {
        public int Id { get; set; }
        public bool IsConflict { get; set; }
        public bool RequiresAdmin { get; set; }
        public int? RequiredFeedback { get; set; }
        public List<int> BotResponseConflictFeedbacks { get; set; }
    }

    public class BotResponseConflictFeedback
    {
        public int FeedbackId { get; set; }
        public string FeedbackName { get; set; }
    }
}
