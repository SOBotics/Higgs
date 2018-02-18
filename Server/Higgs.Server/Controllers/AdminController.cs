using System.Collections.Generic;
using System.Linq;
using System.Net;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.Admin;
using Higgs.Server.Models.Responses;
using Higgs.Server.Models.Responses.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server.Controllers
{
    [Route("api/Admin")]
    public class AdminController : Controller
    {
	    private readonly HiggsDbContext _dbContext;
	    public AdminController(HiggsDbContext dbContext)
	    {
		    _dbContext = dbContext;
	    }
		/// <summary>
		/// Lists all bots
		/// </summary>
		[HttpGet("Bots")]
	    [Authorize(Scopes.ADMIN_VIEW_BOT_DETAILS)]
	    [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<BotsResponse>), Description = "View bot details")]
	    public IActionResult Bots()
		{
			return Json(_dbContext.Bots.Select(b => new BotsResponse
			{
				BotId = b.Id,
				Description = b.Description,
				Name = b.Name,
				PublicKey = b.PublicKey
			}).ToList());
		}

		/// <summary>
		/// Register a bot
		/// </summary>
		/// <returns>The ID of the created bot</returns>
		[HttpPost("RegisterBot")]
	    [Authorize(Scopes.ADMIN_REGISTER_BOT)]
		[SwaggerResponse((int)HttpStatusCode.OK, typeof(int), "Successfully registered bot")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
		public IActionResult RegisterBot([FromBody] CreateBotRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Name))
				return BadRequest(new ErrorResponse("Name is required"));
			if (string.IsNullOrWhiteSpace(request.Description))
				return BadRequest(new ErrorResponse("Description is required"));
			if (string.IsNullOrWhiteSpace(request.PublicKey))
				return BadRequest(new ErrorResponse("PublicKey is required"));

			if (_dbContext.Bots.Any(b => b.Name == request.Name))
				return BadRequest(new ErrorResponse($"Bot with name '{request.Name}' already exists"));

			var bot = new DbBot
			{
				Name = request.Name,
				Description = request.Description,
				PublicKey = request.PublicKey
			};
			_dbContext.Bots.Add(bot);
			_dbContext.SaveChanges();
		    return Json(bot.Id);
	    }

		/// <summary>
		/// Update a bots details
		/// </summary>
		[HttpPost("EditBot")]
	    [Authorize(Scopes.ADMIN_EDIT_BOT)]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully edited bot")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
		public IActionResult EditBot([FromBody] EditCreateBotRequest request)
	    {
		    return Ok(0);
	    }

	    /// <summary>
	    /// Deactivates a bot
	    /// </summary>
	    [HttpPost("DeactiveateBot")]
	    [Authorize(Scopes.ADMIN_DELETE_BOT)]
	    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully deactivated bot")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
		public IActionResult DeactiveateBot([FromBody] DeleteCreateBotRequest request)
	    {
		    return Ok(0);
	    }

	    /// <summary>
	    /// Add a scope to a bot
	    /// </summary>
	    [HttpPost("AddBotScope")]
	    [Authorize(Scopes.ADMIN_ADD_BOT_SCOPE)]
	    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully added scope to bot")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
	    public IActionResult AddBotScope([FromBody] DeleteCreateBotRequest request)
	    {
		    return Ok(0);
	    }

	    /// <summary>
	    /// Remove a scope from a bot
	    /// </summary>
	    [HttpPost("RemoveBotScope")]
	    [Authorize(Scopes.ADMIN_REMOVE_BOT_SCOPE)]
	    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully removed scope from bot")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
	    public IActionResult RemoveBotScope([FromBody] AddBotScopeRequest request)
	    {
		    return Ok(0);
	    }

		/// <summary>
		/// Lists all users
		/// </summary>
		[HttpGet("Users")]
	    [Authorize(Scopes.ADMIN_VIEW_USER_DETAILS)]
	    [SwaggerResponse((int)HttpStatusCode.OK, typeof(UsersResponse), Description = "View user details")]
	    public IActionResult Users()
	    {
		    return Ok(0);
	    }

	    /// <summary>
	    /// Add a scope to a user
	    /// </summary>
	    [HttpPost("AddUserScope")]
	    [Authorize(Scopes.ADMIN_ADD_USER_SCOPE)]
	    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully added scope to user")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
	    public IActionResult AddUserScope([FromBody] AddUserScopeRequest request)
	    {
		    return Ok(0);
	    }

	    /// <summary>
	    /// Remove a scope from a user
	    /// </summary>
	    [HttpPost("RemoveUserScope")]
	    [Authorize(Scopes.ADMIN_REMOVE_USER_SCOPE)]
	    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully removed scope from user")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
	    public IActionResult RemoveUserScope([FromBody] RemoveUserScopeRequest request)
	    {
		    return Ok(0);
	    }
	}
}