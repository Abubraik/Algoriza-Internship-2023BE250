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
        public int feedbackId { get; set; }
        [Required]
        public string feedback { get; set; }
        [Required]
        public int rating { get; set; }

        [Required]
        public int bookingId { get; set; }
        public virtual Booking booking { get; set; }
        [Required]
        public string patientId { get; set; }
        public virtual Patient patient { get; set; }
        [Required]
        public string doctorId { get; set; }
        public virtual Doctor doctor { get; set; }  
    }
}
