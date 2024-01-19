using Ecomm.Web.Models;
using Ecomm.Web.Service.IService;
using Ecomm.Web.Utility;

namespace Ecomm.Web.Service
{
    public class AuthService : IAuthService
    {
        IBaseService _baseservice;
        public AuthService(IBaseService baseService)
        {
            _baseservice = baseService;
        }

        public async Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto loginRequestDto)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.POST,
                    Data = loginRequestDto,
                    Url = Constants.AuthAPIBaseUrl + $"/api/auth/AssignRole"

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

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.POST,
                    Data = loginRequestDto,
                    Url = Constants.AuthAPIBaseUrl + $"/api/auth/Login"

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

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto loginRequestDto)
        {
            try
            {
                var result = await _baseservice.SendAsync(new RequestDto
                {
                    AccessToken = "",
                    ApiType = Constants.APIType.POST,
                    Data = loginRequestDto,
                    Url = Constants.AuthAPIBaseUrl + $"/api/auth/Register"

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
