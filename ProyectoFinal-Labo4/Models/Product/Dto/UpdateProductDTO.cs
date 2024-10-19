using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Product.Dto
{
	public class UpdateProductDTO
	{
		[MaxLength(150)]
		public string? Titulo { get; set; }
		public int? Precio { get; set; }

		public int? Unidades { get; set; }

		[MaxLength(500)]
		public string? Descripcion { get; set; }
	}
}
