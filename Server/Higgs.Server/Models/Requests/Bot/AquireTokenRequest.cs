using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Bot
{
	public class AquireTokenRequest
    {
		/// <summary>
		/// The ID of the bot
		/// </summary>
		[Required]
		public int BotId { get; set; }

	    /// <summary>
	    /// A list of requested scopes
	    /// </summary>
	    [Required]
	    public List<string> RequiredScopes { get; set; }

		/// <summary>
		/// A randomly generated request ID. Must not be previously used
		/// </summary>
		[Required]
		public string RequestId { get; set; }
    }
}
