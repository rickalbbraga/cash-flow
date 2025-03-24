using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Configurations.Swagger
{
    public class EnumDescriptionSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum = context.Type
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(field =>
                        new OpenApiString(
                            field.GetRawConstantValue() + " - " +
                            (field.GetCustomAttribute<DescriptionAttribute>()?.Description ?? field.Name)
                        ) as IOpenApiAny
                    )
                    .ToList();
            }
        }
    }
}