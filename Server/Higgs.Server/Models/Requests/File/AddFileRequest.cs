namespace Higgs.Server.Models.Requests.File
{
    public class AddFileRequest
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Contents { get; set; }
    }
}
