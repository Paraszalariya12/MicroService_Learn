namespace Ecomm_Service.ShoppingCartAPI.Models.DTO
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
