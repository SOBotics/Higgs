namespace Higgs.Server.Models.Responses.Admin
{
    public class ViewBotFeedbackTypesResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Colour { get; set; }

        public string Icon { get; set; }

        public bool IsActionable { get; set; }

        public bool IsEnabled { get; set; }
    }
}
