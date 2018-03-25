using System;
using System.Collections.Generic;
using System.Linq;
using Higgs.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Higgs.Server.Data
{
    public static class DBExtensions
    {
        public static int RobAccountId = 272446;

        public static void Setup(this HiggsDbContext context)
        {
            SeedData(context);
        }

        private static void SeedData(HiggsDbContext context)
        {
            if (!context.AllMigrationsApplied())
                return;

            var seedUsers = new[] {new SeedUser {AccountId = RobAccountId, UserName = "Rob"}};
            var dbScopes = context.Scopes.ToList().ToLookup(p => p.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var scope in Scopes.AllScopes)
                if (!dbScopes.Contains(scope.Key))
                    context.Scopes.Add(new DbScope {Name = scope.Key, Description = scope.Value});

            foreach (var seedUser in seedUsers)
            {
                var existingUser = context.Users.Include(u => u.UserScopes)
                    .FirstOrDefault(u => u.AccountId == seedUser.AccountId);
                if (existingUser == null)
                {
                    existingUser = new DbUser {AccountId = seedUser.AccountId, Name = seedUser.UserName};
                    context.Users.Add(existingUser);
                }

                if (existingUser.UserScopes == null)
                    existingUser.UserScopes = new List<DbUserScope>();

                foreach (var scope in Scopes.AllScopes)
                {
                    if (existingUser.UserScopes.All(s => !string.Equals(s.ScopeName, scope.Key, StringComparison.OrdinalIgnoreCase)))
                    {
                        context.UserScopes.Add(new DbUserScope { UserId = seedUser.AccountId, ScopeName = scope.Key });
                    }
                }
            }

            context.SaveChanges();
        }

        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        private class SeedUser
        {
            public int AccountId { get; set; }
            public string UserName { get; set; }
        }
    }
}