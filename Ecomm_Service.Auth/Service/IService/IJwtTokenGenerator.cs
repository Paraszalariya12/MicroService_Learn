using Ecomm_Service.AuthAPI.Model;

namespace Ecomm_Service.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
