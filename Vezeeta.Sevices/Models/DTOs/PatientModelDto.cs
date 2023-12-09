using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Service.Services;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class PatientModelDto
    {
        [Required]
        public string? Image { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [ValidEnumValue]
        public Gender Gender { get; set; }
        [Required]
        [ValidEnumValue]
        public DateOnly DateOfBirth { get; set; }
        public List<BookingsInfoDto> Bookings { get; set; }
    }
}
