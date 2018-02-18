using System.Linq;
using System.Net;
using Higgs.Server.Data;
using Higgs.Server.Models.Requests.Bot;
using Higgs.Server.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server.Controllers
{
	[Route("[controller]")]
	public class BotController : Controller
    {
	    private readonly HiggsDbContext _dbContext;
	    public BotController(HiggsDbContext dbContext)
	    {
		    _dbContext = dbContext;
	    }
		/// <summary>
		/// Used by bots to aquire an access token
		/// </summary>
		/// <returns>The access token, encryped with the bots public key</returns>
		[HttpPost("AquireToken")]
		[SwaggerResponse((int)HttpStatusCode.OK)]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
		[SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "Signature failed")]
		public IActionResult AquireToken([FromBody] AquireTokenRequest request)
		{
			return Ok(string.Empty);
		}
		
		/// <summary>
		/// Used by bots to register feedback types
		/// </summary>
		/// <returns>The access token</returns>
		[HttpPost("RegisterFeedbackTypes")]
		[Authorize(Scopes.BOT_SET_FEEDBACK_TYPES)]
	    [SwaggerResponse((int)HttpStatusCode.OK)]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
		public IActionResult RegisterFeedbackTypes([FromBody] RegisterFeedbackTypesRequest request)
	    {
		    return Ok();
	    }

	    /// <summary>
		/// Used by bots to register a detected post
		/// </summary>
		[HttpPost("RegisterPost")]
	    [Authorize(Scopes.BOT_REGISTER_POST)]
	    [SwaggerResponse((int)HttpStatusCode.OK)]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
	    public IActionResult RegisterPost([FromBody] RegisterPostRequest request)
	    {
		    return Ok();
	    }
	}
}