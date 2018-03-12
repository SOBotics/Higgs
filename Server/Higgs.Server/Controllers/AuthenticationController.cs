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
            var payload =
                JsonConvert.SerializeObject(new LoginState {RedirectURI = redirectURI, Scope = scope ?? string.Empty});
            var encodedPayload = EncodeBase64(payload);

            var clientId = _configuration["SE.ClientId"];
            return Redirect($"https://stackexchange.com/oauth?client_id={clientId}&scope=&redirect_uri={_oauthRedirect}&state={encodedPayload}");
        }

        /// <summary>
        ///     Redirect endpoint for OAuth flow
        /// </summary>
        [HttpGet("OAuthRedirect")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> OAuthRedirect(string code, string state, string error,
            string error_description, string access_token)
        {
            if (!string.IsNullOrEmpty(error))
                return Json(new
                {
                    error,
                    error_description
                });

            if (string.IsNullOrWhiteSpace(access_token))
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
                access_token = oauthContent.access_token;
            }

            // Query the SE.API with their access token, to get their user details.
            var stackExchangeApiClient = new RestClient("https://api.stackexchange.com/");
            var apiRequest = new RestRequest("2.2/me", Method.GET);
            apiRequest.AddParameter("key", _configuration["SE.Key"]);
            apiRequest.AddParameter("site", "stackoverflow");
            apiRequest.AddParameter("access_token", access_token);
            apiRequest.AddParameter("filter", "!JmXzzBW1uefdN).yXhRDGnC");
            var apiResponse = await stackExchangeApiClient.ExecuteTaskAsync(apiRequest);
            var apiContent = JsonConvert.DeserializeObject<dynamic>(apiResponse.Content);

            var userDetails = apiContent.items[0];
            int accountId = userDetails.account_id;
            string displayName = userDetails.display_name;

            var signingKey = Convert.FromBase64String(_configuration["JwtSigningKey"]);

            var defaultNewUserScopes = new[] {Scopes.REVIEWER_SEND_FEEDBACK};

            // Temporary measure for testing
            defaultNewUserScopes = Scopes.AllScopes.Select(a => a.Key).ToArray();

            var userScopes = GetOrCreateUser(accountId, displayName, defaultNewUserScopes).ToList();

            var decodedState = DecodeBase64(state);
            var loginState = JsonConvert.DeserializeObject<LoginState>(decodedState);
            var requestedScopes = loginState.Scope.Split(' ').ToList();
            if (requestedScopes.Count == 1 && requestedScopes[0] == "all")
                requestedScopes = userScopes;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, displayName),
                new Claim("accountId", accountId.ToString())
            }.Concat(userScopes.Intersect(requestedScopes).Select(c => new Claim(c, string.Empty)));

            var token = CreateJwtToken(claims, signingKey);

            return Redirect($"{loginState.RedirectURI}?access_token={token}");
        }

        private IEnumerable<string> GetOrCreateUser(int accountId, string displayName, string[] defaultNewUserScopes)
        {
            var existingUser = _dbContext.Users.Include(u => u.UserScopes)
                .FirstOrDefault(u => u.AccountId == accountId);
            var userScopes = defaultNewUserScopes.ToArray();
            if (existingUser == null)
            {
                existingUser = new DbUser
                {
                    AccountId = accountId,
                    Name = displayName
                };
                _dbContext.Users.Add(existingUser);
                foreach (var allowedScope in defaultNewUserScopes)
                    _dbContext.UserScopes.Add(new DbUserScope
                    {
                        ScopeName = allowedScope,
                        UserId = accountId
                    });

                _dbContext.SaveChanges();
            }
            else
            {
                userScopes = existingUser.UserScopes.Select(us => us.ScopeName).ToArray();
            }

            return userScopes;
        }

        public static string CreateJwtToken(IEnumerable<Claim> claims, byte[] symmetricKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(60)),
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