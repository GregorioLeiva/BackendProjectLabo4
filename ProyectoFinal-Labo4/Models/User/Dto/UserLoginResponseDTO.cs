namespace ProyectoFinal_Labo4.Models.User.Dto
{
	public class UserLoginResponseDTO
	{
		public int Id { get; set; }

		public string? Email { get; set; }

		public string UserName { get; set; } = null!;

		public List<string> Roles { get; set; } = null!;
	}
}
