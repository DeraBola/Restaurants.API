
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Restaurants.Application.Serialization
{
	public class DateOnlySchemaFilter : ISchemaFilter
	{
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (context.Type == typeof(DateOnly))
			{
				schema.Type = "string";
				schema.Format = "date";
			}
		}
	}
}
