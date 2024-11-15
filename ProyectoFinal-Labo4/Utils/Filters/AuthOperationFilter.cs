using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinal_Labo4.Utils.Filters
{
    public class AuthOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var Attributes = context.ApiDescription.CustomAttributes();
			var isAuthRequired = Attributes.Any(attr => attr.GetType() == typeof(AuthorizeAttribute)); 
			var allowAnonymous = Attributes.Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute)); 

			if (!isAuthRequired || allowAnonymous) return;

			operation.Security = new List<OpenApiSecurityRequirement>
			{
				new OpenApiSecurityRequirement
				{
					[
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						}
					] = new string []{}
				},
			};
		}
	}


    
}



