using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Higgs.Server.Data.Models
{
    public class DbUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; set; }

        public string Name { get; set; }

        [InverseProperty("User")]
        public List<DbUserScope> UserScopes { get; set; }

        [InverseProperty("User")]
        public List<DbReportFeedback> ReportFeedbacks { get; set; }

        [InverseProperty("InvalidatedBy")]
        public List<DbReportFeedback> ReportInvalidations { get; set; }

        [InverseProperty("OwnerAccount")]
        public List<DbDashboard> OwnedBots { get; set; }
    }
}