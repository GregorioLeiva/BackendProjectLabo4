using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_Labo4.Models.Auth.Dto;
using ProyectoFinal_Labo4.Models.Auth;
using ProyectoFinal_Labo4.Services;
using ProyectoFinal_Labo4.Utils.Exceptions;
using System.Net;

namespace ProyectoFinal_Labo4.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthServices _authServices;
		private readonly UserServices _userService;
		private readonly IEncoderServices _encoderServices;
		private readonly IMapper _mapper;

		public AuthController(AuthServices authServices, UserServices userService, IMapper mapper, IEncoderServices encoderServices)
		{
			_authServices = authServices;
			_userService = userService;
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
				var user = await _userService.GetOneByUsernameOrEmail(login.Username, login.Email);

				var passwordMatch = _encoderServices.Verify(login.Password, user.Password);

				if (!passwordMatch)
				{
					throw new CustomHttpException("Invalid Credentials", HttpStatusCode.BadRequest);
				}

				var token = _authServices.GenerateJwtToken(user);

				return Ok(new LoginResponseDTO { Token = token });
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
	}
}
