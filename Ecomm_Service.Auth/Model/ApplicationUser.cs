using Microsoft.AspNetCore.Identity;

namespace Ecomm_Service.AuthAPI.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public DateTime? DateofBirth { get; set; }

    }
}
