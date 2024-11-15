using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Auth.Dto
{
	public class UpdateUserRolesDTO
	{
		[Required]
		public List<int> RoleIds { get; set; }
	}
}
