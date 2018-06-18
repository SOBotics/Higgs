using System;
using System.Collections.Generic;

namespace Higgs.Server.Models.Responses.Reviewer
{
    public class ReviewerReportResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DashboardLogo { get; set; }
        public string BotName { get; set; }
        public string DashboardName { get; set; }
        public string TabTitle { get; set; }
        public string FavIcon { get; set; }
        public string BotHomePage { get; set; }
        public string ContentUrl { get; set; }
        public string ContentSite { get; set; }
        public string ContentType { get; set; }
        public int? ContentId { get; set; }
        public double? DetectionScore { get; set; }
        public List<ReviewerReportContentFragmentResponse> ContentFragments { get; set; }
        public string AuthorName { get; set; }
        public int? AuthorReputation { get; set; }
        public DateTime? ContentCreationDate { get; set; }
        public DateTime? DetectedDate { get; set; }
        public List<ReviewerReportReasonResponse> Reasons { get; set; }
        public List<ReviewerReportAllowedFeedbackResponse> AllowedFeedback { get; set; }
        public List<ReviewerReportFeedbackResponse> Feedback { get; set; }
    }

    public class ReviewerReportFeedbackResponse
    {
        public int Id { get;set; }
        public int UserId { get; set; }
        public string FeedbackName { get; set; }
        public string UserName { get; set; }
        public string Icon { get; set; }
        public string Colour { get; set; }
    }

    public class ReviewerReportContentFragmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
    }

    public class ReviewerReportReasonResponse
    {
        public int ReasonId { get; set; }
        public string Name { get; set; }
        public double? Confidence { get; set; }
        public bool Tripped { get; set; }
        public int Seen { get; set; }
    }

    public class ReviewerReportAllowedFeedbackResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
    }
}
