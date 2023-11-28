using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }


        [Required]
        public Gender gender { get; set; }
        [Required]
        public DateTime dateOfBirth { get; set; }
        [Required]
        public string photoPath { get; set; }
        
    }
}
