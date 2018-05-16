using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbConflictExceptionFeedback
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ConflictException")]
        public int ConflictExceptionId { get; set; }
        public DbConflictException ConflictException { get; set; }

        [ForeignKey("Feedback")]
        public int FeedbackId { get; set; }
        public DbFeedback Feedback { get; set; }
    }
}
