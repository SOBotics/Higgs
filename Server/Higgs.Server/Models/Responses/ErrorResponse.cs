namespace Higgs.Server.Models.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}