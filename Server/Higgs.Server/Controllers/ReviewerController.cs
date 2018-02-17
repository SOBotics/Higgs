using Microsoft.AspNetCore.Mvc;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public class ReviewerController : Controller
    {
	    /// <summary>
	    /// Lists all pending reviews
	    /// </summary>
	    [HttpGet("PendingReviews")]
	    public IActionResult PendingReviews()
	    {
		    return Ok(0);
	    }

	    /// <summary>
	    /// Lists all reviews
	    /// </summary>
	    [HttpGet("AllReviews")]
	    public IActionResult AllReviews()
	    {
		    return Ok(0);
	    }

		/// <summary>
		/// Lists all pending review
		/// </summary>
		[HttpPost("SendFeedback")]
	    public IActionResult SendFeedback()
	    {
		    return Ok(0);
	    }
	}
}