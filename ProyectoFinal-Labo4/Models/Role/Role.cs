using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Role
{
	public class Role
	{
		//[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(20, MinimumLength = 3)]
		public string Name { get; set; } = null!;
	}

	public class RoleUsers
	{
		public int RoleId { get; set; }
		public int UserId { get; set; }
	}
}
