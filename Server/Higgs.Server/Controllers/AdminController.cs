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
        ///     Register a dashboard
        /// </summary>
        /// <returns>The ID of the created dashboard</returns>
        /// <response code="200">Successfully registered dashboard</response>
        [HttpPost("RegisterDashboard")]
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult RegisterDashboard([FromBody] CreateDashboardRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.BotName))
                return BadRequest(new ErrorResponse("Name is required"));
            if (string.IsNullOrWhiteSpace(request.Description))
                return BadRequest(new ErrorResponse("Description is required"));
            if (string.IsNullOrWhiteSpace(request.Secret))
                return BadRequest(new ErrorResponse("Secret is required"));
            if (_dbContext.Dashboards.Any(b => b.DashboardName == request.DashboardName))
                return BadRequest(new ErrorResponse($"Dashboard with name '{request.DashboardName}' already exists"));

            var userId = User.GetUserId();
            if (!userId.HasValue)
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            var dashboard = new DbDashboard
            {
                Scopes = new List<DbDashboardScope>(),
                Feedbacks = new List<DbFeedback>(),
                ConflictExceptions = new List<DbConflictException>()
            };

            FillDashboardDetails(dashboard, request);

            if (request.OwnerAccountId.HasValue && User.HasClaim(Scopes.SCOPE_ADMIN))
                dashboard.OwnerAccountId = request.OwnerAccountId.Value;

            _dbContext.Dashboards.Add(dashboard);
            _dbContext.DashboardScopes.Add(new DbDashboardScope
            {
                Dashboard = dashboard,
                ScopeName = Scopes.SCOPE_BOT
            });

            _dbContext.SaveChanges();
            return Json(dashboard.Id);
        }
        
        [HttpGet("Dashboard")]
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        public DashboardResponse Dashboard(int dashboardId)
        {
            var dasboard = _dbContext.Dashboards.Where(b => b.Id == dashboardId)
                .Select(b => new DashboardResponse
                {
                    Id = b.Id,
                    BotName = b.BotName,
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

            if (dasboard == null)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Bot not found");
            
            var feedbacks = _dbContext.Feedbacks.Where(f => f.DashboardId == dashboardId)
                .Select(f => new BotResponseFeedback
                {
                    Id = f.Id,
                    Name = f.Name,
                    Colour = f.Colour,
                    Icon = f.Icon,
                    IsActionable = f.IsActionable,
                    IsEnabled = f.IsEnabled
                }).ToList();
            dasboard.Feedbacks = feedbacks;

            var conflictExceptions = _dbContext.ConflictExceptions.Where(bceg => bceg.DashboardId == dashboardId)
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

            dasboard.ConflictExceptions = conflictExceptions;
            
            if (User.HasClaim(Scopes.SCOPE_ADMIN) || dasboard.OwnerAccountId == User.GetUserId())
                return dasboard;
            
            throw new HttpStatusException(HttpStatusCode.Unauthorized);
        }

        [HttpGet("Scopes")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [Authorize(Scopes.SCOPE_ADMIN)]
        public List<string> AllScopes()
        {
            return Scopes.AllScopes.Select(s => s.Key).ToList();
        }

        /// <summary>
        ///     Update a dashboard's details
        /// </summary>
        /// <response code="200">Successfully edited bot</response>
        /// <response code="400">Bot not found</response>
        [HttpPost("EditDashboard")]
        [Authorize(Scopes.SCOPE_BOT_OWNER)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public IActionResult EditDashboard([FromBody] EditDashboardRequest request)
        {
            var existingDashboard = _dbContext.Dashboards
                .Include(b => b.Scopes)
                .Include(b => b.Feedbacks)
                .Include(b => b.ConflictExceptions)
                .ThenInclude(b => b.ConflictExceptionFeedbacks)
                .FirstOrDefault(b => b.Id == request.DashboardId);

            if (existingDashboard == null)
                return BadRequest(new ErrorResponse($"Dashboard with id {request.DashboardId} not found."));

            if (!User.HasClaim(Scopes.SCOPE_ADMIN) && existingDashboard.OwnerAccountId != User.GetUserId())
                throw new HttpStatusException(HttpStatusCode.Unauthorized);

            FillDashboardDetails(existingDashboard, request);

            _dbContext.SaveChanges();

            return Ok();
        }

        private void FillDashboardDetails(DbDashboard existingDashboard, CreateDashboardRequest request)
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

            existingDashboard.BotName = request.BotName;
            existingDashboard.DashboardName = request.DashboardName;
            existingDashboard.Description = request.Description;

            if (!string.IsNullOrWhiteSpace(request.Secret))
                existingDashboard.Secret = BCrypt.Net.BCrypt.HashPassword(request.Secret);

            if (request.OwnerAccountId.HasValue && User.HasClaim(Scopes.SCOPE_ADMIN))
                existingDashboard.OwnerAccountId = request.OwnerAccountId.Value;

            existingDashboard.FavIcon = request.FavIcon;
            existingDashboard.Homepage = request.Homepage;
            existingDashboard.LogoUrl = request.LogoUrl;
            existingDashboard.TabTitle = request.TabTitle;
            existingDashboard.RequiredFeedback = request.RequiredFeedback;
            existingDashboard.RequiredFeedbackConflicted = request.RequiredFeedbackConflicted;
            
            var createdFeedbacks = new Dictionary<int, DbFeedback>();
            CollectionUpdater.UpdateCollection(
                existingDashboard.Feedbacks.ToDictionary(f => f.Id, f => f),
                request.Feedbacks.ToDictionary(f => f.Id, f => f),
                newFeedback =>
                {
                    var dbFeedbackType = new DbFeedback
                    {
                        Dashboard = existingDashboard,
                        Name = newFeedback.Name,
                        Colour = newFeedback.Colour,
                        Icon = newFeedback.Icon,
                        IsActionable = newFeedback.IsActionable,
                        IsEnabled = newFeedback.IsEnabled
                    };
                    existingDashboard.Feedbacks.Add(dbFeedbackType);
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
            
            if (existingDashboard.Feedbacks.GroupBy(f => f.Name, StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Feedback names must be unique");

            CollectionUpdater.UpdateCollection(
                existingDashboard.ConflictExceptions.ToDictionary(ce => ce.Id, ce => ce),
                request.ConflictExceptions.ToDictionary(ce => ce.Id, ce => ce),
                newConflict =>
                {
                    var dbConflictException = new DbConflictException
                    {
                        Dashboard = existingDashboard,
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

                    existingDashboard.ConflictExceptions.Add(dbConflictException);
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
        ///     Lists all users
        /// </summary>
        [HttpGet("Users")]
        [Authorize(Scopes.SCOPE_ADMIN)]
        [ProducesResponseType(typeof(List<UsersResponse>), (int)HttpStatusCode.OK)]
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
        ///     Lists user details
        /// </summary>
        [HttpGet("User")]
        [Authorize(Scopes.SCOPE_ADMIN)]
        [ProducesResponseType(typeof(UsersResponse), (int)HttpStatusCode.OK)]
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
        /// <response code="200">"Updated user details"</response>
        /// <response code="400">"User doesn't exist"</response>
        [HttpPost("User")]
        [Authorize(Scopes.SCOPE_ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
                .Include(r => r.AllowedFeedback)
                .Include(r => r.Feedbacks).ThenInclude(f => f.Feedback)
                .Include(r => r.ConflictExceptions).ThenInclude(ce => ce.ConflictExceptionFeedbacks)
                .ToList();
            foreach (var report in reports)
                ReportProcessor.ProcessReport(report);

            _dbContext.SaveChanges();
            return Ok();
        }
    }
}