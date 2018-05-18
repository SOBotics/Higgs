using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Higgs.Server.Utilities
{
    public class ConflictHelper
    {
        public static void AssertUniqueConflictFeedbacks(IEnumerable<List<int>> conflictFeedbacks)
        {
            var conflictFeedbackList = conflictFeedbacks.ToList();

            if (conflictFeedbackList.Any(cf => cf.Count <= 1))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Conflicts must contain at least two feedback types");

            var groups = conflictFeedbackList.SelectMany(CreateFeedbackPairs).GroupBy(g => g).ToList();

            if (groups.Any(g => g.Count() > 1))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "A pair of feedback ids cannot appear in two different conflicts");
        }

        public static IEnumerable<FeedbackPair<T>> CreateFeedbackPairs<T>(IList<T> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = i + 1; j < list.Count; j++)
                {
                    yield return new FeedbackPair<T>(list[i], list[j]);
                }
            }
        }

        public struct FeedbackPair<T>
        {
            public T FeedbackOne { get; }
            public T FeedbackTwo { get; }

            public FeedbackPair(T feedbackOne, T feedbackTwo)
            {
                FeedbackOne = feedbackOne;
                FeedbackTwo = feedbackTwo;
            }
        }
    }
}
