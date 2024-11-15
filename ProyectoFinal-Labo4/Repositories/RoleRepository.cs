using ProyectoFinal_Labo4.Config;
using ProyectoFinal_Labo4.Models.Role;

namespace ProyectoFinal_Labo4.Repositories
{
	public interface IRoleRepository : IRepository<Role> { }
	public class RoleRepository : Repository<Role>, IRoleRepository
	{
		public RoleRepository(ApplicationDbContext db) : base(db) { }
	}
}
