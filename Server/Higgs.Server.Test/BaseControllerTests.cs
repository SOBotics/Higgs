using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Higgs.Server.Controllers;
using Higgs.Server.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Higgs.Server.Test
{
    public class BaseControllerTests
    {
        protected IConfigurationRoot Configuration;
        protected TestServer Server;
        protected HttpClient Client;
        protected IServiceProvider Services => Server.Host.Services;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("test-config.json")
                .Build();
            Configuration = config;
            
            Server = new TestServer(new WebHostBuilder()
                .UseConfiguration(config)
                .UseStartup<TestStartup>()
            );
            
            Client = Server.CreateClient();
        }

        protected void Authenticate(IEnumerable<string> scopes)
        {
            Authenticate(scopes.ToArray());
        }

        protected void Authenticate(params string[] scopes)
        {
            Authenticate(Enumerable.Empty<Claim>(), scopes);
        }

        protected void Authenticate(IEnumerable<Claim> claims, params string[] scopes)
        {
            Authenticate(claims.Concat(scopes.Select(s => new Claim(s, string.Empty))));
        }

        protected void Authenticate(IEnumerable<Claim> claims)
        {
            Authenticate(claims.ToArray());
        }

        protected void Authenticate(params Claim[] claims)
        {
            var signingKey = Convert.FromBase64String(Configuration["JwtSigningKey"]);
            var token = AuthenticationController.CreateJwtToken(claims, signingKey);
            if (Client.DefaultRequestHeaders.Contains("Authorization")) {
                Client.DefaultRequestHeaders.Remove("Authorization");
            }
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        protected void WithDatabase(Action<HiggsDbContext> method)
        {
            using (var serviceScope = Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HiggsDbContext>();
                method(context);
            }
        }
    }
}
