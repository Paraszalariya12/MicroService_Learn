using Ecomm_Service.AuthAPI.Model.Dto;

namespace Ecomm_Service.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<string> Register(RegistrationRequestDto loginRequestDto);
        Task<bool> AssignRole(string Email,string RoleName);
    }
}
