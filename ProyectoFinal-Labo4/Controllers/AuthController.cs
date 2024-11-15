using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_Labo4.Models.Auth.Dto;
using ProyectoFinal_Labo4.Models.Auth;
using ProyectoFinal_Labo4.Services;
using ProyectoFinal_Labo4.Utils.Exceptions;
using System.Net;
using ProyectoFinal_Labo4.Models.Role;
using ProyectoFinal_Labo4.Models.User.Dto;
using ProyectoFinal_Labo4.Enums;
using ProyectoFinal_Labo4.Models.User;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ProyectoFinal_Labo4.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthServices _authServices;
		private readonly UserServices _userServices;
		private readonly RoleServices _roleServices;
		private readonly IEncoderServices _encoderServices;
		private readonly IMapper _mapper;

		public AuthController(AuthServices authServices, UserServices userServices, RoleServices roleServices, IMapper mapper, IEncoderServices encoderServices)
		{
			_authServices = authServices;
			_userServices = userServices;
			_roleServices = roleServices;
			_mapper = mapper;
			_encoderServices = encoderServices;
		}

		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(CustomMessage), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] Login login)
		{
			try
			{
				var user = await _userServices.GetOneByUsernameOrEmail(login.Username, login.Email);

                var passwordMatch = _encoderServices.Verify(login.Password, user.Password);

				if (!passwordMatch)
				{
					throw new CustomHttpException("Invalid Credentials", HttpStatusCode.BadRequest);
				}

				var token = _authServices.GenerateJwtToken(user);

				var userMapped = _mapper.Map<UserLoginResponseDTO>(user);

				return Ok(new LoginResponseDTO { Token = token, User = userMapped });
			}
			catch (CustomHttpException ex)
			{
				return StatusCode((int)ex.StatusCode, new CustomMessage(ex.Message));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new CustomMessage(ex.Message));
			}
		}

		[HttpPost("register")]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(CustomMessage), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<UserDTO>> Register([FromBody] CreateUserDTO register)
		{
			try
			{
				var user = await _userServices.GetOneByUsernameOrEmail(register.UserName, register.Email);

				if (user != null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new CustomMessage("User already exists"));
				}

				var userCreated = await _userServices.CreateOne(register);

				var defaultRole = await _roleServices.GetOneByName(ROLES.USER);

				await _userServices.UpdateRolesById(userCreated.Id, new List<Role> { defaultRole });

				return Created("Register User", userCreated);
			}
			catch (CustomHttpException ex)
			{
				return StatusCode((int)ex.StatusCode, new CustomMessage(ex.Message));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new CustomMessage(ex.Message));
			}
		}
	

		[HttpPut("roles/user/{id}")]
        [Authorize(Roles = ROLES.ADMIN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(CustomMessage), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<User>> Put(int id, [FromBody] UpdateUserRolesDTO updaterolesdto)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var roles = await _roleServices.GetManyByIds(updaterolesdto.RoleIds);
                var userUpdated = await _userServices.UpdateRolesById(id, roles);
                return Ok(userUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }
	}
}
