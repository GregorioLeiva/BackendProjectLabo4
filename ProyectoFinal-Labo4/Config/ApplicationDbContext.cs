using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Labo4.Enums;
using ProyectoFinal_Labo4.Models.Category;
using ProyectoFinal_Labo4.Models.Order;
using ProyectoFinal_Labo4.Models.Orders;
using ProyectoFinal_Labo4.Models.Product;
using ProyectoFinal_Labo4.Models.Role;
using ProyectoFinal_Labo4.Models.User;

namespace ProyectoFinal_Labo4.Config
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Product> Productos { get; set; }
		public DbSet<User> Users { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Order> Orders { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Nombre = "Category 1" },
				new Category { Id = 2, Nombre = "Category 2" },
				new Category { Id = 3, Nombre = "Category 3" }
			);

			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Id = 1,
					Titulo = "Producto 1",
					Precio = 100,
					Unidades = 10,
					Descripcion = "Descripción del producto 1",
					Url= "https://res.cloudinary.com/dmmyupmtm/image/upload/v1731359998/Global%20Market/inodorodeporcelana.jpg",
                    CategoryId = 1
				},
				new Product
				{
					Id = 2,
					Titulo = "Producto 2",
					Precio = 200,
					Unidades = 20,
					Descripcion = "Descripción del producto 2",
					Url= "https://res.cloudinary.com/dmmyupmtm/image/upload/v1731360057/Global%20Market/lavamanosdeceramica.jpg",
					CategoryId = 2
				},
				new Product
				{
					Id = 3,
					Titulo = "Producto 3",
					Precio = 300,
					Unidades = 30,
					Descripcion = "Descripción del producto 3",
					Url= "https://res.cloudinary.com/dmmyupmtm/image/upload/v1731360413/Global%20Market/duchatermostatica.jpg",
					CategoryId = 3
				}
			);
            modelBuilder.Entity<Category>()
               .Property(c => c.Nombre)
               .IsRequired()
               .HasMaxLength(30);

			modelBuilder.Entity<Role>().HasData(
				new Role { Id = 1, Name = ROLES.ADMIN },
				new Role { Id = 2, Name = ROLES.MOD },
				new Role { Id = 3, Name = ROLES.USER }
			);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "LeoMessi@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("el10123"),
                    UserName = "Leo10",
                },
                new User
                {
                    Id = 2,
                    Email = "JuanGomez@outlook.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Juan27"),
                    UserName = "JuanGo",
                },
                new User
                {
                    Id = 3,
                    Email = "Marti@yahoo.com.ar",
                    Password = BCrypt.Net.BCrypt.HashPassword("12345678"),
                    UserName = "Marti1",
                }
            );

            modelBuilder.Entity<User>()
			.HasMany(u => u.Roles)
			.WithMany()
			.UsingEntity<RoleUsers>(
				l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
				r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId)
			);

            modelBuilder.Entity<RoleUsers>().HasData(
                 new RoleUsers { UserId = 1, RoleId = 1 },
                 new RoleUsers { UserId = 2, RoleId = 2 },
                 new RoleUsers { UserId = 3, RoleId = 3 }
            );

        }
    }
}
