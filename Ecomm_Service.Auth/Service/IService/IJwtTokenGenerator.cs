using Ecomm_Service.AuthAPI.Model;
using System.Data;

namespace Ecomm_Service.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser,IEnumerable<string> Roles);
    }
}
