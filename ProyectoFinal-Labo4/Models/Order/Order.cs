using ProyectoFinal_Labo4.Models.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProyectoFinal_Labo4.Models.Order;

namespace ProyectoFinal_Labo4.Models.Orders
{
    public class Order
    {  
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime FechaOrden  { get; set; } = DateTime.Now;

        [Required]
        public int  UserId  { get; set; }

        [ForeignKey("UserId")]
		public User.User User { get; set; } = null!;

        public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public Product.Product Product { get; set; } = null!;

		[Required]
		public int Quantity { get; set; }

		[Required]
		public decimal Price { get; set; }

	}
}
