using Ecomm.Web.Models;
using Ecomm.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Ecomm.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartdetailsByUser());
        }

        private async Task<CartDto> LoadCartdetailsByUser()
        {
            CartDto cartDto = new();
            try
            {
                var userid = User.Claims.Where(a => a.Type == JwtRegisteredClaimNames.NameId).FirstOrDefault().Value;

                ResponseDto? objresponse = await _shoppingCartService.GetCartByUserId(userid);
                if (objresponse != null && objresponse.IsSuccess)
                {
                    cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(objresponse.Data));
                    return cartDto;
                }
            }
            catch (Exception ex)
            {
                return new CartDto();
            }
            return new CartDto();
        }


        public async Task<IActionResult> RemoveItem(int CartDetailsId)
        {
            var userid = User.Claims.Where(a => a.Type == JwtRegisteredClaimNames.NameId).FirstOrDefault().Value;

            ResponseDto? objresponse = await _shoppingCartService.DeleteCartDetail(CartDetailsId);
            if (objresponse != null && objresponse.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            ResponseDto? objresponse = await _shoppingCartService.ApplyCoupon(cartDto);
            if (objresponse != null && objresponse.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = "";
            ResponseDto? objresponse = await _shoppingCartService.ApplyCoupon(cartDto);
            if (objresponse != null && objresponse.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }




    }
}
