using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Sevices.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
