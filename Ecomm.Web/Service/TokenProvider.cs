using Ecomm.Web.Service.IService;
using Ecomm.Web.Utility;

namespace Ecomm.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(Constants.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? isexists = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Constants.TokenCookie, out token);
            return isexists == null ? null : token;
        }

        public void SetToken(string? token)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.Secure = true;
            _contextAccessor.HttpContext?.Response.Cookies.Append(Constants.TokenCookie, token, options);
        }
    }
}
