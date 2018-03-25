﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.Admin;
using Higgs.Server.Models.Requests.Bot;
using Higgs.Server.Models.Responses;
using Higgs.Server.Models.Responses.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly HiggsDbContext _dbContext;

        public AdminController(HiggsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Lists all bots
        /// </summary>
        [HttpGet("Bots")]
        [Authorize(Scopes.ADMIN_VIEW_BOT_DETAILS)]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(List<BotsResponse>), Description = "View bot details")]
        public List<BotsResponse> Bots()
        {
            return _dbContext.Bots.Select(b => new BotsResponse
            {
                BotId = b.Id,
                Description = b.Description,
                Name = b.Name,
            }).ToList();
        }

        /// <summary>
        ///     Register a bot
        /// </summary>
        /// <returns>The ID of the created bot</returns>
        [HttpPost("RegisterBot")]
        [Authorize(Scopes.ADMIN_REGISTER_BOT)]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(int), "Successfully registered bot")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult RegisterBot([FromBody] CreateBotRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new ErrorResponse("Name is required"));
            if (string.IsNullOrWhiteSpace(request.Description))
                return BadRequest(new ErrorResponse("Description is required"));
            if (string.IsNullOrWhiteSpace(request.Secret))
                return BadRequest(new ErrorResponse("Secret is required"));

            if (_dbContext.Bots.Any(b => b.Name == request.Name))
                return BadRequest(new ErrorResponse($"Bot with name '{request.Name}' already exists"));
            
            var bot = new DbBot
            {
                Name = request.Name,
                DashboardName = request.DashboardName,
                Description = request.Description,
                Secret = BCrypt.Net.BCrypt.HashPassword(request.Secret),
                FavIcon = request.FavIcon,
                Homepage = request.Homepage,
                LogoUrl = request.LogoUrl,
                TabTitle = request.TabTitle
            };
            _dbContext.Bots.Add(bot);
            _dbContext.SaveChanges();
            return Json(bot.Id);
        }

        [HttpPost("RegisterFeedbackTypesForBot")]
        [Authorize(Scopes.BOT_SET_FEEDBACK_TYPES)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult RegisterFeedbackTypesForBot([FromBody] RegisterFeedbackTypesForBotRequest request)
        {
            var existingFeedbacks = _dbContext.Feedbacks.Where(f => f.BotId == request.BotId)
                .ToList()
                .GroupBy(f => f.Name) // In case there are duplicates, just handle the first one
                .Select(g => g.First())
                .ToDictionary(f => f.Name, f => f, StringComparer.OrdinalIgnoreCase);

            var requestFeedback = new HashSet<string>(request.FeedbackTypes.Select(ft => ft.Name), StringComparer.OrdinalIgnoreCase);
            foreach (var feedback in existingFeedbacks.Values)
            {
                if (!requestFeedback.Contains(feedback.Name))
                    _dbContext.Feedbacks.Remove(feedback);
            }

            foreach (var feedbackType in request.FeedbackTypes)
            {
                DbFeedback dbFeedback;
                if (existingFeedbacks.ContainsKey(feedbackType.Name))
                    dbFeedback = existingFeedbacks[feedbackType.Name];
                else
                {
                    dbFeedback = new DbFeedback
                    {
                        BotId = request.BotId,
                        Name = feedbackType.Name
                    };
                    _dbContext.Feedbacks.Add(dbFeedback);
                }

                dbFeedback.Colour = feedbackType.Colour;
                dbFeedback.Icon = feedbackType.Icon;
                dbFeedback.IsActionable = feedbackType.IsActionable;
                dbFeedback.RequiredActions = feedbackType.RequiredActions;
            }

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("Bot")]
        [Authorize(Scopes.ADMIN_VIEW_BOT_DETAILS)]
        public BotResponse Bot(int botId)
        {
            return _dbContext.Bots.Where(b => b.Id == botId)
                .Select(b => new BotResponse
                {
                    Id = b.Id,
                    Name = b.Name,
                    DashboardName = b.DashboardName,
                    Description = b.Description,
                    Homepage = b.Homepage,
                    LogoUrl = b.LogoUrl,
                    FavIcon = b.FavIcon,
                    TabTitle = b.TabTitle
                }).FirstOrDefault();
        }

        [HttpGet("Scopes")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<string>))]
        [Authorize(Scopes.ADMIN_VIEW_SCOPES)]
        public List<string> AllScopes()
        {
            return Scopes.AllScopes.Select(s => s.Key).ToList();
        }

        [HttpGet("BotScopes")]
        [Authorize(Scopes.ADMIN_VIEW_BOT_DETAILS)]
        public List<string> BotScopes(int botId)
        {
            var botScopes = _dbContext.Bots.Where(b => b.Id == botId)
                .SelectMany(b => b.BotScopes.Select(bs => bs.ScopeName))
                .ToList();

            return botScopes;
        }
       
        /// <summary>
        ///     Update a bots details
        /// </summary>
        [HttpPost("EditBot")]
        [Authorize(Scopes.ADMIN_EDIT_BOT)]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Successfully edited bot")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse), Description = "Bot not found")]
        public IActionResult EditBot([FromBody] EditCreateBotRequest request)
        {
            var existingBot = _dbContext.Bots.FirstOrDefault(b => b.Id == request.BotId);
            if (existingBot == null)
                return BadRequest(new ErrorResponse($"Bot with id {request.BotId} not found."));

            existingBot.Name = request.Name;
            existingBot.DashboardName = request.DashboardName;
            existingBot.Description = request.Description;

            // Deliberately not using IsNullOrWhitespace here, as an admin may want to revoke credentials from the bot entirely
            if (!string.IsNullOrEmpty(request.Secret))
                existingBot.Secret = BCrypt.Net.BCrypt.HashPassword(request.Secret);

            existingBot.FavIcon = request.FavIcon;
            existingBot.Homepage = request.Homepage;
            existingBot.LogoUrl = request.LogoUrl;
            existingBot.TabTitle = request.TabTitle;

            _dbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        ///     Deactivates a bot
        /// </summary>
        [HttpPost("DeactiveateBot")]
        [Authorize(Scopes.ADMIN_DEACTIVATE_BOT)]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Successfully deactivated bot")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult DeactiveateBot([FromBody] DeleteBotRequest request)
        {
            return Ok(0);
        }

        /// <summary>
        ///     Set bot scopes
        /// </summary>
        [HttpPost("SetBotScopes")]
        [Authorize(Scopes.ADMIN_EDIT_BOT_SCOPE)]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Successfully updated bots scopes")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult SetBotScopes([FromBody] SetBotScopesRequest request)
        {
            var bot = _dbContext.Bots.FirstOrDefault(b => b.Id == request.BotId);
            if (bot == null)
                return BadRequest(new ErrorResponse("Bot does not exist with that id"));

            var requestScopesLookup = new HashSet<string>(request.Scopes);
            var existingBotScopes = _dbContext.BotScopes.Where(bs => bs.BotId == request.BotId).ToList();
            var existingScopesLookup = existingBotScopes.ToDictionary(s => s.ScopeName, s => s);

            foreach (var existingBotScope in existingBotScopes)
            {
                if (!requestScopesLookup.Contains(existingBotScope.ScopeName))
                    _dbContext.BotScopes.Remove(existingBotScope);
            }

            foreach (var newBotScope in request.Scopes)
            {
                if (!existingScopesLookup.ContainsKey(newBotScope))
                    _dbContext.BotScopes.Add(new DbBotScope {BotId = request.BotId, ScopeName = newBotScope});
            }

            _dbContext.SaveChanges();
            return Ok(0);
        }
        
        /// <summary>
        ///     Lists all users
        /// </summary>
        [HttpGet("Users")]
        [Authorize(Scopes.ADMIN_VIEW_USER_DETAILS)]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(UsersResponse), Description = "View user details")]
        public IActionResult Users()
        {
            return Ok(0);
        }

        /// <summary>
        ///     Add a scope to a user
        /// </summary>
        [HttpPost("SetUserScopes")]
        [Authorize(Scopes.ADMIN_EDIT_USER_SCOPE)]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Successfully added scope to user")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult SetUserScopes([FromBody] AddUserScopeRequest request)
        {
            return Ok(0);
        }
    }
}