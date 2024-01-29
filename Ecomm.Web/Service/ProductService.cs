﻿using Ecomm.Web.Models;
using Ecomm.Web.Service.IService;
using Ecomm.Web.Utility;

namespace Ecomm.Web.Service
{
    public class ProductService : IProductService
    {
        IBaseService _baseservice;
        public ProductService(IBaseService baseService)
        {
            _baseservice = baseService;
        }
        public async Task<ResponseDto> CreateAsync(ProductDto coupon)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.POST,
                    Data = coupon,
                    Url = Constants.ProductAPIBaseUrl + $"/api/product"
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

        public async Task<ResponseDto> DeleteAsync(int id)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.Delete,
                    Url = Constants.ProductAPIBaseUrl + $"/api/product/{id}"

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

        public async Task<ResponseDto> GetAllAsync()
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.GET,
                    Url = Constants.ProductAPIBaseUrl + "/api/product"

                });
                if (result != null && result.Data != null)
                {
                    return result;
                }
                else
                {
                    return new ResponseDto()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = result != null ? result.Message : "No Data Found..!!"
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

        public async Task<ResponseDto> GetAsync(int id)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.GET,
                    Url = Constants.ProductAPIBaseUrl + $"/api/product/{id}"

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
        public async Task<ResponseDto> UpdateAsync(ProductDto coupon)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.PUT,
                    Url = Constants.ProductAPIBaseUrl + "/api/product",
                    Data = coupon,
                    ContentType = Constants.ContentType.MultipartFormData

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
