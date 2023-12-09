using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class CreateDoctorModelDto : AccountModelDto
    {

        //[Required]
        //public string id { get; set; }
        //[Required(ErrorMessage = "First Name is required"), MinLength(3)]
        //public string firstName { get; set; }
        //[Required(ErrorMessage = "Last Name is required")]
        //public string lastName { get; set; }
        //[Required(ErrorMessage = "Email address is required")]
        //[EmailAddress]
        //public string email { get; set; }
        //public string phoneNumber { get; set; }
        //[Required(ErrorMessage = "Gender is required")]
        //public Gender gender { get; set; }

        //[Required(ErrorMessage = "Date Of Birth is required")]
        //public DateOnly dateOfBirth { get; set; }
        [Required]
        public required IFormFile Photo { get; set; }
        [Required]
        public required string Specialization { get; set; }
    }
}
