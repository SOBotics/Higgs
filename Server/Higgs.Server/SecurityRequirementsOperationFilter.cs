using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        private readonly IOptions<AuthorizationOptions> authorizationOptions;

        public SecurityRequirementsOperationFilter(IOptions<AuthorizationOptions> authorizationOptions)
        {
            this.authorizationOptions = authorizationOptions;
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            var policies =
                context.ApiDescription.ControllerAttributes()
                    .OfType<AuthorizeAttribute>()
                    .Union(context.ApiDescription.ActionAttributes().OfType<AuthorizeAttribute>())
                    .Select(a => a.Policy);

            var requiredClaimTypes = policies
                .Select(x => authorizationOptions.Value.GetPolicy(x))
                .SelectMany(x => x.Requirements)
                .OfType<ClaimsAuthorizationRequirement>()
                .Select(x => x.ClaimType)
                .ToList();

            if (requiredClaimTypes.Any())
            {
                operation.Responses.Add("401", new Response {Description = "Unauthorized"});
                operation.Responses.Add("403", new Response {Description = "Forbidden"});

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                operation.Security.Add(
                    new Dictionary<string, IEnumerable<string>>
                    {
                        {"oauth2", requiredClaimTypes}
                    });
            }
        }
    }
}