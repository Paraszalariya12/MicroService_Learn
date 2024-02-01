using Ecomm.Web.Models;
using Ecomm.Web.Service;
using Ecomm.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecomm.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
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
        public async Task<IActionResult> ProductCreate()
        {
            ProductDto objproductdto = new();
            objproductdto.ProductId = 0;
            return View(objproductdto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response;

                if (model.ProductId == 0)
                {
                    response = await _productService.CreateAsync(model);
                }
                else
                {
                    //model.ImageUrl=model.ImageUrl
                    response = await _productService.UpdateAsync(model);
                }

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = model.ProductId == 0 ? "Product created successfully" : "Product Updated successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDto? response = await _productService.GetAsync(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response?.Data));
                return View("ProductCreate", model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var result = await _productService.GetAsync(productId);
            if (result != null && result.IsSuccess)
            {
                var objdeleteresponse = await _productService.DeleteAsync(productId);
                if (objdeleteresponse.IsSuccess)
                {
                    TempData["success"] = "Product Deleted Sucessfully..!!";
                    return RedirectToAction("ProductIndex");
                }
                else
                {
                    TempData["error"] = objdeleteresponse.Message;
                    return RedirectToAction("ProductIndex");
                }
            }
            else
            {
                TempData["error"] = "No Data Found..!!";
                return RedirectToAction("ProductIndex");
            }
        }
    }
}
