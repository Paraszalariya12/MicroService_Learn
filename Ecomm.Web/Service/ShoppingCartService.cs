using Ecomm.Web.Models;
using Ecomm.Web.Service.IService;
using Ecomm.Web.Utility;

namespace Ecomm.Web.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        IBaseService _baseservice;
        public ShoppingCartService(IBaseService baseService)
        {
            _baseservice = baseService;
        }
        public async Task<ResponseDto?> ApplyCoupon(CartDto cartDto)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.POST,
                    Data = cartDto,
                    Url = Constants.ShoppingcartBaseUrl + $"/api/shoppingcart/ApplyCoupon"
                });
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new ResponseDto()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "No Result Found !!"
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDto?> CartUpsert(CartDto cartDto)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.POST,
                    Data = cartDto,
                    Url = Constants.ShoppingcartBaseUrl + $"/api/shoppingcart/CartUpsert"
                });
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new ResponseDto()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "No Result Found !!"
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDto?> DeleteCartDetail(int cartDetailId)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.Delete,
                    Url = Constants.ShoppingcartBaseUrl + $"/api/shoppingcart/DeleteCartDetail/{cartDetailId}"
                });
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new ResponseDto()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "No Result Found !!"
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDto?> GetCartByUserId(string UserId)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.GET,
                    Url = Constants.ShoppingcartBaseUrl + $"/api/shoppingcart/GetCart/{UserId}"
                });
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new ResponseDto()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "No Result Found !!"
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
