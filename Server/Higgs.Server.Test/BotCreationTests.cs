using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Higgs.Server.Controllers;
using Higgs.Server.Data;
using Higgs.Server.Models.Requests.Admin;
using Higgs.Server.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Higgs.Server.Test
{
    [TestFixture]
    public class BotCreationTests : BaseControllerTests
    {
        [Test]
        public async Task TestCreateBot()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateBotRequest
            {
                Name = "asd",
                DashboardName = "da",
                Description = "ds",
                Secret = "abc",
                Feedbacks = new List<CreateBotRequestFeedback>
                {
                    new CreateBotRequestFeedback {Id = -1, Name = "tp"},
                    new CreateBotRequestFeedback {Id = -2, Name = "fp"},
                },
                ConflictExceptions = new List<CreateBotRequestExceptions>
                {
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> {-1, -2}}
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterBot", request);
            response.AssertSuccess();
            WithDatabase(dbContext =>
                {
                    var tpFeedback = dbContext.Feedbacks.FirstOrDefault(f => f.Name == "tp");
                    var fpFeedback = dbContext.Feedbacks.FirstOrDefault(f => f.Name == "fp");

                    Assert.AreEqual(2, dbContext.ConflictExceptionFeedbacks.Count());
                    Assert.NotNull(dbContext.ConflictExceptionFeedbacks.FirstOrDefault(a => a.FeedbackId == tpFeedback.Id));
                    Assert.NotNull(dbContext.ConflictExceptionFeedbacks.FirstOrDefault(a => a.FeedbackId == fpFeedback.Id));
                });
        }

        [Test]
        public async Task TestCreateBotInvalidConflict()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateBotRequest
            {
                Name = "asd",
                DashboardName = "da",
                Description = "ds",
                Secret = "abc",
                Feedbacks = new List<CreateBotRequestFeedback>
                {
                    new CreateBotRequestFeedback {Id = -1, Name = "tp"},
                    new CreateBotRequestFeedback {Id = -2, Name = "fp"},
                },
                ConflictExceptions = new List<CreateBotRequestExceptions>
                {
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> {-1, -2, -3}}
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterBot", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Invalid FeedbackId for conflict");
        }
    }
}
