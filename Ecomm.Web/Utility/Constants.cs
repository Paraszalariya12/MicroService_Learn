namespace Ecomm.Web.Utility
{
    public class Constants
    {
        public enum APIType { GET, POST, PUT, Delete }

        public static string CouponAPIBaseUrl { get; set; }
        public static string AuthAPIBaseUrl { get; set; }
        public static string ProductAPIBaseUrl { get; set; }

        public static string Role_Admin = "Admin";
        public static string Role_Customer = "Customer";

        public static string TokenCookie = "JwtCookie";

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
