using ProyectoFinal_Labo4.Models.User.Dto;

namespace ProyectoFinal_Labo4.Models.Auth.Dto
{
	public class LoginResponseDTO
	{
		public string Token { get; set; } = null!;

		public UserLoginResponseDTO User { get; set; } = null!;
	}
}
