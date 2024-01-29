using Ecomm.Web.Models;

namespace Ecomm.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto> CreateAsync(ProductDto coupon);
        Task<ResponseDto> UpdateAsync(ProductDto coupon);
        Task<ResponseDto> DeleteAsync(int id);
        Task<ResponseDto> GetAsync(int id);
        Task<ResponseDto> GetAllAsync();
        //Task<ResponseDto> GetByCodeAsync(string couponCode);
    }
}
