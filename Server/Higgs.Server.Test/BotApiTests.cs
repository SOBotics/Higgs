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
    }
}
