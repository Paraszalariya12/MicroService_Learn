using Ecomm.Web.Models;
using Ecomm.Web.Service.IService;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace Ecomm.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool WithBearer = true)
        {
            try
            {

                HttpClient httpClient = _httpClientFactory.CreateClient("EcomAPI");
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Headers.Add("Accept", "application/json");

                //token
                if (WithBearer)
                {
                    var token = _tokenProvider.GetToken();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                httpRequestMessage.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;
                switch (requestDto.ApiType)
                {
                    case Utility.Constants.APIType.GET:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                    case Utility.Constants.APIType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        break;
                    case Utility.Constants.APIType.PUT:
                        httpRequestMessage.Method = HttpMethod.Put;
                        break;
                    case Utility.Constants.APIType.Delete:
                        httpRequestMessage.Method = HttpMethod.Delete;
                        break;
                    default:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }
                ResponseDto? apiReponseDto = null;
                apiResponse = await httpClient.SendAsync(httpRequestMessage);
                var apicontent = await apiResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(apicontent))
                {
                    apiReponseDto = JsonConvert.DeserializeObject<ResponseDto>(apicontent);
                }

                switch (apiResponse.StatusCode)
                {
                    //case System.Net.HttpStatusCode.Continue:
                    //    return new() { IsSuccess = false, Message = "" };

                    //case System.Net.HttpStatusCode.SwitchingProtocols:
                    //    return new() { IsSuccess = false, Message = "" };
                    //case System.Net.HttpStatusCode.Processing:
                    //    return new() { IsSuccess = false, Message = "" };
                    //case System.Net.HttpStatusCode.EarlyHints:
                    //    return new() { IsSuccess = false, Message = "" };
                    //case System.Net.HttpStatusCode.OK:
                    //    break;
                    //case System.Net.HttpStatusCode.Created:
                    //    break;
                    //case System.Net.HttpStatusCode.Accepted:
                    //    break;
                    //case System.Net.HttpStatusCode.NonAuthoritativeInformation:
                    //    break;
                    //case System.Net.HttpStatusCode.NoContent:
                    //    break;
                    //case System.Net.HttpStatusCode.ResetContent:
                    //    break;
                    //case System.Net.HttpStatusCode.PartialContent:
                    //    break;
                    //case System.Net.HttpStatusCode.MultiStatus:
                    //    break;
                    //case System.Net.HttpStatusCode.AlreadyReported:
                    //    break;
                    //case System.Net.HttpStatusCode.IMUsed:
                    //    break;
                    //case System.Net.HttpStatusCode.Ambiguous:
                    //    break;
                    ////case System.Net.HttpStatusCode.MultipleChoices:
                    ////    break;
                    //case System.Net.HttpStatusCode.Moved:
                    //    break;
                    ////case System.Net.HttpStatusCode.MovedPermanently:
                    ////    break;
                    //case System.Net.HttpStatusCode.Found:
                    //    break;
                    ////case System.Net.HttpStatusCode.Redirect:
                    ////    break;
                    //case System.Net.HttpStatusCode.RedirectMethod:
                    //    break;
                    ////case System.Net.HttpStatusCode.SeeOther:
                    ////    break;
                    //case System.Net.HttpStatusCode.NotModified:
                    //    break;
                    //case System.Net.HttpStatusCode.UseProxy:
                    //    break;
                    //case System.Net.HttpStatusCode.Unused:
                    //    break;
                    //case System.Net.HttpStatusCode.RedirectKeepVerb:
                    //    break;
                    ////case System.Net.HttpStatusCode.TemporaryRedirect:
                    ////    break;
                    //case System.Net.HttpStatusCode.PermanentRedirect:
                    //    break;
                    case System.Net.HttpStatusCode.BadRequest:
                        return new() { IsSuccess = false, Message = apiReponseDto != null ? apiReponseDto.Message : "Bad Request" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = apiReponseDto != null ? apiReponseDto.Message : "Unauthorize" };
                    //case System.Net.HttpStatusCode.PaymentRequired:
                    //    break;
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = apiReponseDto != null ? apiReponseDto.Message : "Access Denied" };
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = apiReponseDto != null ? apiReponseDto.Message : "Not Found" };
                    //case System.Net.HttpStatusCode.MethodNotAllowed:
                    //    break;
                    //case System.Net.HttpStatusCode.NotAcceptable:
                    //    break;
                    //case System.Net.HttpStatusCode.ProxyAuthenticationRequired:
                    //    break;
                    //case System.Net.HttpStatusCode.RequestTimeout:
                    //    break;
                    //case System.Net.HttpStatusCode.Conflict:
                    //    break;
                    //case System.Net.HttpStatusCode.Gone:
                    //    break;
                    //case System.Net.HttpStatusCode.LengthRequired:
                    //    break;
                    //case System.Net.HttpStatusCode.PreconditionFailed:
                    //    break;
                    //case System.Net.HttpStatusCode.RequestEntityTooLarge:
                    //    break;
                    //case System.Net.HttpStatusCode.RequestUriTooLong:
                    //    break;
                    //case System.Net.HttpStatusCode.UnsupportedMediaType:
                    //    break;
                    //case System.Net.HttpStatusCode.RequestedRangeNotSatisfiable:
                    //    break;
                    //case System.Net.HttpStatusCode.ExpectationFailed:
                    //    break;
                    //case System.Net.HttpStatusCode.MisdirectedRequest:
                    //    break;
                    //case System.Net.HttpStatusCode.UnprocessableEntity:
                    //    break;
                    //case System.Net.HttpStatusCode.UnprocessableContent:
                    //    break;
                    //case System.Net.HttpStatusCode.Locked:
                    //    break;
                    //case System.Net.HttpStatusCode.FailedDependency:
                    //    break;
                    //case System.Net.HttpStatusCode.UpgradeRequired:
                    //    break;
                    //case System.Net.HttpStatusCode.PreconditionRequired:
                    //    break;
                    //case System.Net.HttpStatusCode.TooManyRequests:
                    //    break;
                    //case System.Net.HttpStatusCode.RequestHeaderFieldsTooLarge:
                    //    break;
                    //case System.Net.HttpStatusCode.UnavailableForLegalReasons:
                    //    break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = apiReponseDto != null ? apiReponseDto.Message : "Internal Server Error" };
                    //case System.Net.HttpStatusCode.NotImplemented:
                    //    break;
                    //case System.Net.HttpStatusCode.BadGateway:
                    //    break;
                    //case System.Net.HttpStatusCode.ServiceUnavailable:
                    //    break;
                    //case System.Net.HttpStatusCode.GatewayTimeout:
                    //    break;
                    //case System.Net.HttpStatusCode.HttpVersionNotSupported:
                    //    break;
                    //case System.Net.HttpStatusCode.VariantAlsoNegotiates:
                    //    break;
                    //case System.Net.HttpStatusCode.InsufficientStorage:
                    //    break;
                    //case System.Net.HttpStatusCode.LoopDetected:
                    //    break;
                    //case System.Net.HttpStatusCode.NotExtended:
                    //    break;
                    //case System.Net.HttpStatusCode.NetworkAuthenticationRequired:
                    //    break;
                    default:
                        return apiReponseDto;
                }
            }
            catch (Exception ex)
            {
                return new() { IsSuccess = false, Message = ex.Message };
            }

        }
    }
}
