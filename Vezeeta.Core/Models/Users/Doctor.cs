using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models.Users
{
    public class Doctor : ApplicationUser
    {
        [Required]
        public int specializationId { get; set; }
        [Required]
        public required string photoPath { get; set; }
        public virtual Specialization specialization { get; set; }

        public virtual List<Appointment> appointments { get; set; }
        public virtual List<Booking> bookings { get; set; }
        public virtual List<Feedback> feedbacks { get; set; }

    }
}
