using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbUserScope
    {
        [Key] public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")] public DbUser User { get; set; }


        public string ScopeName { get; set; }

        [ForeignKey("ScopeName ")] public DbScope Scope { get; set; }
    }
}