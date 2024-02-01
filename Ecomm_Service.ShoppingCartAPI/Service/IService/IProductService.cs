using Ecomm_Service.ShoppingCartAPI.Models.dto;

namespace Ecomm_Service.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
