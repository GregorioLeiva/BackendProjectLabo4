using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Orders.Dto
{
    public class CreateOrderDTO
    {
        [Required]
        public DateTime FechaOrden { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }

		[Required]
        public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		public decimal Price { get; set; }



	}
}
