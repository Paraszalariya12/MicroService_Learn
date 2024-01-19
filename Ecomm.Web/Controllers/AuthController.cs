using Ecomm.Web.Models;
using Ecomm.Web.Service.IService;
using Ecomm.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Ecomm.Web.Controllers
{
    public class AuthController : Controller
    {
        IAuthService _authService;
        ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;

        }

        public async Task<IActionResult> Register()
        {
            var roleitems = new List<SelectListItem>() {
                new SelectListItem {Text=Constants.Role_Admin,Value=Constants.Role_Admin },
                new SelectListItem {Text=Constants.Role_Customer,Value=Constants.Role_Customer }
            };
            ViewBag.RoleList = roleitems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result != null && result.IsSuccess)
            {
                model.Role = Constants.Role_Customer;

                var result_Assignrole = await _authService.AssignRoleAsync(model);
                if (result_Assignrole != null && result_Assignrole.IsSuccess)
                {
                    TempData["success"] = $"User {model.Name} successfully Created.";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["error"] = result != null ? result.Message : "Internal Error..!!";
                }
            }
            else
            {
                TempData["error"] = result != null ? result.Message : "Internal Error..!!";
            }
            var roleitems = new List<SelectListItem>() {
                new SelectListItem {Text=Constants.Role_Admin,Value=Constants.Role_Admin },
                new SelectListItem {Text=Constants.Role_Customer,Value=Constants.Role_Customer }
                 };
            ViewBag.RoleList = roleitems;
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model);
                if (result != null && result.IsSuccess)
                {
                    LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(result?.Data.ToString());

                    SignInUser(loginResponseDto);
                    _tokenProvider.SetToken(loginResponseDto?.Token);

                    return RedirectToAction("CouponIndex", "Coupon");
                }
                else
                {
                    TempData["error"] = result != null ? result.Message : "Internal Error..!!";
                }
            }
            else
            {

            }
            return View();
        }

        private async void SignInUser(LoginResponseDto model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtdata = tokenHandler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwtdata.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwtdata.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, jwtdata.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.NameId).Value));
            var prinicipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, prinicipal);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Profile()
        {
            return View();
        }
    }
}
