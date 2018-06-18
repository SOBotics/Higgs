using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbReason
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public DbDashboard Dashboard { get; set; }

        [InverseProperty("Reason")]
        public List<DbReportReason> ReportReasons { get; set; }
    }
}
