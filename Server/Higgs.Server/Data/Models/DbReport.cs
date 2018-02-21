using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public double? DetectionScore { get; set; }
        public string Content { get; set; }
        public string ObfuscatedContent { get; set; }
        public string AuthorName { get; set; }
        public int? AuthorReputation { get; set; }
        public DateTime? ContentCreationDate { get; set; }
        public DateTime? DetectedDate { get; set; }

        [InverseProperty("Report")]
        public List<DbReportReason> ReportReasons { get; set; }

        [InverseProperty("Report")]
        public List<DbReportAllowedFeedback> ReportAllowedFeedback { get; set; }
    }
}