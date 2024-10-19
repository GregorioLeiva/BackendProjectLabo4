using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Web.Http;

namespace ProyectoFinal_Labo4.Utils.Filters
{
	public class AuthOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var Attributes = context.ApiDescription.CustomAttributes();
			var isAuthRequired = Attributes.Any(attr => attr.GetType() == typeof(AuthorizeAttribute)); //de todos los atributos que le pega el usuario, hay algun authorize?
			var AllowAnonymous = Attributes.Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute)); //de todos los atributos que le pega el usuario, hay algun allowanonymous?

			if (!isAuthRequired || AllowAnonymous) return;

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
								Id = "Token"
							}
						}
					] = new string []{}
				},
			};


		}
	}
}
