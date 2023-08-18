using Ecomm.Web.Models;

namespace Ecomm.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto> CreateAsync(CouponDto coupon);
        Task<ResponseDto> UpdateAsync(CouponDto coupon);
        Task<ResponseDto> DeleteAsync(int id);
        Task<ResponseDto> GetAsync(int id);
        Task<ResponseDto> GetAllAsync();
        Task<ResponseDto> GetByCodeAsync(string couponCode);
    }
}
