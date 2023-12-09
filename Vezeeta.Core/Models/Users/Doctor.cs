using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Core.Models.Users
{
    public class Doctor : ApplicationUser
    {
        [Required]
        public int SpecializationId { get; set; }
        [Required]
        public required string Photo { get; set; }
        public  Specialization? Specialization { get; set; }
        public Appointment Appointments { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Feedback> Feedbacks { get; set; }

    }
}
