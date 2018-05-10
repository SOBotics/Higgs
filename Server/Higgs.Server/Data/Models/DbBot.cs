using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbBot
    {
        [Key]
        public int Id { get; set; }
        public string Secret { get; set; }

        public string Name { get; set; }
        public string DashboardName { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string LogoUrl { get; set; }

        public string FavIcon { get; set; }
        public string TabTitle { get; set; }

        [ForeignKey("OwnerAccount")]
        public int OwnerAccountId { get; set; }
        public DbUser OwnerAccount { get; set; }

        [InverseProperty("Bot")]
        public List<DbBotScope> BotScopes { get; set; }

        [InverseProperty("Bot")]
        public List<DbFeedback> Feedbacks { get; set; }
    }
}