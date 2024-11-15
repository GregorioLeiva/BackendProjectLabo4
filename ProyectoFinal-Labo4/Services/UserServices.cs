using AutoMapper;
using ProyectoFinal_Labo4.Models.User.Dto;
using ProyectoFinal_Labo4.Models.User;
using ProyectoFinal_Labo4.Repositories;
using ProyectoFinal_Labo4.Utils.Exceptions;
using System.Net;
using ProyectoFinal_Labo4.Models.Role;


namespace ProyectoFinal_Labo4.Services
{
	public class UserServices
	{

		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepo;
		private readonly IEncoderServices _encoderServices;

		public UserServices(IMapper mapper, IUserRepository userRepo, IEncoderServices encoderServices)
		{
			_mapper = mapper;
			_userRepo = userRepo;
			_encoderServices = encoderServices;
		}

		private async Task<User> GetOneByIdOrException(int id)
		{
			var user = await _userRepo.GetOne(u => u.Id == id);
			if (user == null)
			{
				throw new CustomHttpException($"No se encontro el usuario con Id = {id}", HttpStatusCode.NotFound);
			}
			return user;
		}

		public async Task<List<UsersDTO>> GetAll()
		{
			var users = await _userRepo.GetAll();
			return _mapper.Map<List<UsersDTO>>(users);
		}

		public async Task<UserDTO> GetOneById(int id)
		{
			var user = await GetOneByIdOrException(id);
			return _mapper.Map<UserDTO>(user);
		}

		public async Task<User> CreateOne(CreateUserDTO createUserDto)
		{
			var user = _mapper.Map<User>(createUserDto);

			// Hasheo de la contraseña del usuario
			user.Password = _encoderServices.Encode(user.Password);

			await _userRepo.Add(user);
			return user;
		}

		public async Task<User> UpdateOneById(int id, UpdateUserDTO updateUserDto)
		{
			var user = await GetOneByIdOrException(id);

			var userMapped = _mapper.Map(updateUserDto, user);

			await _userRepo.Update(userMapped);

			return userMapped;
		}

		public async Task DeleteOneById(int id)
		{
			var user = await GetOneByIdOrException(id);

			await _userRepo.Delete(user);
		}

		public async Task<User> GetOneByUsernameOrEmail(string? username, string? email)
		{
            
            try
			{
                User user;
                if (email == null && username == null)
				{
					throw new CustomHttpException("Invalid Credentials", HttpStatusCode.BadRequest);
				}

				user = email != null
				   ? await _userRepo.GetOne(u => u.Email == email, "Roles")
					 : await _userRepo.GetOne(u => u.UserName == username, "Roles");
                return user;
            }
            catch(CustomHttpException ex)
			{
                throw new CustomHttpException(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                throw new CustomHttpException("An unexpected error occurred", HttpStatusCode.InternalServerError);
            }

        }

		public async Task<User> UpdateRolesById(int id, List<Role> roles)
		{
			var user = await GetOneByIdOrException(id);

			user.Roles = roles;

			return await _userRepo.Update(user);
		}

		public async Task<List<Role>> GetRolesById(int id)
		{
			var user = await GetOneByIdOrException(id);

			return user.Roles.ToList();
		}
	}
}
