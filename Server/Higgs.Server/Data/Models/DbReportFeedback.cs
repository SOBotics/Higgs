﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbReportFeedback
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public DbUser User { get; set; }

        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public DbReport Report { get; set; }

        public int FeedbackId { get; set; }
        [ForeignKey("FeedbackId")]
        public DbFeedback Feedback { get; set; }

        public DateTime? InvalidatedDate { get; set; }
        public string InvalidationReason { get; set; }

        [ForeignKey("InvalidatedBy")]
        public int? InvalidatedByUserId { get; set; }
        public DbUser InvalidatedBy { get; set; }
    }
}
