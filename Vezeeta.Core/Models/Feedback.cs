using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        [Required]
        public required string PatientFeedback { get; set; }
        [Required]
        public int Rating { get; set; }

        [Required]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        [Required]
        public required string PatientId { get; set; }
        public required virtual Patient Patient { get; set; }
        [Required]
        public required string DoctorId { get; set; }
        public required virtual Doctor Doctor { get; set; }
    }
}
