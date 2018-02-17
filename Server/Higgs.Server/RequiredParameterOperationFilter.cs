using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Higgs.Server
{
    public class RequiredParameterOperationFilter : IOperationFilter
	{
	    public void Apply(Operation operation, OperationFilterContext context)
	    {
			if (operation.Parameters == null || !operation.Parameters.Any())
		    {
			    return;
		    }

		    var paramLookup = operation.Parameters.ToDictionary(p => p.Name, p => p);

		    foreach (var parameterDescription in context.ApiDescription.ParameterDescriptions)
		    {
			    if (parameterDescription.ParameterDescriptor is ControllerParameterDescriptor controllerParameterDescription)
			    {
				    if (controllerParameterDescription.ParameterInfo.GetCustomAttribute(typeof(RequiredAttribute)) != null)
				    {
					    if (paramLookup.ContainsKey(parameterDescription.Name))
						    paramLookup[parameterDescription.Name].Required = true;
				    }
			    }
		    }
		}
	}
}
