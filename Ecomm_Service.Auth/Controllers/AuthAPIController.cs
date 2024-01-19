using Ecomm_Service.AuthAPI.Model.Dto;
using Ecomm_Service.AuthAPI.Models.DTO;
using Ecomm_Service.AuthAPI.Service;
using Ecomm_Service.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Service.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        public ResponseDto response;
        private readonly IAuthService _authService;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegistrationRequestDto objuser)
        {
            var Errormessage = await _authService.Register(objuser);
            if (!String.IsNullOrWhiteSpace(Errormessage))
            {
                response = new()
                {
                    IsSuccess = false,
                    Message = Errormessage,
                    Data = null
                };

                return Ok(response);
            }
            else
            {
                response = new()
                {
                    IsSuccess = true,
                    Message = "",
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginRequestDto objuser)
        {
            var loginResponsedto = await _authService.Login(objuser);
            if (loginResponsedto == null || loginResponsedto.userDto == null)
            {
                response = new()
                {
                    IsSuccess = false,
                    Message = "Username Or Password is incorrect..!!"
                };
                return BadRequest(response);
            }
            else
            {
                response = new()
                {
                    IsSuccess = true,
                    Message = "",
                    Data = loginResponsedto
                };
                return Ok(response);
            }
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(RegistrationRequestDto objuser)
        {
            var loginResponsedto = await _authService.AssignRole(objuser.Email, objuser.Role);
            if (!loginResponsedto)
            {
                response = new()
                {
                    IsSuccess = false,
                    Message = "Error encountered..!!"
                };
                return BadRequest(response);
            }
            else
            {
                response = new()
                {
                    IsSuccess = true,
                    Message = ""
                };
                return Ok(response);
            }
        }
    }
}
