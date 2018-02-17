using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
	public class EditCreateBotRequest
	{
		/// <summary>
		/// Id of bot to be edited
		/// </summary>
		[Required]
		public int BotId { get; set; }
		
		/// <summary>
		/// New name of the bot
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// New description for the bot
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// New public key for the bot
		/// </summary>
		public string PublicKey { get; set; }
	}
}
