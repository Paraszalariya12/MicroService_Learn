using Ecomm.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Web.Service.IService
{
    public interface IShoppingCartService
    {
        Task<ResponseDto?> GetCartByUserId(string UserId);
        Task<ResponseDto?> ApplyCoupon(CartDto cartDto);
        Task<ResponseDto?> CartUpsert(CartDto cartDto);
        Task<ResponseDto?> DeleteCartDetail(int cartDetailId);

    }
}
