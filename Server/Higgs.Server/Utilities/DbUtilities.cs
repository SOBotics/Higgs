using System;
using System.Collections.Generic;
using System.Linq;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Shared;
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

        public static PagingResponse<T> Page<T>(this IQueryable<T> query, PagingRequest pagingRequest)
        {
            return query.Page(pagingRequest.PageNumber ?? 1, pagingRequest.PageSize ?? 50);
        }
        public static PagingResponse<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = query.Count();
            var data =
                query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

            return new PagingResponse<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int) Math.Ceiling(1.0 * count / pageSize),
                Data = data
            };
        }
    }
}
