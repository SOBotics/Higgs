using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbReportAttribute
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }

        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public DbReport Report { get; set; }
    }
}
