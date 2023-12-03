using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Models.Users
{
    public class AccountModel
    {
        [Required(ErrorMessage = "First Name is required"),MinLength(3)]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }
        //[JsonIgnore]
        //public string? fullName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string email { get; set; }

        //[Required(ErrorMessage = "Password is required and minimum length = 6"), MinLength(10),]
        //[DataType(DataType.Password)]
        //[JsonIgnore]
        //public string password { get; set; }

        public string? image { get; set;}
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Gender gender { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateOnly dateOfBirth { get; set; }

    }
}
