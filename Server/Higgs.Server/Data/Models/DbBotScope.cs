using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbBotScope
    {
        [Key]
        public int Id { get; set; }

        public int BotId { get; set; }
        [ForeignKey("BotId")]
        public DbBot Bot { get; set; }

        [Required]
        public string ScopeName { get; set; }
        [ForeignKey("ScopeName ")]
        public DbScope Scope { get; set; }
    }
}
