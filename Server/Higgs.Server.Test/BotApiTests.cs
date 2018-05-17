using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Higgs.Server.Controllers;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Models.Requests.Admin;
using Higgs.Server.Models.Requests.Bot;
using Higgs.Server.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Higgs.Server.Test
{
    [TestFixture]
    public class BotApiTests : BaseControllerTests
    {
        [Test]
        public async Task TestBlankTitleInvalid()
        {
            Authenticate(
                new[] { new Claim("botId", "1") },
                Scopes.SCOPE_BOT
            );

            var request = new RegisterPostRequest
            {
                Title = "",
                ContentUrl = "http://www.google.com"
            };

            var response = await Client.PostAsync("/Bot/RegisterPost", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Title is required");
        }

        [Test]
        public async Task TestBlankContentUrlInvalid()
        {
            Authenticate(
                new[] { new Claim("botId", "1") },
                Scopes.SCOPE_BOT
            );

            var request = new RegisterPostRequest
            {
                Title = "My Title",
                ContentUrl = ""
            };

            var result = await Client.PostAsync("/Bot/RegisterPost", request);
            await result.AssertError(HttpStatusCode.BadRequest, "ContentUrl is required");
        }

        [Test]
        public async Task TestInvalidFeedbackRejected() 
        {
            Authenticate(
                new[] { new Claim("botId", "1") },
                Scopes.SCOPE_BOT
            );

            var request = new RegisterPostRequest
            {
                Title = "My Title",
                ContentUrl = "My Url",
                AllowedFeedback = new List<string> { "tp" }
            };

            var result = await Client.PostAsync("/Bot/RegisterPost", request);
            await result.AssertError(HttpStatusCode.BadRequest, "Feedback 'tp' not registered for bot");
        }

        [Test]
        public async Task TestBotCanProvideFeedbackForNewUser()
        {
            Authenticate(
                new[] { new Claim("botId", "1") },
                Scopes.SCOPE_BOT
            );

            WithDatabase(dbContext =>
            {
                dbContext.Feedbacks.Add(new Data.Models.DbFeedback()
                {
                    BotId = 1,
                    Name = "tp"
                });
                dbContext.SaveChanges();
            });

            var registerPostRequest = new RegisterPostRequest
            {
                Title = "My Title",
                ContentUrl = "My Url",
                AllowedFeedback = new List<string> { "tp" }
            };
            var result = await Client.PostAsync("/Bot/RegisterPost", registerPostRequest);
            var reportId = JsonConvert.DeserializeObject<int>(await result.Content.ReadAsStringAsync());

            var feedbackResult = await Client.PostAsync("/Bot/RegisterUserFeedback", new RegisterUserFeedbackRequest
            {
                ReportId = reportId,
                UserId = 1,
                Feedback = "tp"
            });

            feedbackResult.AssertSuccess();
        }

        [Test]
        public async Task TestBotCanProvideFeedbackForUserWithReviewerRole()
        {
            Authenticate(
                new[] { new Claim("botId", "1") },
                Scopes.SCOPE_BOT
            );

            WithDatabase(dbContext =>
            {
                dbContext.Users.Add(new Data.Models.DbUser()
                {
                    AccountId = 1,
                    UserScopes = new List<DbUserScope> { new DbUserScope {
                        ScopeName = Scopes.SCOPE_REVIEWER
                    }}
                });
                dbContext.Feedbacks.Add(new Data.Models.DbFeedback()
                {
                    BotId = 1,
                    Name = "tp"
                });
                dbContext.SaveChanges();
            });

            var registerPostRequest = new RegisterPostRequest
            {
                Title = "My Title",
                ContentUrl = "My Url",
                AllowedFeedback = new List<string> { "tp" }
            };
            var result = await Client.PostAsync("/Bot/RegisterPost", registerPostRequest);
            var reportId = JsonConvert.DeserializeObject<int>(await result.Content.ReadAsStringAsync());

            var feedbackResult = await Client.PostAsync("/Bot/RegisterUserFeedback", new RegisterUserFeedbackRequest
            {
                ReportId = reportId,
                UserId = 1,
                Feedback = "tp"
            });

            feedbackResult.AssertSuccess();
        }

        [Test]
        public async Task TestBotCantProvideFeedbackForUserWithoutReviewerRole()
        {
            Authenticate(
                new[] { new Claim("botId", "1") },
                Scopes.SCOPE_BOT
            );

            WithDatabase(dbContext =>
            {
                dbContext.Users.Add(new Data.Models.DbUser()
                {
                    AccountId = 1
                });
                dbContext.Feedbacks.Add(new Data.Models.DbFeedback()
                {
                    BotId = 1,
                    Name = "tp"
                });
                dbContext.SaveChanges();
            });

            var registerPostRequest = new RegisterPostRequest
            {
                Title = "My Title",
                ContentUrl = "My Url",
                AllowedFeedback = new List<string> { "tp" }
            };
            var result = await Client.PostAsync("/Bot/RegisterPost", registerPostRequest);
            var reportId = JsonConvert.DeserializeObject<int>(await result.Content.ReadAsStringAsync());

            var userFeedbackResult = await Client.PostAsync("/Bot/RegisterUserFeedback", new RegisterUserFeedbackRequest
            {
                ReportId = reportId,
                UserId = 1,
                Feedback = "tp"
            });

            await userFeedbackResult.AssertError(HttpStatusCode.BadRequest, "User is not authorized as a reviewer");
        }

        [Test]
        public async Task TestBotInvalidFeedback()
        {
            Authenticate(
                new[] { new Claim("botId", "1") },
                Scopes.SCOPE_BOT
            );

            WithDatabase(dbContext =>
            {
                dbContext.Feedbacks.Add(new Data.Models.DbFeedback()
                {
                    BotId = 1,
                    Name = "tp"
                });
                dbContext.SaveChanges();
            });

            var registerPostRequest = new RegisterPostRequest
            {
                Title = "My Title",
                ContentUrl = "My Url",
                AllowedFeedback = new List<string> { "tp" }
            };
            var result = await Client.PostAsync("/Bot/RegisterPost", registerPostRequest);
            var reportId = JsonConvert.DeserializeObject<int>(await result.Content.ReadAsStringAsync());

            var userFeedbackResult = await Client.PostAsync("/Bot/RegisterUserFeedback", new RegisterUserFeedbackRequest
            {
                ReportId = reportId,
                UserId = 1,
                Feedback = "fp"
            });
            await userFeedbackResult.AssertError(HttpStatusCode.BadRequest, "Feedback not allowed for report");
        }
    }
}
