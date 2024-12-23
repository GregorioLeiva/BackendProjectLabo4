﻿using ProyectoFinal_Labo4.Models.Category;

namespace ProyectoFinal_Labo4.Models.Product.Dto
{
	public class ProductDTO
	{
		public int Id { get; set; }

		public string Titulo { get; set; } = null!;

		public int Precio { get; set; }

		public int Unidades { get; set; }

		public string Descripcion { get; set; } = null!;
		public string Url { get; set; } = null!;

		public Category.Category Category { get; set; } = null!;
	}
}
