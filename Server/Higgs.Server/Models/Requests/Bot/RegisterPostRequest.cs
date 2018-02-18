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
		/// The confidence of the report, between 0 and 100
		/// </summary>
		public double Confidence { get; set; }
		/// <summary>
		/// A list of reasons the report was detected
		/// </summary>
		public List<string> Reasons { get; set; }
		/// <summary>
		/// Any custom attributes to be associated with the report
		/// </summary>
		public List<RegsiterPostAttribute> Attributes { get; set; }
	}

	public class RegsiterPostAttribute
	{
		[Required]
		public string Key { get; set; }
		[Required]
		public string Value { get; set; }
	}
}
