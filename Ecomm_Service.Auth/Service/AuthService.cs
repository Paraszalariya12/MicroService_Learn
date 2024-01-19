using Ecomm_Service.AuthAPI.Data;
using Ecomm_Service.AuthAPI.Model;
using Ecomm_Service.AuthAPI.Model.Dto;
using Ecomm_Service.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Ecomm_Service.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string Email, string RoleName)
        {
            var user = _db.applicationUsers.FirstOrDefault(u => u.UserName == Email);
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(RoleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(RoleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, RoleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            var result = _db.applicationUsers.FirstOrDefault(a => a.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            if (result == null)
            {
                return new LoginResponseDto() { userDto = null, Token = "" };
            }
            else
            {
                bool iavalid = await _userManager.CheckPasswordAsync(result, loginRequestDto.Password);
                if (!iavalid)
                {
                    return new LoginResponseDto() { userDto = null, Token = "" };
                }

                loginResponseDto.Token = _jwtTokenGenerator.GenerateToken(result);

                loginResponseDto.userDto = new()
                {
                    DateofBirth = result.DateofBirth,
                    Email = result.Email,
                    Id = result.Id,
                    Name = result.Name,
                    PhoneNumber = result.PhoneNumber
                };
                return loginResponseDto;
            }
        }

        public async Task<string> Register(RegistrationRequestDto loginRequestDto)
        {
            string ErrorMessage = "";
            ApplicationUser user = new()
            {
                Email = loginRequestDto.Email,
                Name = loginRequestDto.Name,
                DateofBirth = loginRequestDto.DateofBirth,
                PhoneNumber = loginRequestDto.PhoneNumber,
                UserName = loginRequestDto.Email

            };

            try
            {
                var result = await _userManager.CreateAsync(user, loginRequestDto.Password);
                if (result.Succeeded)
                {
                    var usertoreturn = _db.applicationUsers.First(a => a.Email == loginRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = loginRequestDto.Email,
                        Name = loginRequestDto.Name,
                        DateofBirth = loginRequestDto.DateofBirth,
                        PhoneNumber = loginRequestDto.PhoneNumber,
                    };
                    return ErrorMessage;
                }
                else
                {
                    return result.Errors.First().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
