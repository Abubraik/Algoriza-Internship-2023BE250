using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Sevices.Helpers;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class PatientModelDto
    {
        [Required]
        public string? Image { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [ValidEnumValue]
        public Gender Gender { get; set; }
        [Required]
        [ValidEnumValue]
        public DateOnly DateOfBirth { get; set; }
        public List<BookingsInfoDto>? Bookings { get; set; }
        public PatientModelDto()
        {
            
        }
        public PatientModelDto(Patient patient,Booking booking)
        {
            Image = patient.Photo;
            FullName = $"{ patient.FirstName} {patient.LastName}";
            Email= patient.Email;
            PhoneNumber = patient.PhoneNumber;
            Gender= patient.Gender;
            DateOfBirth = patient.DateOfBirth;
            Bookings = new List<BookingsInfoDto> { new BookingsInfoDto(new DoctorInfoDto(booking.Doctor),booking) };
        }
    }
}
