using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Labo4.Models.Product;
using ProyectoFinal_Labo4.Models.User;

namespace ProyectoFinal_Labo4.Config
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Product> Productos { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Id = 1,
					Titulo = "Producto 1",
					Precio = 100,
					Unidades = 10,
					Descripcion = "Descripción del producto 1"
				},
				new Product
				{
					Id = 2,
					Titulo = "Producto 2",
					Precio = 200,
					Unidades = 20,
					Descripcion = "Descripción del producto 2"
				},
				new Product
				{
					Id = 3,
					Titulo = "Producto 3",
					Precio = 300,
					Unidades = 30,
					Descripcion = "Descripción del producto 3"
				}
			);
		}
	}
}
