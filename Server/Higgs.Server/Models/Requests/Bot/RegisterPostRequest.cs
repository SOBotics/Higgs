using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Bot
{
	public class RegisterPostRequest
	{
		/// <summary>
		/// The ID of the bot, returned by the corresponding register call
		/// </summary>
		[Required]
		public int BotId { get; set; }
		/// <summary>
		/// Link to detected post
		/// </summary>
		[Required]
		public string PostUrl { get; set; }
		/// <summary>
		/// Any details about the report - for example, why the post was reported
		/// </summary>
		public string Details { get; set; }

		/// <summary>
		/// Any custom attributes to be associated with the report
		/// </summary>
		public List<RegsiterPostAttribute> Attributes { get; set; }
	}

	public class RegsiterPostAttribute
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}
}
