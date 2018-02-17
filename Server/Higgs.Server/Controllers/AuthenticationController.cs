using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Higgs.Server.Controllers
{
	[Route("[controller]")]
	public class AuthenticationController : Controller
	{
		private class LoginState
		{
			public string RedirectURI { get; set; }
			public string Scope { get; set; }
		}

		private const string CLIENT_ID = "11853";
		private const string CLIENT_SECRET = "";
		private const string KEY = "Fq3XHmFjAQUxBUxEI3YOgg((";
		private const string OAUTH_REDIRECT = "http://higgs.sobotics.org/Authentication/OAuthRedirect";

		[HttpGet("Login")]
		public IActionResult Login(
			[FromQuery(Name = "redirect_uri")] string redirectURI,
			[FromQuery(Name = "scope")] string scope
		)
		{
			var payload = JsonConvert.SerializeObject(new LoginState { RedirectURI = redirectURI, Scope = scope });
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(payload);
			var encodedPayload = System.Convert.ToBase64String(plainTextBytes);

			return Redirect($"https://stackexchange.com/oauth?client_id={CLIENT_ID}&scope=&redirect_uri={OAUTH_REDIRECT}&state={encodedPayload}");
		}
		
		[HttpGet("OAuthRedirect")]
		public async Task<IActionResult> OAuthRedirect(string code, string state)
		{
			var stateBytes = System.Convert.FromBase64String(state);
			var decodedState = System.Text.Encoding.UTF8.GetString(stateBytes);
			var payload = JsonConvert.DeserializeObject<LoginState>(decodedState);

			var stackExchangeClient = new RestClient("https://stackexchange.com/");
			var oauthRequest = new RestRequest("oauth/access_token/json", Method.POST);
			oauthRequest.AddParameter("client_id", CLIENT_ID);
			oauthRequest.AddParameter("client_secret", CLIENT_SECRET);
			oauthRequest.AddParameter("code", code);
			oauthRequest.AddParameter("redirect_uri", OAUTH_REDIRECT);

			var oauthResponse = await stackExchangeClient.ExecuteTaskAsync(oauthRequest, CancellationToken.None);
			var oauthContent = JsonConvert.DeserializeObject<dynamic>(oauthResponse.Content);
			
			var stackExchangeApiClient = new RestClient("https://api.stackexchange.com/");
			var apiRequest = new RestRequest("2.2/me", Method.GET);
			apiRequest.AddParameter("key", KEY);
			apiRequest.AddParameter("site", "stackoverflow");
			apiRequest.AddParameter("access_token", oauthContent.access_token);
			apiRequest.AddParameter("filter", "!JmXzzBW1uefdN).yXhRDGnC");
			var apiResponse = await stackExchangeApiClient.ExecuteTaskAsync(apiRequest);
			var apiContent = JsonConvert.DeserializeObject<dynamic>(apiResponse.Content);

			var userDetails = apiContent.items[0];
			int accountId = userDetails.account_id;
			string userType = userDetails.user_type;
			string displayName = userDetails.display_name;
			
			return Redirect(payload.RedirectURI + "?access_token=" + "somethingidunno");
		}
    }
}