using System;
using Higgs.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Higgs.Server.Test
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration env) : base(env)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthorization(options =>
            {
                foreach (var scope in Scopes.AllScopes) options.AddPolicy(scope.Key, a => a.RequireClaim(scope.Key));
            });
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<HiggsDbContext>((sp, options) =>
                {
                    options
                        .UseInMemoryDatabase("HiggsDb")
                        .UseInternalServiceProvider(sp)
                        .ConfigureWarnings(warnings =>
                            {
                                warnings.Throw(RelationalEventId.QueryClientEvaluationWarning);
                            })
                        ;
                });

            var symmetricKey = Convert.FromBase64String(Configuration["JwtSigningKey"]);
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                    };
                });
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
            app.UseMvc();
        }
    }

}
