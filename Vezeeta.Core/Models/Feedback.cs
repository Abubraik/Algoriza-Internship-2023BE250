using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        [Required]
        public string PatientFeedback { get; set; }
        [Required]
        public int Rating { get; set; }

        [Required]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        [Required]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }
        [Required]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
