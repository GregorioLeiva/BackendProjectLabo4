using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Labo4.Config;
using ProyectoFinal_Labo4.Models.Product;
using ProyectoFinal_Labo4.Models.User;
using System.Linq.Expressions;
using System.Linq;

namespace ProyectoFinal_Labo4.Repositories
{
	public interface IProductRepository : IRepository<Product> { }
	public class ProductRespository : Repository<Product>, IProductRepository
	{
		public ProductRespository(ApplicationDbContext db) : base(db) { }

		public new async Task<Product> GetOne(Expression<Func<Product, bool>>? filter = null)
		{
			IQueryable<Product> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter).Include(p => p.Category);
			}
			return await query.FirstOrDefaultAsync();
		}
	}
}
