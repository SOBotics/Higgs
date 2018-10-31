using System;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server
{
    public class RemoveStatus200Attribute : Attribute
    {
    }

    public class RemoveStatus200OperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.GetCustomAttributes(true);
            if (attributes.OfType<RemoveStatus200Attribute>().Any())
            {
                var apiDescriptionSupportedResponseTypes = context.ApiDescription.SupportedResponseTypes;
                for (var i = apiDescriptionSupportedResponseTypes.Count - 1; i >= 0; i--)
                    if (apiDescriptionSupportedResponseTypes[i].StatusCode == 200)
                        apiDescriptionSupportedResponseTypes.RemoveAt(i);
            }
        }
    }
}