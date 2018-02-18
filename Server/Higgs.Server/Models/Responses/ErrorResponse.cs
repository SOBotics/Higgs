namespace Higgs.Server.Models.Responses
{
    public class ErrorResponse
    {
	    public string Message { get; }

		public ErrorResponse(string message)
	    {
		    Message = message;
	    }	
    }
}
