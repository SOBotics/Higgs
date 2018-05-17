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
        public void TestBlankTitleInvalid()
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

            var exception = Assert.ThrowsAsync<HttpStatusException>(async () =>
            {
                await Client.PostAsync("/Bot/RegisterPost", request);
            });
            Assert.AreEqual("Title is required", exception.Message);
        }

        [Test]
        public void TestBlankContentUrlInvalid()
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

            var exception = Assert.ThrowsAsync<HttpStatusException>(async () =>
            {
                await Client.PostAsync("/Bot/RegisterPost", request);
            });
            Assert.AreEqual("ContentUrl is required", exception.Message);
        }

        [Test]
        public void TestInvalidFeedbackRejected() 
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

            var exception = Assert.ThrowsAsync<HttpStatusException>(async () =>
            {
                await Client.PostAsync("/Bot/RegisterPost", request);
            });
            Assert.AreEqual("Feedback 'tp' not registered for bot", exception.Message);
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

            Assert.AreEqual(HttpStatusCode.OK, feedbackResult.StatusCode);
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

            Assert.AreEqual(HttpStatusCode.OK, feedbackResult.StatusCode);
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

            var exception = Assert.ThrowsAsync<HttpStatusException>(async () => await Client.PostAsync("/Bot/RegisterUserFeedback", new RegisterUserFeedbackRequest
            {
                ReportId = reportId,
                UserId = 1,
                Feedback = "tp"
            }));

            Assert.AreEqual("User is not authorized as a reviewer", exception.Message);
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

            var exception = Assert.ThrowsAsync<HttpStatusException>(async () => await Client.PostAsync("/Bot/RegisterUserFeedback", new RegisterUserFeedbackRequest
            {
                ReportId = reportId,
                UserId = 1,
                Feedback = "fp"
            }));

            Assert.AreEqual("Feedback not allowed for report", exception.Message);
        }
    }
}
