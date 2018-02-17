﻿using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server.Controllers
{
	[Route("[controller]")]
	public class AuthenticationController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly HiggsDbContext _dbContext;

		private class LoginState
		{
			public string RedirectURI { get; set; }
			public string Scope { get; set; }
		}

		private const string OAUTH_REDIRECT = "http://higgs.sobotics.org/Authentication/OAuthRedirect";

		public AuthenticationController(IConfiguration configuration, HiggsDbContext dbContext)
		{
			_configuration = configuration;
			_dbContext = dbContext;
		}

		private static string EncodeBase64(string str)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
			return Convert.ToBase64String(plainTextBytes);
		}

		private static string DecodeBase64(string str)
		{
			var plainTextBytes = Convert.FromBase64String(str);
			return System.Text.Encoding.UTF8.GetString(plainTextBytes);
		}

		/// <returns></returns>
		[HttpGet("Login")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public RedirectResult Login(
			[FromQuery(Name = "redirect_uri")] string redirectURI,
			[FromQuery(Name = "scope")] string scope
		)
		{
			var payload = JsonConvert.SerializeObject(new LoginState { RedirectURI = redirectURI, Scope = scope ?? string.Empty });
			var encodedPayload = EncodeBase64(payload);

			var clientId = _configuration["SE.ClientId"];
			return Redirect($"https://stackexchange.com/oauth?client_id={clientId}&scope=&redirect_uri={OAUTH_REDIRECT}&state={encodedPayload}");
		}
		
		/// <summary>
		/// Redirect endpoint for OAuth flow
		/// </summary>
		[HttpGet("OAuthRedirect")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> OAuthRedirect(string code, string state, string error, string error_description)
		{
			if (!String.IsNullOrEmpty(error))
			{
				//TODO: Show an error page
				return Json(new
				{
					error,
					error_description
				});
			}

			var decodedState = DecodeBase64(state);
			var payload = JsonConvert.DeserializeObject<LoginState>(decodedState);

			var stackExchangeClient = new RestClient("https://stackexchange.com/");
			var oauthRequest = new RestRequest("oauth/access_token/json", Method.POST);
			oauthRequest.AddParameter("client_id", _configuration["SE.ClientId"]);
			oauthRequest.AddParameter("client_secret", _configuration["SE.ClientSecret"]);
			oauthRequest.AddParameter("code", code);
			oauthRequest.AddParameter("redirect_uri", OAUTH_REDIRECT);

			var oauthResponse = await stackExchangeClient.ExecuteTaskAsync(oauthRequest, CancellationToken.None);
			var oauthContent = JsonConvert.DeserializeObject<dynamic>(oauthResponse.Content);
			
			var stackExchangeApiClient = new RestClient("https://api.stackexchange.com/");
			var apiRequest = new RestRequest("2.2/me", Method.GET);
			apiRequest.AddParameter("key", _configuration["SE.Key"]);
			apiRequest.AddParameter("site", "stackoverflow");
			apiRequest.AddParameter("access_token", oauthContent.access_token);
			apiRequest.AddParameter("filter", "!JmXzzBW1uefdN).yXhRDGnC");
			var apiResponse = await stackExchangeApiClient.ExecuteTaskAsync(apiRequest);
			var apiContent = JsonConvert.DeserializeObject<dynamic>(apiResponse.Content);

			var userDetails = apiContent.items[0];
			int accountId = userDetails.account_id;
			string userType = userDetails.user_type;
			string displayName = userDetails.display_name;

			var tokenHandler = new JwtSecurityTokenHandler();
			
			var symmetricKey = Convert.FromBase64String(_configuration["JwtSigningKey"]);
			
			var existingUser = _dbContext.Users.Include(u => u.UserScopes).FirstOrDefault(u => u.AccountId == accountId);
			var newUserScopes = new[] { Scopes.REVIEWER_SEND_FEEDBACK };
			if (existingUser == null)
			{
				existingUser = new DbUser
				{
					AccountId = accountId,
					Name = displayName
				};
				_dbContext.Users.Add(existingUser);
				foreach (var allowedScope in newUserScopes)
				{
					_dbContext.UserScopes.Add(new DbUserScope
					{
						ScopeName = allowedScope,
						UserId = accountId
					});
				}
				_dbContext.SaveChanges();
			}
			else
			{
				newUserScopes = existingUser.UserScopes.Select(us => us.ScopeName).ToArray();
			}

			var requestedScopes = payload.Scope.Split(' ');
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, displayName),
				new Claim("accountId", accountId.ToString()),
			}.Concat(newUserScopes.Intersect(requestedScopes).Select(c => new Claim(c, string.Empty)));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),

				Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(60)),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
			};

			var stoken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(stoken);

			return Redirect($"{payload.RedirectURI}?access_token={token}");
		}
    }
}