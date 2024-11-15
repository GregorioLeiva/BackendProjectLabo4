using Microsoft.IdentityModel.Tokens;
using ProyectoFinal_Labo4.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoFinal_Labo4.Services
{
	public class AuthServices
	{
		private string secretKey;
		public AuthServices(IConfiguration config)
		{
			secretKey = config.GetSection("jwtSettings").GetSection("secretKey").ToString() ?? null!;
		}

		public string GenerateJwtToken(User user)
		{
			var claims = new ClaimsIdentity();
			claims.AddClaim(new Claim("id", user.Id.ToString()));

			if (user.Roles != null)
			{
				foreach (var role in user.Roles)
				{
					claims.AddClaim(new Claim(ClaimTypes.Role, role.Name));
				}
			}

			var symetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var credentials = new SigningCredentials(
					symetricKey,
					SecurityAlgorithms.HmacSha256Signature
				);

			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = claims,
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = credentials
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
			string token = tokenHandler.WriteToken(tokenConfig);
			return token;
		}
	}
}
