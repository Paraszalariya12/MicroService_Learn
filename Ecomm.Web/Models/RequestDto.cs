using static Ecomm.Web.Utility.Constants;

namespace Ecomm.Web.Models
{
    public class RequestDto
    {
        public APIType ApiType { get; set; } = APIType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
