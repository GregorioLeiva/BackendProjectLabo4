using ProyectoFinal_Labo4.Config;
using ProyectoFinal_Labo4.Models.Product;

namespace ProyectoFinal_Labo4.Repositories
{
	public interface IProductRepository : IRepository<Product> { }
	public class ProductRespository : Repository<Product>, IProductRepository
	{
		public ProductRespository(ApplicationDbContext db) : base(db) { }
	}
}
