namespace ProyectoFinal_Labo4.Models.Product.Dto
{
	public class ProductsDTO
	{
		public int Id { get; set; }

		public string Titulo { get; set; } = null!;

		public int Precio { get; set; }
		public string Url { get; set; } = null!;
        public int? CategoryId { get; set; }
    }
}
