using System.Collections.Generic;
using System.Net;
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
	    /// <summary>
	    /// Lists all bots
	    /// </summary>
	    [HttpGet("Bots")]
	    [Authorize(Scopes.ADMIN_VIEW_BOT_DETAILS)]
	    [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<BotsResponse>), Description = "View bot details")]
	    public IActionResult Bots()
	    {
		    return Ok(0);
	    }

		/// <summary>
		/// Register a bot
		/// </summary>
		/// <returns>The ID of the created bot</returns>
		[HttpPost("Register")]
	    [Authorize(Scopes.ADMIN_REGISTER_BOT)]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully registered bot")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
		public IActionResult Register([FromBody] CreateBotRequest request)
	    {
		    return Ok(0);
	    }

		/// <summary>
		/// Update a bots details
		/// </summary>
		[HttpPost("Edit")]
	    [Authorize(Scopes.ADMIN_EDIT_BOT)]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully edited bot")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
		public IActionResult Edit([FromBody] EditCreateBotRequest request)
	    {
		    return Ok(0);
	    }

	    /// <summary>
	    /// Delete a bot
	    /// </summary>
	    [HttpPost("Delete")]
	    [Authorize(Scopes.ADMIN_DELETE_BOT)]
	    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully deleted bot")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
		public IActionResult Delete([FromBody] DeleteCreateBotRequest request)
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
	    [Authorize(Scopes.ADMIN_ADD_BOT_SCOPE)]
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
	    [Authorize(Scopes.ADMIN_REMOVE_BOT_SCOPE)]
	    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Successfully removed scope from user")]
	    [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
	    public IActionResult RemoveUserScope([FromBody] RemoveUserScopeRequest request)
	    {
		    return Ok(0);
	    }
	}
}