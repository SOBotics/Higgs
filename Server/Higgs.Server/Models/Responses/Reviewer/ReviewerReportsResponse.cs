using System.Collections.Generic;

namespace Higgs.Server.Models.Responses.Reviewer
{
    public class ReviewerReportsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DashboardName { get; set; }
        public double? DetectionScore { get; set; }
        public List<ReviewerReportFeedbackResponse> Feedback { get; set; }
    }
}
