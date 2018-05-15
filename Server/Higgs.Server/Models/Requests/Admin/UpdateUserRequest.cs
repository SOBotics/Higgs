using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class UpdateUserRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Scopes { get; set; }
    }
}
