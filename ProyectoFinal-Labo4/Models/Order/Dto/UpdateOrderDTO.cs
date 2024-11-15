using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Order.Dto
{
    public class UpdateOrderDTO
    {
		[Required]
		public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
