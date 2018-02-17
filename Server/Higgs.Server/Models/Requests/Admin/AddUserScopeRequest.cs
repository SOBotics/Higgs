using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class AddUserScopeRequest
    {
		/// <summary>
		/// User to add the scope to
		/// </summary>
		[Required]
		public int UserId { get; set; }

		/// <summary>
		/// Scope to add
		/// </summary>
		[Required]
		public string Scope { get; set; }
    }
}
