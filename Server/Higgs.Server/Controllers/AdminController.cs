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
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(List<BotsResponse>), Description = "View bot details")]
        public List<BotsResponse> Bots()
        {
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
                BotScopes = new List<DbBotScope>(),
                Feedbacks = new List<DbFeedback>(),
                ConflictExceptions = new List<DbConflictException>()
            };

            FillBotDetails(bot, request);

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
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        public BotResponse Bot(int botId)
        {
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
                    OwnerAccountId = b.OwnerAccountId,
                    RequiredFeedback = b.RequiredFeedback,
                    RequiredFeedbackConflicted = b.RequiredFeedbackConflicted
                }).FirstOrDefault();

            if (bot == null)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Bot not found");
            
            var feedbacks = _dbContext.Feedbacks.Where(f => f.BotId == botId)
                .Select(f => new BotResponseFeedback
                {
                    Id = f.Id,
                    Name = f.Name,
                    Colour = f.Colour,
                    Icon = f.Icon,
                    IsActionable = f.IsActionable,
                    IsEnabled = f.IsEnabled
                }).ToList();
            bot.Feedbacks = feedbacks;

            var conflictExceptions = _dbContext.ConflictExceptions.Where(bceg => bceg.BotId == botId)
                .Include(conflictExceptionGroup => conflictExceptionGroup.ConflictExceptionFeedbacks)
                .ToList()
                .Select(conflictExceptionGroup => new BotResponseConflictExceptions
                {
                    Id = conflictExceptionGroup.Id,
                    IsConflict = conflictExceptionGroup.IsConflict,
                    RequiresAdmin = conflictExceptionGroup.RequiresAdmin,
                    RequiredFeedback = conflictExceptionGroup.RequiredFeedback,
                    BotResponseConflictFeedbacks = conflictExceptionGroup.ConflictExceptionFeedbacks.Select(gg => gg.FeedbackId).ToList()
                }).ToList();

            bot.ConflictExceptions = conflictExceptions;
            
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
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Successfully edited bot")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(ErrorResponse), Description = "Bot not found")]
        public IActionResult EditBot([FromBody] EditBotRequest request)
        {
            var existingBot = _dbContext.Bots
                .Include(b => b.BotScopes)
                .Include(b => b.Feedbacks)
                .Include(b => b.ConflictExceptions)
                .ThenInclude(b => b.ConflictExceptionFeedbacks)
                .FirstOrDefault(b => b.Id == request.BotId);

            if (existingBot == null)
                return BadRequest(new ErrorResponse($"Bot with id {request.BotId} not found."));

            if (!User.HasClaim(Scopes.SCOPE_ADMIN) && existingBot.OwnerAccountId != User.GetUserId())
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            FillBotDetails(existingBot, request);

            _dbContext.SaveChanges();

            return Ok();
        }

        private void FillBotDetails(DbBot existingBot, CreateBotRequest request)
        {
            if (request.Feedbacks == null)
                request.Feedbacks = new List<CreateBotRequestFeedback>();
            if (request.Feedbacks.GroupBy(f => f.Id).Any(g => g.Count() > 1))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Duplicate feedback ids");

            if (request.ConflictExceptions == null)
                request.ConflictExceptions = new List<CreateBotRequestExceptions>();
            if (request.ConflictExceptions.GroupBy(f => f.Id).Any(g => g.Count() > 1))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Duplicate conflict exception ids");

            ConflictHelper.AssertUniqueConflictFeedbacks(request.ConflictExceptions.Select(c => c.BotResponseConflictFeedbacks));

            existingBot.Name = request.Name;
            existingBot.DashboardName = request.DashboardName;
            existingBot.Description = request.Description;

            if (!string.IsNullOrWhiteSpace(request.Secret))
                existingBot.Secret = BCrypt.Net.BCrypt.HashPassword(request.Secret);

            if (request.OwnerAccountId.HasValue && User.HasClaim(Scopes.SCOPE_ADMIN))
                existingBot.OwnerAccountId = request.OwnerAccountId.Value;

            existingBot.FavIcon = request.FavIcon;
            existingBot.Homepage = request.Homepage;
            existingBot.LogoUrl = request.LogoUrl;
            existingBot.TabTitle = request.TabTitle;
            existingBot.RequiredFeedback = request.RequiredFeedback;
            existingBot.RequiredFeedbackConflicted = request.RequiredFeedbackConflicted;
            
            var createdFeedbacks = new Dictionary<int, DbFeedback>();
            CollectionUpdater.UpdateCollection(
                existingBot.Feedbacks.ToDictionary(f => f.Id, f => f),
                request.Feedbacks.ToDictionary(f => f.Id, f => f),
                newFeedback =>
                {
                    var dbFeedbackType = new DbFeedback
                    {
                        Bot = existingBot,
                        Name = newFeedback.Name,
                        Colour = newFeedback.Colour,
                        Icon = newFeedback.Icon,
                        IsActionable = newFeedback.IsActionable,
                        IsEnabled = newFeedback.IsEnabled
                    };
                    existingBot.Feedbacks.Add(dbFeedbackType);
                    _dbContext.Feedbacks.Add(dbFeedbackType);

                    createdFeedbacks.Add(newFeedback.Id, dbFeedbackType);
                },
                (existingFeedback, newFeedback) =>
                {
                    existingFeedback.Name = newFeedback.Name;
                    existingFeedback.Colour = newFeedback.Colour;
                    existingFeedback.Icon = newFeedback.Icon;
                    existingFeedback.IsActionable = newFeedback.IsActionable;
                    existingFeedback.IsEnabled = newFeedback.IsEnabled;
                },
                existingFeedback => { }
            );
            
            if (existingBot.Feedbacks.GroupBy(f => f.Name, StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Feedback names must be unique");

            CollectionUpdater.UpdateCollection(
                existingBot.ConflictExceptions.ToDictionary(ce => ce.Id, ce => ce),
                request.ConflictExceptions.ToDictionary(ce => ce.Id, ce => ce),
                newConflict =>
                {
                    var dbConflictException = new DbConflictException
                    {
                        Bot = existingBot,
                        IsConflict = newConflict.IsConflict,
                        RequiresAdmin = newConflict.RequiresAdmin,
                        RequiredFeedback = newConflict.RequiredFeedback
                    };

                    foreach (var conflictFeedbackId in newConflict.BotResponseConflictFeedbacks)
                    {
                        var newConflictException = new DbConflictExceptionFeedback
                        {
                            ConflictException = dbConflictException
                        };

                        if (conflictFeedbackId < 0)
                        {
                            if (createdFeedbacks.ContainsKey(conflictFeedbackId))
                                newConflictException.Feedback = createdFeedbacks[conflictFeedbackId];
                            else
                                throw new HttpStatusException(HttpStatusCode.BadRequest, "Invalid FeedbackId for conflict");
                        }
                        else
                        {
                            newConflictException.FeedbackId = conflictFeedbackId;
                        }
                        _dbContext.ConflictExceptionFeedbacks.Add(newConflictException);
                    }

                    existingBot.ConflictExceptions.Add(dbConflictException);
                    _dbContext.ConflictExceptions.Add(dbConflictException);
                },
                (existingConflict, newConflict) =>
                {
                    existingConflict.IsConflict = newConflict.IsConflict;
                    existingConflict.RequiresAdmin = newConflict.RequiresAdmin;
                    existingConflict.RequiredFeedback = newConflict.RequiredFeedback;

                    CollectionUpdater.UpdateCollection(
                        existingConflict.ConflictExceptionFeedbacks.ToDictionary(d => d.FeedbackId, d => d),
                        newConflict.BotResponseConflictFeedbacks.ToDictionary(d => d, d => d),
                        newConflictFeedbackId =>
                        {
                            var newConflictException = new DbConflictExceptionFeedback
                            {
                                ConflictException = existingConflict
                            };
                            if (newConflictFeedbackId < 0)
                            {
                                if (createdFeedbacks.ContainsKey(newConflictFeedbackId))
                                    newConflictException.Feedback = createdFeedbacks[newConflictFeedbackId];
                                else
                                    throw new HttpStatusException(HttpStatusCode.BadRequest, "Invalid FeedbackId for conflict");
                            }
                            else
                            {
                                newConflictException.FeedbackId = newConflictFeedbackId;
                            }
                            _dbContext.ConflictExceptionFeedbacks.Add(newConflictException);
                        },
                        (existingConflictFeedback, newConflictFeedback) => { },
                        existingConflictFeedback =>
                        {
                            _dbContext.ConflictExceptionFeedbacks.Remove(existingConflictFeedback);
                        }
                    );
                },
                existingConflict =>
                {
                    _dbContext.ConflictExceptions.Remove(existingConflict);
                }
            );
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
            if (request.Scopes.GroupBy(s => s, StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Duplicate scopes");

            var user = _dbContext.Users.Include(u => u.UserScopes).FirstOrDefault(u => u.AccountId == request.Id);
            if (user == null)
                return BadRequest(new ErrorResponse("User not found"));

            user.Name = request.Name;

            CollectionUpdater.UpdateCollection(
                user.UserScopes.ToDictionary(us => us.ScopeName, us => us, StringComparer.OrdinalIgnoreCase),
                request.Scopes.ToDictionary(s => s, s => s, StringComparer.OrdinalIgnoreCase),
                newScope =>
                {
                    _dbContext.UserScopes.Add(new DbUserScope
                    {
                        UserId = user.AccountId,
                        ScopeName = newScope
                    });
                },
                (existingScope, newScope) => { },
                existingScope =>
                {
                    _dbContext.UserScopes.Remove(existingScope);
                }
            );

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("ForceProcessReports")]
        [Authorize(Scopes.SCOPE_ADMIN)]
        public IActionResult ForceProcessReports()
        {
            var reports = _dbContext.Reports
                .Include(r => r.Feedbacks).ThenInclude(f => f.Feedback)
                .Include(r => r.ConflictExceptions)
                .ToList();
            foreach (var report in reports)
                ReportProcessor.ProcessReport(report);

            _dbContext.SaveChanges();
            return Ok();
        }
    }
}