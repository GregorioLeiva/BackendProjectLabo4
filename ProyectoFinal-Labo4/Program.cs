
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal_Labo4.Config;
using ProyectoFinal_Labo4.Repositories;
using ProyectoFinal_Labo4.Services;
using ProyectoFinal_Labo4.Utils.Filters;
using System.Text;

namespace ProyectoFinal_Labo4
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Version = "v1",
					Title = "Un e-commerce",
					Description = "Es una API para un e-commerce de Proyecto Final",
				});
				options.AddSecurityDefinition("Token", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					BearerFormat = "JWT",
					Description = "Ingrese el token JWT",
					Name = "Authorization",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
					Scheme = "bearer"
				});
				options.OperationFilter<AuthOperationFilter>();
			});

			// Services: Agregamos los servicios al scope para utilizar Inyección de Depndencias.
			builder.Services.AddScoped<ProductServices>();
			builder.Services.AddScoped<UserServices>();
			builder.Services.AddScoped<IEncoderServices, EncoderServices>();
			builder.Services.AddScoped<AuthServices>();

			// Repositorios
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IProductRepository, ProductRespository>();

			// AutoMapper
			builder.Services.AddAutoMapper(typeof(Mapping));

			//SQL
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
			});

			// secret key
			var secretKey = builder.Configuration.GetSection("jwtSettings").GetSection("secretKey").ToString();

			// JWT
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true
					};
				});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//cors
			app.UseCors(options =>
			{
				options.AllowAnyHeader();
				options.AllowAnyMethod();
				options.AllowAnyOrigin();
			});

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
