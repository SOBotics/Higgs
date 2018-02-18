using Higgs.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Higgs.Server.Data
{
    public class HiggsDbContext : DbContext
    {
        public HiggsDbContext(DbContextOptions<HiggsDbContext> options) : base(options)
        {
        }

        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbBot> Bots { get; set; }
        public DbSet<DbScope> Scopes { get; set; }
        public DbSet<DbUserScope> UserScopes { get; set; }
    }
}