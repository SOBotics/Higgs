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
        [Required]
        public string Title { get; set; }

        /// <summary>
        ///     Link to detected content
        /// </summary>
        [Required]
        public string ContentUrl { get; set; }

        /// <summary>
        /// The site on which the content was detected
        /// </summary>
        public string ContentSite { get; set; }
        /// <summary>
        /// The type of content (question, answer, comment, etc)
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// The Id of the content
        /// </summary>
        public int? ContentId { get; set; }

        /// <summary>
        ///     The score of the report, between 0 and 1
        /// </summary>
        public double? DetectionScore { get; set; }

        /// <summary>
        ///     The content of the report
        /// </summary>
        public List<RegisterPostContentFragment> ContentFragments { get; set; }
        
        /// <summary>
        /// The name of the author who created the content
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// The author's reputation
        /// </summary>
        public int? AuthorReputation { get; set; }

        /// <summary>
        /// The UTC date the content was created
        /// </summary>
        public DateTime? ContentCreationDate { get; set; }

        /// <summary>
        /// The UTC date the content was detected
        /// </summary>
        public DateTime? DetectedDate { get; set; }

        /// <summary>
        ///     A list of reasons the report was detected
        /// </summary>
        public List<RegisterPostReason> Reasons { get; set; }

        /// <summary>
        ///     A list of feedback types
        /// </summary>
        [Required]
        public List<string> AllowedFeedback { get; set; }

        /// <summary>
        ///     Any custom attributes to be associated with the report
        /// </summary>
        public List<RegisterPostAttribute> Attributes { get; set; }
    }

    public class RegisterPostContentFragment
    {
        /// <summary>
        /// The order in which the content will be displayed to the user
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// The name of the content type
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The content itself
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Required scopes the user must have to view the content.
        /// </summary>
        public string RequiredScope { get; set; }
    }

    public class RegisterPostReason
    {
        /// <summary>
        /// The name of the reason
        /// </summary>
        [Required]
        public string ReasonName { get; set; }

        /// <summary>
        /// Confidence of the reason, between 0 and 1
        /// </summary>
        public double? Confidence { get; set; }
    }

    public class RegisterPostAttribute
    {
        [Required] public string Key { get; set; }
        [Required] public string Value { get; set; }
    }
}