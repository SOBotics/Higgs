using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbDashboard
    {
        [Key]
        public int Id { get; set; }
        public string Secret { get; set; }

        public string BotName { get; set; }
        public string DashboardName { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string LogoUrl { get; set; }

        public string FavIcon { get; set; }
        public string TabTitle { get; set; }

        public int RequiredFeedback { get; set; }
        public int RequiredFeedbackConflicted { get; set; }

        [ForeignKey("OwnerAccount")]
        public int OwnerAccountId { get; set; }
        public DbUser OwnerAccount { get; set; }

        [InverseProperty("Dashboard")]
        public List<DbReport> Reports { get; set; }

        [InverseProperty("Dashboard")]
        public List<DbDashboardScope> Scopes { get; set; }

        [InverseProperty("Dashboard")]
        public List<DbFeedback> Feedbacks { get; set; }

        [InverseProperty("Dashboard")]
        public List<DbConflictException> ConflictExceptions { get; set; }
    }
}