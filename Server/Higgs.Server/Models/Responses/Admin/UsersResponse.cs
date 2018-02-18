using System.Collections.Generic;
using Higgs.Server.Models.Shared;

namespace Higgs.Server.Models.Responses.Admin
{
    public class UsersResponse
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public List<ScopeInfo> Scopes { get; set; }
    }
}