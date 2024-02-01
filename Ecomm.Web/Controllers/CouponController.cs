using Ecomm.Web.Models;
using Ecomm.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace Ecomm.Web.Controllers
{
    [Authorize]
    public class CouponController : Controller
    {
        public readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> lst = new();
            ResponseDto? responseDto = await _couponService.GetAllAsync();
            if (responseDto != null && responseDto.IsSuccess)
            {
                lst = JsonConvert.DeserializeObject<List<CouponDto>>((string)responseDto?.Data);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(lst);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto obj)
        {
            try
            {
                var objresult = await _couponService.CreateAsync(obj);
                if (objresult.IsSuccess)
                {
                    TempData["success"] = "New Coupon Successfully Created.";
                    return RedirectToAction("CouponIndex");
                }
                else
                {
                    TempData["error"] = objresult.Message;
                    ModelState.AddModelError("", objresult.Message);
                    return View(obj);
                }

            }
            catch (Exception ex)
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> CouponDelete(int CouponId)
        {
            var result = await _couponService.GetAsync(CouponId);
            if (result != null && result.IsSuccess)
            {
                var objdeleteresponse = await _couponService.DeleteAsync(CouponId);
                if (objdeleteresponse.IsSuccess)
                {
                    TempData["success"] = "Coupon Deleted Sucessfully..!!";
                    return RedirectToAction("CouponIndex");
                }
                else
                {
                    TempData["error"] = objdeleteresponse.Message;
                    return RedirectToAction("CouponIndex");
                }
            }
            else
            {
                TempData["error"] = "No Data Found..!!";
                return RedirectToAction("CouponIndex");
            }
        }

    }
}
