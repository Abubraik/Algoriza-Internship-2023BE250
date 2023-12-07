using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Vezeeta.Service.Services;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class AccountModelDto
    {
        [Required(ErrorMessage = "First Name is required"),MinLength(3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [JsonIgnore]
        public string? _password{ get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required and minimum length = 6"), MinLength(10),]
        //[DataType(DataType.Password)]
        //[JsonIgnore]
        //public string password { get; set; }

        public string? Photo { get; set;}
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [ValidEnumValue]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateOnly DateOfBirth { get; set; }

    }
}
