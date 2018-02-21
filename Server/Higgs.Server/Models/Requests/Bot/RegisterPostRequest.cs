using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Bot
{
    public class RegisterPostRequest
    {
        /// <summary>
        ///     Title of the report (for example, the question title)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Link to detected content
        /// </summary>
        public string ContentUrl { get; set; }

        public string ContentSite { get; set; }
        public string ContentType { get; set; }
        public int? ContentId { get; set; }

        /// <summary>
        ///     The score of the report, between 0 and 1
        /// </summary>
        public double? DetectionScore { get; set; }

        /// <summary>
        ///     The content of the report
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     The obfuscated content of the report, seen by unregistered users
        /// </summary>
        public string ObfuscatedContent { get; set; }

        public string AuthorName { get; set; }

        public int? AuthorReputation { get; set; }

        public DateTime? ContentCreationDate { get; set; }

        public DateTime? DetectedDate { get; set; }

        /// <summary>
        ///     A list of reasons the report was detected
        /// </summary>
        public List<RegisterPostReason> Reasons { get; set; }

        /// <summary>
        ///     A list of feedback types
        /// </summary>
        public List<string> AllowedFeedback { get; set; }

        /// <summary>
        ///     Any custom attributes to be associated with the report
        /// </summary>
        public List<RegsiterPostAttribute> Attributes { get; set; }
    }

    public class RegisterPostReason
    {
        [Required]
        public string ReasonName { get; set; }

        /// <summary>
        /// Confidence of the reason, between 0 and 1
        /// </summary>
        public double? Confidence { get; set; }
    }

    public class RegsiterPostAttribute
    {
        [Required] public string Key { get; set; }
        [Required] public string Value { get; set; }
    }
}