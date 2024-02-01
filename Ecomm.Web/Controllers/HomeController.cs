using Ecomm.Web.Models;
using Ecomm.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace Ecomm.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IProductService _productService;
        IShoppingCartService _shoppingCartService;
        public HomeController(IProductService productService, IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = new();

            ResponseDto? response = await _productService.GetAllAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDto? list = new();
            ResponseDto? response = await _productService.GetAsync(productId);

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        [HttpPost]
        [ActionName("ProductDetails")]
        [Authorize]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {

            CartDto cart = new CartDto()
            {
                CartHeader = new CartHeaderDto()
                {
                    UserId = User.Claims.Where(a => a.Type == "nameid").FirstOrDefault().Value
                }
            };



            CartDetailDto cartDetail = new CartDetailDto();
            cartDetail.Count = productDto.Count;
            cartDetail.ProductId = productDto.ProductId;
            cart.CartDetails = new List<CartDetailDto>() { cartDetail
            };

            ResponseDto? response = await _shoppingCartService.CartUpsert(cart);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item hass been added to shopping cart.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}