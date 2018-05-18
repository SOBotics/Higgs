using System.Linq;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Higgs.Server.Utilities
{
    public static class ReportProcessor
    {
        public static void ProcessReport(this HiggsDbContext dbContext, int reportId)
        {
            var report = dbContext.Reports.Where(r => r.Id == reportId)
                .Include(r => r.Feedbacks).ThenInclude(f => f.Feedback)
                .Include(r => r.ConflictExceptions).ThenInclude(ce => ce.ConflictExceptionFeedbacks)
                .FirstOrDefault();

            if (report != null)
                ProcessReport(report);
        }

        /// <summary>
        /// Processes reports and mark them as requiring review, or conflicted
        /// </summary>
        /// <param name="report"></param>
        public static void ProcessReport(DbReport report)
        {
            var distinctFeedbackIds = report.Feedbacks.Where(f => f.InvalidatedDate == null && f.Feedback.IsActionable).Select(f => f.FeedbackId).Distinct().ToList();
            var isConflicted = false;
            var actualRequiredReviews = report.RequiredFeedback;

            if (distinctFeedbackIds.Count > 1)
            {
                var feedbackPairs = ConflictHelper.CreateFeedbackPairs(distinctFeedbackIds).ToList();
                var conflictingPairs = report.ConflictExceptions.Where(ce => ce.IsConflict).Select(ce => new
                {
                    ConflictException = ce,
                    Pairs = ConflictHelper.CreateFeedbackPairs(ce.ConflictExceptionFeedbacks.Select(cef => cef.FeedbackId))
                });

                foreach (var conflictingPair in conflictingPairs)
                {
                    if (conflictingPair.Pairs.Intersect(feedbackPairs).Any())
                    {
                        isConflicted = true;
                        if (conflictingPair.ConflictException.RequiredFeedback.HasValue)
                            actualRequiredReviews = conflictingPair.ConflictException.RequiredFeedback.Value;

                        break;
                    }
                }

                if (!isConflicted)
                {
                    var nonConflictingPairs = report.ConflictExceptions.Where(ce => !ce.IsConflict).SelectMany(ce =>
                        ConflictHelper.CreateFeedbackPairs(ce.ConflictExceptionFeedbacks.Select(cef => cef.FeedbackId)));

                    if (!nonConflictingPairs.Intersect(feedbackPairs).Any())
                    {
                        isConflicted = true;
                        actualRequiredReviews = report.RequiredFeedbackConflicted;
                    }
                }
            }

            report.Conflicted = isConflicted;
            report.RequiresReview = report.Feedbacks.Count(f => f.InvalidatedDate == null && f.Feedback.IsActionable) < actualRequiredReviews;
        }
    }
}
