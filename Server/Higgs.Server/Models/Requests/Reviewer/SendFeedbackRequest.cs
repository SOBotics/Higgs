namespace Higgs.Server.Models.Requests.Reviewer
{
    public class SendFeedbackRequest
    {
        public int ReportId { get; set; }
        public int FeedbackId { get; set; }
    }
}