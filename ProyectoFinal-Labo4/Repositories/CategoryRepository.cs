using ProyectoFinal_Labo4.Config;
using ProyectoFinal_Labo4.Models.Category;

namespace ProyectoFinal_Labo4.Repositories
{
    public interface ICategoryRepository : IRepository<Category> { }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext db) : base(db) { }
    }
}
