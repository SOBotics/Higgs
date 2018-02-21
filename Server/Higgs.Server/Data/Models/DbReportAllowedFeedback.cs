using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbReportAllowedFeedback
    {
        [Key]
        public int Id { get; set; }

        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public DbReport Report { get; set; }

        public int FeedbackId { get; set; }
        [ForeignKey("FeedbackId")]
        public DbFeedback Feedback { get; set; }
    }
}
