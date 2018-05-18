using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Higgs.Server.Models.Requests.Admin;

namespace Higgs.Server.Data.Models
{
    public class DbReport
    {
        [Key]
        public int Id { get; set; }

        public int BotId { get; set; }
        [ForeignKey("BotId")]
        public DbBot Bot { get; set; }

        public string Title { get; set; }
        public string ContentUrl { get; set; }
        public string ContentSite { get; set; }
        public string ContentType { get; set; }
        public int? ContentId { get; set; }

        public double? DetectionScore { get; set; }
        public string AuthorName { get; set; }
        public int? AuthorReputation { get; set; }
        public DateTime? ContentCreationDate { get; set; }
        public DateTime? DetectedDate { get; set; }

        public int RequiredFeedback { get; set; }
        public int RequiredFeedbackConflicted { get; set; }
        
        public bool RequiresReview { get; set; }
        public bool Conflicted { get; set; }

        [InverseProperty("Report")]
        public List<DbReportReason> Reasons { get; set; }

        [InverseProperty("Report")]
        public List<DbReportAllowedFeedback> AllowedFeedback { get; set; }

        [InverseProperty("Report")]
        public List<DbContentFragment> ContentFragments { get; set; }

        [InverseProperty("Report")]
        public List<DbReportAttribute> Attributes { get; set; }

        [InverseProperty("Report")]
        public List<DbReportFeedback> Feedbacks { get; set; }

        [InverseProperty("Report")]
        public List<DbConflictException> ConflictExceptions { get; set; }
    }
}