using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace GreenerGrain.Framework.Swagger
{
    public class SwaggerSecurityRequirementsOperationFilter : IOperationFilter
    {
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.Security == null)
			{
				operation.Security = new List<OpenApiSecurityRequirement>();
			}

			var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } };
			operation.Security.Add(new OpenApiSecurityRequirement
			{
				[scheme] = new List<string>()
			});
		}
	}
}
