using System;
using Higgs.Server.Data;
using Higgs.Server.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("X-MiniProfiler-Ids")
                );
            });

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
                foreach (var scope in Scopes.AllScopes) options.AddPolicy(scope.Key, a => a.RequireClaim(scope.Key));
            });
            
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}/Higgs.Server.xml", AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new Info {Title = "Higgs API", Version = "v1"});
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{Configuration["HostName"]}/Authentication/Login",
                    Scopes = Scopes.AllScopes
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.OperationFilter<RequiredParameterOperationFilter>();
                c.OperationFilter<RemoveStatus200OperationFilter>();
            });

            var connectionString = Configuration.GetConnectionString("HiggsDB");
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<HiggsDbContext>(options => options.UseNpgsql(connectionString));

            services.AddMiniProfiler(options =>
            {
                options.ResultsAuthorize = request => request.HttpContext.User.HasClaim(Scopes.SCOPE_DEV);
                options.ResultsListAuthorize = request => request.HttpContext.User.HasClaim(Scopes.SCOPE_DEV);
                options.TrackConnectionOpenClose = false;
            }).AddEntityFramework();

            services.AddLogging(c =>
            {
                c.AddConsole();
                c.AddConfiguration(Configuration.GetSection("Logging"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseCors("AllowAnyOrigin");
            app.UseMiniProfiler();
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Higgs API V1"));
            
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HiggsDbContext>();
                context.Database.Migrate();
                context.Setup();
            }
        }
    }
}