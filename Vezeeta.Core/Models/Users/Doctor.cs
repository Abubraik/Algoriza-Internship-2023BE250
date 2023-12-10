using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Core.Models.Users
{
    public class Doctor : ApplicationUser
    {
        [Required]
        public int SpecializationId { get; set; }
        [Required]
        public required string Photo { get; set; }
        public virtual required Specialization Specialization { get; set; }
        public virtual Appointment Appointments { get; set; }
        public virtual List<Booking>? Bookings { get; set; }
        public virtual List<Feedback>? Feedbacks { get; set; }

    }
}
