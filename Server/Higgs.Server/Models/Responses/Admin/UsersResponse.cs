using System.Collections.Generic;

namespace Higgs.Server.Models.Responses.Admin
{
    public class UsersResponse
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public List<string> Scopes { get; set; }
    }
}