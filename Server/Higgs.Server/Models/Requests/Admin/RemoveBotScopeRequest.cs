using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class RemoveBotScopeRequest
    {
		/// <summary>
		/// Bot to remove the scope from
		/// </summary>
		[Required]
		public int BotId { get; set; }

		/// <summary>
		/// Scope to remove
		/// </summary>
		[Required]
		public string Scope { get; set; }
    }
}
