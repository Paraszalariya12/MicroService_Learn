using Ecomm_Service.ShoppingCartAPI.Models.DTO;

namespace Ecomm_Service.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string Couponcode);
    }
}
