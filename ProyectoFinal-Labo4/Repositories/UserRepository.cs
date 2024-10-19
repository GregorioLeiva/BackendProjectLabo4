using ProyectoFinal_Labo4.Config;
using ProyectoFinal_Labo4.Models.User;

namespace ProyectoFinal_Labo4.Repositories
{
	public interface IUserRepository : IRepository<User> { }

	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(ApplicationDbContext db) : base(db) { }
	}
}
