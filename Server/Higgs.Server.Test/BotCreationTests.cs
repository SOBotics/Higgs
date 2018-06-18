using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Higgs.Server.Models.Requests.Admin;
using Higgs.Server.Utilities;
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

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
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

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
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
        public async Task TestCreateBotConflictWithMismatchedFeedbackId()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
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

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Invalid FeedbackId for conflict");
        }

        [Test]
        public async Task TestBotCreateDuplicateFeedbackNames()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
                DashboardName = "da",
                Description = "ds",
                Secret = "abc",
                Feedbacks = new List<CreateBotRequestFeedback>
                {
                    new CreateBotRequestFeedback {Id = -1, Name = "tp"},
                    new CreateBotRequestFeedback {Id = -2, Name = "tp"},
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Feedback names must be unique");
        }


        [Test]
        public async Task TestBotCreateDuplicateFeedbackIds()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
                DashboardName = "da",
                Description = "ds",
                Secret = "abc",
                Feedbacks = new List<CreateBotRequestFeedback>
                {
                    new CreateBotRequestFeedback {Id = -1, Name = "tp"},
                    new CreateBotRequestFeedback {Id = -1, Name = "fp"},
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Duplicate feedback ids");
        }

        [Test]
        public async Task TestBotCreateDuplicateConflictIds()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
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
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> {-1 }},
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> {-2 }}
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Duplicate conflict exception ids");
        }

        [Test]
        public async Task TestInvalidConflictFeedback()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
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
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> { -1, -2 }},
                    new CreateBotRequestExceptions {Id = -2, BotResponseConflictFeedbacks = new List<int> { -1, -2 }}
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "A pair of feedback ids cannot appear in two different conflicts");
        }

        [Test]
        public async Task TestInvalidConflictFeedback2()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
                DashboardName = "da",
                Description = "ds",
                Secret = "abc",
                Feedbacks = new List<CreateBotRequestFeedback>
                {
                    new CreateBotRequestFeedback {Id = -1, Name = "tp"},
                    new CreateBotRequestFeedback {Id = -2, Name = "fp"},
                    new CreateBotRequestFeedback {Id = -3, Name = "nc"},
                },
                ConflictExceptions = new List<CreateBotRequestExceptions>
                {
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> { -1, -2 }},
                    new CreateBotRequestExceptions {Id = -2, BotResponseConflictFeedbacks = new List<int> { -1, -2, -3 }}
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "A pair of feedback ids cannot appear in two different conflicts");
        }

        [Test]
        public async Task TestFeedbackPresentTwoConflictsValid()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
                DashboardName = "da",
                Description = "ds",
                Secret = "abc",
                Feedbacks = new List<CreateBotRequestFeedback>
                {
                    new CreateBotRequestFeedback {Id = -1, Name = "tp"},
                    new CreateBotRequestFeedback {Id = -2, Name = "fp"},
                    new CreateBotRequestFeedback {Id = -3, Name = "nc"},
                },
                ConflictExceptions = new List<CreateBotRequestExceptions>
                {
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> { -1, -2 }},
                    new CreateBotRequestExceptions {Id = -2, BotResponseConflictFeedbacks = new List<int> { -2, -3 }}
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            response.AssertSuccess();
        }

        [Test]
        public async Task TestFeedbackPresentTwoConflictsValid2()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
                DashboardName = "da",
                Description = "ds",
                Secret = "abc",
                Feedbacks = new List<CreateBotRequestFeedback>
                {
                    new CreateBotRequestFeedback {Id = -1, Name = "tp"},
                    new CreateBotRequestFeedback {Id = -2, Name = "fp"},
                    new CreateBotRequestFeedback {Id = -3, Name = "nc"},
                    new CreateBotRequestFeedback {Id = -4, Name = "sk"},
                },
                ConflictExceptions = new List<CreateBotRequestExceptions>
                {
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> { -1, -2 }},
                    new CreateBotRequestExceptions {Id = -2, BotResponseConflictFeedbacks = new List<int> { -2, -3 }},
                    new CreateBotRequestExceptions {Id = -3, BotResponseConflictFeedbacks = new List<int> { -2, -4 }}
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            response.AssertSuccess();
        }

        [Test]
        public async Task TestConflictMustContainAtLeastTwoFeedbacks()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
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
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> { }},
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Conflicts must contain at least two feedback types");
        }


        [Test]
        public async Task TestConflictMustContainAtLeastTwoFeedbacks2()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
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
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> { -1 }},
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Conflicts must contain at least two feedback types");
        }

        [Test]
        public async Task TestConflictMustContainAtLeastTwoFeedbacks3()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
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
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> { -1, -2 }},
                    new CreateBotRequestExceptions {Id = -2, BotResponseConflictFeedbacks = new List<int> { }},
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Conflicts must contain at least two feedback types");
        }


        [Test]
        public async Task TestConflictMustContainAtLeastTwoFeedbacks4()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_BOT_OWNER
            );

            var request = new CreateDashboardRequest
            {
                BotName = "asd",
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
                    new CreateBotRequestExceptions {Id = -1, BotResponseConflictFeedbacks = new List<int> { -1, -2 }},
                    new CreateBotRequestExceptions {Id = -2, BotResponseConflictFeedbacks = new List<int> { -1 }},
                }
            };

            var response = await Client.PostAsync("/Admin/RegisterDashboard", request);
            await response.AssertError(HttpStatusCode.BadRequest, "Conflicts must contain at least two feedback types");
        }
    }
}
