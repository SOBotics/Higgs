using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Higgs.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

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

			services.AddAuthorization(options =>
	        {
		        foreach (var scope in Scopes.AllScopes)
		        {
			        options.AddPolicy(scope.Key, a => a.RequireClaim(scope.Key));
				}
	        });

	        services.AddSwaggerGen(c =>
	        {
				c.SwaggerDoc("v1", new Info {Title = "Higgs API", Version = "v1"});
				c.AddSecurityDefinition("oauth2", new OAuth2Scheme
				{
					Type = "oauth2",
					Flow = "implicit",
					AuthorizationUrl = "/Authentication/Login",
					Scopes = Scopes.AllScopes
				});
				c.OperationFilter<SecurityRequirementsOperationFilter>();
	        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

	        app.UseAuthentication();
			app.UseMvc();
			app.UseSwagger();
	        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Higgs API V1"));
        }
    }
}
