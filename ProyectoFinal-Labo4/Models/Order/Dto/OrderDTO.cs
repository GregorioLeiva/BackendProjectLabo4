using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Orders.Dto
{
    public class OrderDTO
    {

        public int Id { get; set; }
        public DateTime FechaOrden { get; set; } = DateTime.Now;

		public int UserId { get; set; }

		public int ProductId { get; set; }

		public int Quantity { get; set; }

        public decimal Price { get; set; }


	}
}
