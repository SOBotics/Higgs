using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbConflictException
    {
        [Key]
        public int Id { get; set; }

        public bool IsConflict { get; set; }
        public bool RequiresAdmin { get; set; }
        public int? RequiredFeedback { get; set; }

        [ForeignKey("Dashboard")]
        public int? DashboardId { get; set; }
        public DbDashboard Dashboard { get; set; }

        [ForeignKey("Report")]
        public int? ReportId { get; set; }
        public DbReport Report { get; set; }

        [InverseProperty("ConflictException")]
        public List<DbConflictExceptionFeedback> ConflictExceptionFeedbacks { get; set; }
    }
}
