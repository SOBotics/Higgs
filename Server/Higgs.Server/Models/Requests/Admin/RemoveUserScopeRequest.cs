using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class RemoveUserScopeRequest
    {
        /// <summary>
        ///     User to remove the scope from
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        ///     Scope to remove
        /// </summary>
        [Required]
        public string Scope { get; set; }
    }
}