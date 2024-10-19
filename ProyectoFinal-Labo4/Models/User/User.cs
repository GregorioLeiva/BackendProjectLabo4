using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.User
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(40)]
		public string Name { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[MinLength(6)]
		public string Password { get; set; } = null!;

		[Required]
		public string UserName { get; set; } = null!;
	}
}
