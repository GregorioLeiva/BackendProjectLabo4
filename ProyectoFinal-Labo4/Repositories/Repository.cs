using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Labo4.Config;
using System.Linq.Expressions;
using System.Linq;
using ProyectoFinal_Labo4.Models.User;

namespace ProyectoFinal_Labo4.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null);
		Task<T> GetOne(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
		Task Add(T entity);
		Task<T> Update(T entity);
		Task Delete(T entity);
		Task Save();
	}

	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;

		public Repository(ApplicationDbContext db)
		{
			_db = db;
			dbSet = _db.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return await query.ToListAsync();
		}

		public async Task<T> GetOne(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
            
            if (filter != null)
			{
				query = query.Where(filter);
			}
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            return await query.FirstOrDefaultAsync();
		}

		public async Task Add(T entity)
		{
			await dbSet.AddAsync(entity);
			await Save();
		}

		public async Task<T> Update(T entity)
		{
			dbSet.Update(entity);
			await Save();
			return entity;
		}

		public async Task Delete(T entity)
		{
			dbSet.Remove(entity);
			await Save();
		}

		public async Task Save()
		{
			await _db.SaveChangesAsync();
		}
	}
}
