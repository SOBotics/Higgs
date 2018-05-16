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
                new[] {new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, DBExtensions.RobAccountId.ToString())},
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
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            WithDatabase(dbContext =>
                {
                    Assert.AreEqual(0, dbContext.ConflictExceptionFeedbacks.Count(a => a.FeedbackId < 0), "Expected ConflictExceptionFeedback IDs to be updated to newly created feedback");
                });
        }
    }
}
