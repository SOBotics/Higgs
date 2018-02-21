using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbReportReason
    {
        [Key]
        public int Id { get; set; }

        public double? Confidence { get; set; }

        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public DbReport Report { get; set; }

        public int ReasonId { get; set; }
        [ForeignKey("ReasonId")]
        public DbReason Reason { get; set; }
    }
}
