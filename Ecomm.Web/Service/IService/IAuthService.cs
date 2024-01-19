using Ecomm.Web.Models;

namespace Ecomm.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto loginRequestDto);
        Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto loginRequestDto);
    }
}
