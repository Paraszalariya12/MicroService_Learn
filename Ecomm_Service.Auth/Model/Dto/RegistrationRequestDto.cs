namespace Ecomm_Service.AuthAPI.Model.Dto
{
    public class RegistrationRequestDto
    {
        public string Name { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? PhoneNumber { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
