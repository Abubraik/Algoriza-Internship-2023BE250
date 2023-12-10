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
        public required Booking Booking { get; set; }
        [Required]
        public required string PatientId { get; set; }
        public required Patient Patient { get; set; }
        [Required]
        public required string DoctorId { get; set; }
        public required Doctor Doctor { get; set; }
    }
}
