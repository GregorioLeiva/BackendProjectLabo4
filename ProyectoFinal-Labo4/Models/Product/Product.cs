using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Product
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

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

		[Required]
		public int CategoryId { get; set; }

		[Required]
		public string Url { get; set; }

		[ForeignKey("CategoryId")]
		public Category.Category Category { get; set; } = null!;
	}
}
