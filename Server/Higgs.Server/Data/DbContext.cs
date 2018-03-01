using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.Admin;
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
        public DbSet<DbFeedback> Feedbacks { get; set; }
        public DbSet<DbReportAllowedFeedback> ReportAllowedFeedbacks { get; set; }
        public DbSet<DbContentFragment> ContentFragments { get; set; }
        public DbSet<DbReport> Reports { get; set; }
        public DbSet<DbReportAttribute> ReportAttributes { get; set; }
        public DbSet<DbReportReason> ReportReasons { get; set; }
        public DbSet<DbReason> Reasons { get; set; }
        public DbSet<DbScope> Scopes { get; set; }
        public DbSet<DbUserScope> UserScopes { get; set; }
    }
}