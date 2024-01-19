namespace Ecomm.Web.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? PhoneNumber { get; set; }
        public DateTime? DateofBirth { get; set; }
    }
}
