using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbDashboardScope
    {
        [Key]
        public int Id { get; set; }

        public int DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public DbDashboard Dashboard { get; set; }

        [Required]
        public string ScopeName { get; set; }
        [ForeignKey("ScopeName ")]
        public DbScope Scope { get; set; }
    }
}
