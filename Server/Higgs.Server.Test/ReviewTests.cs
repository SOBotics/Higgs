using System;
using System.Collections.Generic;
using Higgs.Server.Data.Models;
using Higgs.Server.Utilities;
using NUnit.Framework;

namespace Higgs.Server.Test
{
    [TestFixture]
    public class ReviewTests : BaseControllerTests
    {
        [Test]
        public void TestNewReport()
        {
            var report = new DbReport
            {
                RequiredFeedback = 2,
                Feedbacks = new List<DbReportFeedback>()
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(true, report.RequiresReview);
            Assert.AreEqual(false, report.Conflicted);
        }

        [Test]
        public void TestSimpleReview()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true }}
                },
                RequiredFeedback = 2
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(true, report.RequiresReview);
            Assert.AreEqual(false, report.Conflicted);
        }

        [Test]
        public void TestFinishedReview()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true } },
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true } }
                },
                RequiredFeedback = 2
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(false, report.RequiresReview);
            Assert.AreEqual(false, report.Conflicted);
        }

        [Test]
        public void TestReviewsCompletedNotActionable()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true } },
                    new DbReportFeedback { FeedbackId = 2, Feedback = new DbFeedback { IsActionable = false } },
                },
                RequiredFeedback = 2
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(true, report.RequiresReview);
            Assert.AreEqual(false, report.Conflicted);
        }

        [Test]
        public void TestSimpleConflict()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 2, Feedback = new DbFeedback { IsActionable = true }}
                },
                ConflictExceptions = new List<DbConflictException>(),
                RequiredFeedback = 2,
                RequiredFeedbackConflicted = 4
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(true, report.RequiresReview);
            Assert.AreEqual(true, report.Conflicted);
        }
        
        [Test]
        public void TestConflictBumpsRequiredReviews()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 2, Feedback = new DbFeedback { IsActionable = true }}
                },
                ConflictExceptions = new List<DbConflictException>(),
                RequiredFeedback = 2,
                RequiredFeedbackConflicted = 4
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(true, report.RequiresReview);
            Assert.AreEqual(true, report.Conflicted);
        }

        [Test]
        public void TestConflictedReviewCompleted()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 2, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 2, Feedback = new DbFeedback { IsActionable = true }}
                },
                ConflictExceptions = new List<DbConflictException>(),
                RequiredFeedback = 2,
                RequiredFeedbackConflicted = 4
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(false, report.RequiresReview);
            Assert.AreEqual(true, report.Conflicted);
        }

        [Test]
        public void TestSimpleConflictException()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback {FeedbackId = 1, Feedback = new DbFeedback {IsActionable = true}},
                    new DbReportFeedback {FeedbackId = 2, Feedback = new DbFeedback {IsActionable = true}}
                },
                ConflictExceptions = new List<DbConflictException>
                {
                    new DbConflictException
                    {
                        IsConflict = false,
                        ConflictExceptionFeedbacks = new List<DbConflictExceptionFeedback>
                        {
                            new DbConflictExceptionFeedback {FeedbackId = 1},
                            new DbConflictExceptionFeedback {FeedbackId = 2},
                        }
                    }
                },
                RequiredFeedback = 2,
                RequiredFeedbackConflicted = 4
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(false, report.RequiresReview);
            Assert.AreEqual(false, report.Conflicted);
        }

        [Test]
        public void TestConflictMoreReviewsRequired()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 2, Feedback = new DbFeedback { IsActionable = true }},
                    new DbReportFeedback { FeedbackId = 2, Feedback = new DbFeedback { IsActionable = true }}
                },
                ConflictExceptions = new List<DbConflictException>
                {
                    new DbConflictException
                    {
                        IsConflict = true,
                        RequiredFeedback = 5,
                        ConflictExceptionFeedbacks = new List<DbConflictExceptionFeedback>
                        {
                            new DbConflictExceptionFeedback {FeedbackId = 1},
                            new DbConflictExceptionFeedback {FeedbackId = 2},
                        }
                    }
                },
                RequiredFeedback = 2,
                RequiredFeedbackConflicted = 4
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(true, report.RequiresReview);
            Assert.AreEqual(true, report.Conflicted);
        }

        [Test]
        public void TestInvalidatedNonConflicting()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, InvalidatedDate = DateTime.UtcNow, Feedback = new DbFeedback { IsActionable = true } },
                    new DbReportFeedback { FeedbackId = 1, Feedback = new DbFeedback { IsActionable = true } }
                },
                RequiredFeedback = 2
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(true, report.RequiresReview);
            Assert.AreEqual(false, report.Conflicted);
        }

        [Test]
        public void TestInvalidatedConflicting()
        {
            var report = new DbReport
            {
                Feedbacks = new List<DbReportFeedback>
                {
                    new DbReportFeedback { FeedbackId = 1, InvalidatedDate = DateTime.UtcNow, Feedback = new DbFeedback { IsActionable = true } },
                    new DbReportFeedback { FeedbackId = 2, Feedback = new DbFeedback { IsActionable = true } }
                },
                RequiredFeedback = 2
            };
            ReportProcessor.ProcessReport(report);
            Assert.AreEqual(true, report.RequiresReview);
            Assert.AreEqual(false, report.Conflicted);
        }

    }
}
