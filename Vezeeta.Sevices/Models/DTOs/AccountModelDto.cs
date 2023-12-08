using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Web.Http.Description;
using Vezeeta.Service.Services;
using Vezeeta.Sevices.Helpers;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class AccountModelDto
    {
        [Required(ErrorMessage = "First Name is required"), MinLength(3)]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required,Phone]
        public required string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [ValidEnumValue]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateOnly DateOfBirth { get; set; }

    }
}
