using System.ComponentModel.DataAnnotations;

namespace Ecomm.Web.Models
{
    public class RegistrationRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual string? Email { get; set; }
        [Required]
        public virtual string? PhoneNumber { get; set; }
        public DateTime? DateofBirth { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
