using System;
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
using Higgs.Server.Models.Requests.Reviewer;
using Higgs.Server.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Higgs.Server.Test
{
    [TestFixture]
    public class FeedbackTests : BaseControllerTests
    {
        [Test]
        public async Task TestFeedbackAndChange()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_REVIEWER
            );

            WithDatabase(dbContext =>
            {
                dbContext.Feedbacks.Add(new DbFeedback { Id = 1, Name = "tp", IsEnabled = true });
                dbContext.Feedbacks.Add(new DbFeedback { Id = 2, Name = "fp", IsEnabled = true });
                dbContext.Reports.Add(new DbReport
                {
                    Id = 1,
                    Title = "Test",
                    ContentUrl = "Test",
                    AllowedFeedback = new List<DbReportAllowedFeedback> {
                        new DbReportAllowedFeedback {
                            FeedbackId = 1
                        },
                        new DbReportAllowedFeedback {
                            FeedbackId = 2
                        }
                    }
                });
                dbContext.SaveChanges();
            });

            var intialFeedbackRequest = await Client.PostAsync("/Reviewer/SendFeedback", new SendFeedbackRequest
            {
                ReportId = 1,
                FeedbackId = 1,
            });
            Assert.AreEqual(HttpStatusCode.OK, intialFeedbackRequest.StatusCode);
            WithDatabase(dbContext =>
            {
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count());
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count(rf => rf.ReportId == 1 && rf.FeedbackId == 1 && rf.InvalidatedDate == null));
            });

            var changeFeedbackRequest = await Client.PostAsync("/Reviewer/SendFeedback", new SendFeedbackRequest
            {
                ReportId = 1,
                FeedbackId = 2,
            });
            Assert.AreEqual(HttpStatusCode.OK, changeFeedbackRequest.StatusCode);
            WithDatabase(dbContext =>
            {
                Assert.AreEqual(2, dbContext.ReportFeedbacks.Count());

                Console.WriteLine(JsonConvert.SerializeObject(dbContext.ReportFeedbacks.ToList()));

                var invalidatedFeedback = dbContext.ReportFeedbacks.FirstOrDefault(rf => rf.ReportId == 1 && rf.FeedbackId == 1);
                Assert.NotNull(invalidatedFeedback);
                Assert.AreEqual(1, invalidatedFeedback.InvalidatedByUserId);
                Assert.AreEqual("Feedback changed", invalidatedFeedback.InvalidationReason);
                Assert.NotNull(invalidatedFeedback.InvalidatedDate);

                var newFeedback = dbContext.ReportFeedbacks.FirstOrDefault(rf => rf.ReportId == 1 && rf.FeedbackId == 2);
                Assert.NotNull(newFeedback);
                Assert.IsNull(newFeedback.InvalidatedByUserId);
                Assert.IsNull(newFeedback.InvalidationReason);
                Assert.IsNull(newFeedback.InvalidatedDate);
            });
        }

        [Test]
        public async Task TestFeedbackAndClear()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_REVIEWER
            );

            WithDatabase(dbContext =>
            {
                dbContext.Feedbacks.Add(new DbFeedback { Id = 1, Name = "tp", IsEnabled = true });
                dbContext.Feedbacks.Add(new DbFeedback { Id = 2, Name = "fp", IsEnabled = true });
                dbContext.Reports.Add(new DbReport
                {
                    Id = 1,
                    Title = "Test",
                    ContentUrl = "Test",
                    AllowedFeedback = new List<DbReportAllowedFeedback> {
                        new DbReportAllowedFeedback {
                            FeedbackId = 1
                        },
                        new DbReportAllowedFeedback {
                            FeedbackId = 2
                        }
                    }
                });
                dbContext.SaveChanges();
            });

            var sendFeedbackRequest = await Client.PostAsync("/Reviewer/SendFeedback", new SendFeedbackRequest
            {
                ReportId = 1,
                FeedbackId = 1,
            });
            Assert.AreEqual(HttpStatusCode.OK, sendFeedbackRequest.StatusCode);
            WithDatabase(dbContext =>
            {
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count());
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count(rf => rf.ReportId == 1 && rf.FeedbackId == 1 && rf.InvalidatedDate == null));
            });

            var clearFeedbackRequest = await Client.PostAsync("/Reviewer/ClearFeedback", new ClearFeedbackRequest
            {
                FeedbackId = 1
            });
            Assert.AreEqual(HttpStatusCode.OK, clearFeedbackRequest.StatusCode);
            WithDatabase(dbContext =>
            {
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count());

                var invalidatedFeedback = dbContext.ReportFeedbacks.FirstOrDefault(rf => rf.ReportId == 1 && rf.FeedbackId == 1);
                Assert.NotNull(invalidatedFeedback);
                Assert.AreEqual(1, invalidatedFeedback.InvalidatedByUserId);
                Assert.AreEqual("Feedback cleared", invalidatedFeedback.InvalidationReason);
                Assert.NotNull(invalidatedFeedback.InvalidatedDate);
            });
        }

        [Test]
        public async Task UserCantClearOtherFeedback()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_REVIEWER
            );

            WithDatabase(dbContext =>
            {
                dbContext.Feedbacks.Add(new DbFeedback { Id = 1, Name = "tp", IsEnabled = true });
                dbContext.Feedbacks.Add(new DbFeedback { Id = 2, Name = "fp", IsEnabled = true });
                dbContext.Reports.Add(new DbReport
                {
                    Id = 1,
                    Title = "Test",
                    ContentUrl = "Test",
                    AllowedFeedback = new List<DbReportAllowedFeedback> {
                        new DbReportAllowedFeedback {
                            FeedbackId = 1
                        },
                        new DbReportAllowedFeedback {
                            FeedbackId = 2
                        }
                    }
                });
                dbContext.SaveChanges();
            });

            var sendFeedbackRequest = await Client.PostAsync("/Reviewer/SendFeedback", new SendFeedbackRequest
            {
                ReportId = 1,
                FeedbackId = 1,
            });
            Assert.AreEqual(HttpStatusCode.OK, sendFeedbackRequest.StatusCode);
            WithDatabase(dbContext =>
            {
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count());
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count(rf => rf.ReportId == 1 && rf.FeedbackId == 1 && rf.InvalidatedDate == null));
            });

            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 2.ToString()) },
                Scopes.SCOPE_REVIEWER
            );

            var exception = Assert.ThrowsAsync<HttpStatusException>(async () => await Client.PostAsync("/Reviewer/ClearFeedback", new ClearFeedbackRequest
            {
                FeedbackId = 1
            }));
            Assert.AreEqual(HttpStatusCode.Unauthorized, exception.StatusCode);
        }

        [Test]
        public async Task AdminClearFeedback()
        {
            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 1.ToString()) },
                Scopes.SCOPE_REVIEWER
            );

            WithDatabase(dbContext =>
            {
                dbContext.Feedbacks.Add(new DbFeedback { Id = 1, Name = "tp", IsEnabled = true });
                dbContext.Feedbacks.Add(new DbFeedback { Id = 2, Name = "fp", IsEnabled = true });
                dbContext.Reports.Add(new DbReport
                {
                    Id = 1,
                    Title = "Test",
                    ContentUrl = "Test",
                    AllowedFeedback = new List<DbReportAllowedFeedback> {
                        new DbReportAllowedFeedback {
                            FeedbackId = 1
                        },
                        new DbReportAllowedFeedback {
                            FeedbackId = 2
                        }
                    }
                });
                dbContext.SaveChanges();
            });

            var sendFeedbackRequest = await Client.PostAsync("/Reviewer/SendFeedback", new SendFeedbackRequest
            {
                ReportId = 1,
                FeedbackId = 1,
            });
            Assert.AreEqual(HttpStatusCode.OK, sendFeedbackRequest.StatusCode);
            WithDatabase(dbContext =>
            {
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count());
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count(rf => rf.ReportId == 1 && rf.FeedbackId == 1 && rf.InvalidatedDate == null));
            });

            Authenticate(
                new[] { new Claim(SecurityUtils.ACCOUNT_ID_CLAIM, 2.ToString()) },
                Scopes.SCOPE_ADMIN, Scopes.SCOPE_REVIEWER
            );

            var clearFeedbackRequest = await Client.PostAsync("/Reviewer/ClearFeedback", new ClearFeedbackRequest
            {
                FeedbackId = 1
            });
            Assert.AreEqual(HttpStatusCode.OK, clearFeedbackRequest.StatusCode);
            WithDatabase(dbContext =>
            {
                Assert.AreEqual(1, dbContext.ReportFeedbacks.Count());

                var invalidatedFeedback = dbContext.ReportFeedbacks.FirstOrDefault(rf => rf.ReportId == 1 && rf.FeedbackId == 1);
                Assert.NotNull(invalidatedFeedback);
                Assert.AreEqual(2, invalidatedFeedback.InvalidatedByUserId);
                Assert.AreEqual("Feedback cleared", invalidatedFeedback.InvalidationReason);
                Assert.NotNull(invalidatedFeedback.InvalidatedDate);
            });
        }
    }
}
