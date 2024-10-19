using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Product.Dto
{
	public class CreateProductDTO
	{
		[Required]
		[MaxLength(150)]
		public string Titulo { get; set; } = null!;

		[Required]
		public int Precio { get; set; }

		[Required]
		public int Unidades { get; set; }

		[Required]
		[MaxLength(500)]
		public string Descripcion { get; set; } = null!;
	}
}
