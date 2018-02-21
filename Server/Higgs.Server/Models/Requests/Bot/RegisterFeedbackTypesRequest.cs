using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Bot
{
    public class RegisterFeedbackTypesRequest
    {
        /// <summary>
        ///     A list of feedback types
        /// </summary>
        [Required]
        public List<FeedbackType> FeedbackTypes { get; set; }
    }

    public class FeedbackType
    {
        /// <summary>
        ///     The name of the feedback type. Will be displayed on the dashboard. Must be unique per bot.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     The colour of the button reviewers will see when providing feedback
        /// </summary>
        public string Colour { get; set; }

        /// <summary>
        ///     A link to a feedback icon.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     Whether or not this feedback is actionable
        /// </summary>
        public bool? IsActionable { get; set; }

        /// <summary>
        ///     Number of required votes to mark the post as actioned
        /// </summary>
        public int? RequiredActions { get; set; }
    }
}