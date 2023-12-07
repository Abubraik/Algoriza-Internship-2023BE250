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
        public int SpecializationId { get; set; }
        [Required]
        public required string Photo { get; set; }
        public  Specialization? Specialization { get; set; }

        public List<Appointment> Appointments { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Feedback> Feedbacks { get; set; }

    }
}
