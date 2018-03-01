using System;
using System.Collections.Generic;

namespace Higgs.Server.Models.Responses.Reviewer
{
    public class ReportResponse
    {
        public string Title { get; set; }
        public string ContentUrl { get; set; }
        public string ContentSite { get; set; }
        public string ContentType { get; set; }
        public int? ContentId { get; set; }
        public double? DetectionScore { get; set; }
        public List<ReportContentFragmentResponse> ContentFragments { get; set; }
        public string AuthorName { get; set; }
        public int? AuthorReputation { get; set; }
        public DateTime? ContentCreationDate { get; set; }
        public DateTime? DetectedDate { get; set; }
        public List<ReportReasonResponse> Reasons { get; set; }
        public List<ReportAllowedFeedbackResponse> AllowedFeedback { get; set; }
    }

    public class ReportContentFragmentResponse
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
    }

    public class ReportReasonResponse
    {
        public int ReasonId { get; set; }
        public double? Confidence { get; set; }
        public string Name { get; set; }
        public int Seen { get; set; }
    }

    public class ReportAllowedFeedbackResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
    }
}
