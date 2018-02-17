using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class CreateBotRequest
    {
		/// <summary>
		/// Name of the bot to be registered
		/// </summary>
		[Required]
	    public string Name { get; set; }

		/// <summary>
		/// Description of the bot to be registered
		/// </summary>
		[Required]
	    public string Description { get; set; }

		/// <summary>
		/// Public key of bot used to sign JWT payloads
		/// </summary>
		[Required]
	    public string PublicKey { get; set; }
	}
}
