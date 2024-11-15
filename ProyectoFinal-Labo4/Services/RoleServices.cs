using ProyectoFinal_Labo4.Models.Role;
using ProyectoFinal_Labo4.Repositories;
using ProyectoFinal_Labo4.Utils.Exceptions;
using System.Net;

namespace ProyectoFinal_Labo4.Services
{
	public class RoleServices
	{
		private readonly IRoleRepository _roleRepository;

		public RoleServices(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		public async Task<Role> GetOneByName(string name)
		{
			var role = await _roleRepository.GetOne(r => r.Name == name);
			if (role == null)
			{
				throw new CustomHttpException(
					$"No se encontro el rol con el nombre : {name}", HttpStatusCode.NotFound);
			}

			return role;
		}

		public async Task<List<Role>> GetManyByIds(List<int> roleIds)
		{
			if (roleIds == null || roleIds.Count == 0)
			{
				throw new CustomHttpException("No se encontro el Rol", HttpStatusCode.BadRequest);
			}

			var roles = await _roleRepository.GetAll(r => roleIds.Contains(r.Id));
			return roles.ToList();
		}
	}
}
