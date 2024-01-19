namespace Ecomm_Service.AuthAPI.Model.Dto
{
    public class LoginResponseDto
    {
        public UserDto userDto { get; set; }
        public string Token { get; set; }

    }
}
