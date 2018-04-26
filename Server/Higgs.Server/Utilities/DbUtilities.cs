using System.Collections.Generic;
using System.Linq;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Higgs.Server.Utilities
{
    public static class DbUtilities
    {
        private static readonly string[] DEFAULT_NEW_USER_SCOPES = {Scopes.SCOPE_REVIEWER};

        public static DbUser GetOrCreateUser(this HiggsDbContext dbContext, int accountId)
        {
            return GetOrCreateUser(dbContext, accountId, DEFAULT_NEW_USER_SCOPES);
        }

        public static DbUser GetOrCreateUser(this HiggsDbContext dbContext, int accountId, IEnumerable<string> scopes)
        {
            var existingUser = dbContext.Users.Include(u => u.UserScopes).FirstOrDefault(u => u.AccountId == accountId);

            if (existingUser == null)
            {
                existingUser = new DbUser
                {
                    AccountId = accountId,
                };
                dbContext.Users.Add(existingUser);
                foreach (var scope in scopes)
                    dbContext.UserScopes.Add(new DbUserScope
                    {
                        ScopeName = scope,
                        UserId = accountId
                    });
            }
            return existingUser;
        }
    }
}
