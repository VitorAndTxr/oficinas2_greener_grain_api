using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace GreenerGrain.Framework.Swagger
{
    public class SwaggerParameterAttributeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<SwaggerParameterAttribute>();

            foreach (var attribute in attributes)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = attribute.Name,
                    Description = attribute.Description,
                    In = GetParameterType(attribute.ParameterType),
                    Required = attribute.Required,
                    Schema = new OpenApiSchema()
                    {
                        Type = attribute.DataType
                    }
                });
            }
        }

        private ParameterLocation GetParameterType(string parameterType)
        {
            ParameterLocation parameterLocation = ParameterLocation.Query;

            switch (parameterType)
            {
                case "query":
                    parameterLocation = ParameterLocation.Query;
                    break;
                case "header":
                    parameterLocation = ParameterLocation.Header;
                    break;
                case "path":
                    parameterLocation = ParameterLocation.Path;
                    break;
                case "cookie":
                    parameterLocation = ParameterLocation.Cookie;
                    break;
            }

            return parameterLocation;
        }
    }
}
