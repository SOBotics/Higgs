using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Higgs.Server.Data;
using Higgs.Server.Data.Models;
using Higgs.Server.Utilities;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;

namespace Higgs.Server.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly string _oauthRedirect;
        private readonly IConfiguration _configuration;
        private readonly HiggsDbContext _dbContext;
        public const string ACCOUNT_ID_CLAIM = "accountId";

        public AuthenticationController(IConfiguration configuration, HiggsDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _oauthRedirect = $"{_configuration["HostName"]}/Authentication/OAuthRedirect";
        }

        private static string EncodeBase64(string str)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(plainTextBytes);
        }

        private static string DecodeBase64(string str)
        {
            var plainTextBytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(plainTextBytes);
        }
        
        [HttpGet("Login")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult Login(
            [FromQuery(Name = "redirect_uri")] string redirectURI,
            [FromQuery(Name = "scope")] string scope
        )
        {
            if (Request.GetUri().IsLoopback && "true".Equals(_configuration["BypassLoopbackAuth"], StringComparison.OrdinalIgnoreCase))
            {
                // We can just login the user immediately, if we have one.
                var user = _dbContext.Users.Include(u => u.UserScopes).FirstOrDefault(u => u.AccountId == DBExtensions.RobAccountId);
                if (user != null)
                {
                    var userScopes = user.UserScopes.Select(s => s.ScopeName).ToList();

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ACCOUNT_ID_CLAIM, user.AccountId.ToString())
                    }.Concat(userScopes.Select(c => new Claim(c, string.Empty)));

                    var signingKey = Convert.FromBase64String(_configuration["JwtSigningKey"]);
                    var token = CreateJwtToken(claims, signingKey);

                    return Redirect($"{redirectURI}?access_token={token}");
                }
            }

            var clientId = _configuration["SE.ClientId"];
            var payload = JsonConvert.SerializeObject(new LoginState { RedirectURI = redirectURI, Scope = scope ?? string.Empty });
            var encodedPayload = EncodeBase64(payload);

            return Redirect($"https://stackexchange.com/oauth?client_id={clientId}&scope=&redirect_uri={_oauthRedirect}&state={encodedPayload}");
        }

        /// <summary>
        ///     Redirect endpoint for OAuth flow
        /// </summary>
        [HttpGet("OAuthRedirect")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> OAuthRedirect(string code, string state, string error,
            [FromQuery(Name = "error_description")] string errorDescription, 
            [FromQuery(Name = "access_token")] string accessToken)
        {
            if (!string.IsNullOrEmpty(error))
                return Json(new
                {
                    error,
                    error_description = errorDescription
                });

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                // Get the access token
                var stackExchangeClient = new RestClient("https://stackexchange.com/");
                var oauthRequest = new RestRequest("oauth/access_token/json", Method.POST);
                oauthRequest.AddParameter("client_id", _configuration["SE.ClientId"]);
                oauthRequest.AddParameter("client_secret", _configuration["SE.ClientSecret"]);
                oauthRequest.AddParameter("code", code);
                oauthRequest.AddParameter("redirect_uri", _oauthRedirect);

                var oauthResponse = await stackExchangeClient.ExecuteTaskAsync(oauthRequest, CancellationToken.None);
                var oauthContent = JsonConvert.DeserializeObject<dynamic>(oauthResponse.Content);
                accessToken = oauthContent.access_token;
            }

            // Query the SE.API with their access token, to get their user details.
            var stackExchangeApiClient = new RestClient("https://api.stackexchange.com/");
            var apiRequest = new RestRequest("2.2/me", Method.GET);
            apiRequest.AddParameter("key", _configuration["SE.Key"]);
            apiRequest.AddParameter("site", "stackoverflow");
            apiRequest.AddParameter("access_token", accessToken);
            apiRequest.AddParameter("filter", "!)iua4.KHF.lCb61RIH7hp");
            var apiResponse = await stackExchangeApiClient.ExecuteTaskAsync(apiRequest);
            var apiContent = JsonConvert.DeserializeObject<dynamic>(apiResponse.Content);

            var userDetails = apiContent.items[0];
            int accountId = userDetails.user_id;
            string displayName = userDetails.display_name;

            var signingKey = Convert.FromBase64String(_configuration["JwtSigningKey"]);
            
            // Temporary measure for testing
            var defaultNewUserScopes = Scopes.AllScopes.Select(a => a.Key).ToArray();
            var user = _dbContext.GetOrCreateUser(accountId, defaultNewUserScopes);
            user.Name = displayName;

            _dbContext.SaveChanges();

            var userScopes = user.UserScopes.Select(s => s.ScopeName).ToList();

            var decodedState = DecodeBase64(state);
            var loginState = JsonConvert.DeserializeObject<LoginState>(decodedState);
            var requestedScopes = loginState.Scope.Split(' ').ToList();
            if (requestedScopes.Count == 1 && requestedScopes[0] == "all")
                requestedScopes = userScopes;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, displayName),
                new Claim(ACCOUNT_ID_CLAIM, accountId.ToString())
            }.Concat(userScopes.Intersect(requestedScopes).Select(c => new Claim(c, string.Empty)));

            var token = CreateJwtToken(claims, signingKey);

            return Redirect($"{loginState.RedirectURI}?access_token={token}");
        }
        
        public static string CreateJwtToken(IEnumerable<Claim> claims, byte[] symmetricKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(symmetricKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }

        private class LoginState
        {
            public string RedirectURI { get; set; }
            public string Scope { get; set; }
        }
    }
}