using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbFeedback
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Colour { get; set; }

        public string Icon { get; set; }

        public bool IsActionable { get; set; }

        public bool IsEnabled { get; set; }

        public int BotId { get; set; }
        [ForeignKey("BotId")]
        public DbBot Bot { get; set; }

        [InverseProperty("Feedback")]
        public List<DbReportFeedback> ReportFeedbacks { get; set; }
    }
}
