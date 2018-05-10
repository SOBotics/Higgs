using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.Admin;
using Higgs.Server.Models.Responses;
using Higgs.Server.Models.Responses.Admin;
using Higgs.Server.Utilities;
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
        [Authorize]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(List<BotsResponse>), Description = "View bot details")]
        public List<BotsResponse> Bots()
        {
            if (!User.HasClaim(Scopes.SCOPE_BOT_OWNER) && !User.HasClaim(Scopes.SCOPE_ADMIN))
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            IQueryable<DbBot> bots = _dbContext.Bots;
            if (!User.HasClaim(Scopes.SCOPE_ADMIN))
            {
                var userId = User.GetUserId();
                if (!userId.HasValue)
                    throw new HttpStatusException(HttpStatusCode.Unauthorized);

                bots = bots.Where(b => b.OwnerAccountId == userId);
            }
                
            return bots.Select(b => new BotsResponse
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
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
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
            var userId = User.GetUserId();
            if (!userId.HasValue)
                throw new HttpStatusException(HttpStatusCode.Unauthorized);
            
            var bot = new DbBot
            {
                Name = request.Name,
                DashboardName = request.DashboardName,
                Description = request.Description,
                Secret = BCrypt.Net.BCrypt.HashPassword(request.Secret),
                FavIcon = request.FavIcon,
                Homepage = request.Homepage,
                LogoUrl = request.LogoUrl,
                TabTitle = request.TabTitle,
                OwnerAccountId = userId.Value
            };

            if (request.OwnerAccountId.HasValue && User.HasClaim(Scopes.SCOPE_ADMIN))
                bot.OwnerAccountId = request.OwnerAccountId.Value;

            _dbContext.Bots.Add(bot);
            _dbContext.BotScopes.Add(new DbBotScope
            {
                Bot = bot,
                ScopeName = Scopes.SCOPE_BOT
            });

            _dbContext.SaveChanges();
            return Json(bot.Id);
        }
        
        [HttpGet("Bot")]
        public BotResponse Bot(int botId)
        {
            if (!User.HasClaim(Scopes.SCOPE_BOT_OWNER) && !User.HasClaim(Scopes.SCOPE_ADMIN))
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            var bot = _dbContext.Bots.Where(b => b.Id == botId)
                .Select(b => new BotResponse
                {
                    Id = b.Id,
                    Name = b.Name,
                    DashboardName = b.DashboardName,
                    Description = b.Description,
                    Homepage = b.Homepage,
                    LogoUrl = b.LogoUrl,
                    FavIcon = b.FavIcon,
                    TabTitle = b.TabTitle,
                    OwnerAccountId = b.OwnerAccountId
                }).FirstOrDefault();

            if (bot == null)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Bot not found");
            if (User.HasClaim(Scopes.SCOPE_ADMIN) || bot.OwnerAccountId == User.GetUserId())
                return bot;
            
            throw new HttpStatusException(HttpStatusCode.Unauthorized);
        }

        [HttpGet("Scopes")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<string>))]
        [Authorize(Scopes.SCOPE_ADMIN)]
        public List<string> AllScopes()
        {
            return Scopes.AllScopes.Select(s => s.Key).ToList();
        }
       
        /// <summary>
        ///     Update a bots details
        /// </summary>
        [HttpPost("EditBot")]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Successfully edited bot")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse), Description = "Bot not found")]
        public IActionResult EditBot([FromBody] EditCreateBotRequest request)
        {
            if (!User.HasClaim(Scopes.SCOPE_BOT_OWNER) && !User.HasClaim(Scopes.SCOPE_ADMIN))
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            var existingBot = _dbContext.Bots.Include(b => b.BotScopes).FirstOrDefault(b => b.Id == request.BotId);
            if (existingBot == null)
                return BadRequest(new ErrorResponse($"Bot with id {request.BotId} not found."));

            if (!User.HasClaim(Scopes.SCOPE_ADMIN) && existingBot.OwnerAccountId != User.GetUserId())
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            existingBot.Name = request.Name;
            existingBot.DashboardName = request.DashboardName;
            existingBot.Description = request.Description;

            // Deliberately not using IsNullOrWhitespace here, as an admin may want to revoke credentials from the bot entirely
            if (!string.IsNullOrEmpty(request.Secret))
                existingBot.Secret = BCrypt.Net.BCrypt.HashPassword(request.Secret);
            
            if (request.OwnerAccountId.HasValue && User.HasClaim(Scopes.SCOPE_ADMIN))
                existingBot.OwnerAccountId = request.OwnerAccountId.Value;
            
            existingBot.FavIcon = request.FavIcon;
            existingBot.Homepage = request.Homepage;
            existingBot.LogoUrl = request.LogoUrl;
            existingBot.TabTitle = request.TabTitle;

            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("ViewBotFeedbackTypes")]
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(IList<ViewBotFeedbackTypesResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ErrorResponse), Description = "Bot not found")]
        public IActionResult ViewBotFeedbackTypes(int botId)
        {
            var existingBot = _dbContext.Bots.FirstOrDefault(b => b.Id == botId);
            if (existingBot == null)
                return BadRequest(new ErrorResponse($"Bot with id {botId} not found."));

            var feedback = _dbContext.Feedbacks.Where(f => f.BotId == botId)
                .Select(f => new ViewBotFeedbackTypesResponse
                {
                    Id = f.Id,
                    Name = f.Name,
                    Colour = f.Colour,
                    Icon = f.Icon,
                    IsActionable = f.IsActionable,
                    IsEnabled = f.IsEnabled
                }).ToList();

            return Json(feedback);
        }

        [HttpPost("EditBotFeedbackTypes")]
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Successfully edited bot feedback types")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse), Description = "Bot not found")]
        public IActionResult EditBotFeedbackTypes([FromBody] EditBotFeedbackTypesRequest request)
        {
            var existingBot = _dbContext.Bots.Include(b => b.Feedbacks).FirstOrDefault(b => b.Id == request.BotId);
            if (existingBot == null)
                return BadRequest(new ErrorResponse($"Bot with id {request.BotId} not found."));

            var feedbackTypes = existingBot.Feedbacks.ToDictionary(f => f.Id, f => f);

            foreach (var feedbackType in request.FeedbackTypes)
            {
                if (feedbackTypes.ContainsKey(feedbackType.Id))
                {
                    // Edit it
                    var dbFeedbackType = feedbackTypes[feedbackType.Id];
                    dbFeedbackType.Name = feedbackType.Name;
                    dbFeedbackType.Colour = feedbackType.Colour;
                    dbFeedbackType.Icon = feedbackType.Icon;
                    dbFeedbackType.IsActionable = feedbackType.IsActionable;
                    dbFeedbackType.IsEnabled = feedbackType.IsEnabled;
                }
                else
                {
                    if (feedbackType.Id == 0) // It's a new one
                    {
                        var dbFeedbackType = new DbFeedback
                        {
                            BotId = request.BotId,
                            Name = feedbackType.Name,
                            Colour = feedbackType.Colour,
                            Icon = feedbackType.Icon,
                            IsActionable = feedbackType.IsActionable,
                            IsEnabled = feedbackType.IsEnabled
                        };
                        existingBot.Feedbacks.Add(dbFeedbackType);

                        _dbContext.Feedbacks.Add(dbFeedbackType);
                    }
                }
            }
            
            if (existingBot.Feedbacks.GroupBy(f => f.Name, StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
            {
                // We have duplicates
                return BadRequest(new ErrorResponse($"Feedback names must be unique"));
            }
            
            _dbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        ///     Deactivates a bot
        /// </summary>
        [HttpPost("DeactiveateBot")]
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Successfully deactivated bot")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse))]
        public IActionResult DeactiveateBot([FromBody] DeleteBotRequest request)
        {
            return Ok();
        }

        /// <summary>
        ///     Lists all users
        /// </summary>
        [HttpGet("Users")]
        [Authorize(Scopes.SCOPE_ADMIN)]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(List<UsersResponse>), Description = "View all users")]
        public IActionResult Users()
        {
            return Json(_dbContext.Users.Select(u => new UsersResponse
            {
                UserId = u.AccountId,
                DisplayName = u.Name,
                Scopes = u.UserScopes.Select(us => us.ScopeName).ToList()
            }).ToList());
        }

        /// <summary>
        ///     Lists all users
        /// </summary>
        [HttpGet("User")]
        [Authorize(Scopes.SCOPE_ADMIN)]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(UsersResponse), Description = "View user details")]
        public IActionResult GetUser(int userId)
        {
            return Json(_dbContext.Users
                .Where(u => u.AccountId == userId)
                .Select(u => new UsersResponse
                {
                    UserId = u.AccountId,
                    DisplayName = u.Name,
                    Scopes = u.UserScopes.Select(us => us.ScopeName).ToList()
                }).FirstOrDefault());
        }

        /// <summary>
        ///     Lists all users
        /// </summary>
        [HttpPost("User")]
        [Authorize(Scopes.SCOPE_ADMIN)]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Update user details")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "User doesn't exist")]
        public IActionResult UpdateUserDetails([FromBody] UpdateUserRequest request)
        {
            var user = _dbContext.Users.Include(u => u.UserScopes).FirstOrDefault(u => u.AccountId == request.Id);
            if (user == null)
                return BadRequest(new ErrorResponse("User not found"));

            var userScopes = user.UserScopes.ToDictionary(us => us.ScopeName, us => us, StringComparer.OrdinalIgnoreCase);
            var requestScopes = request.Scopes.ToHashSet();
            foreach (var userScope in userScopes)
            {
                if (!requestScopes.Contains(userScope.Key))
                    _dbContext.UserScopes.Remove(userScope.Value);
            }

            foreach (var requestScope in requestScopes)
            {
                if (!userScopes.ContainsKey(requestScope))
                    _dbContext.UserScopes.Add(new DbUserScope
                    {
                        UserId = user.AccountId,
                        ScopeName = requestScope
                    });
            }

            _dbContext.SaveChanges();
            return Ok();
        }
    }
}