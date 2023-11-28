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
        //public int specializationId { get; set; }
        public Specialization specialization { get; set; }

        public List<Appointment> appointments { get; set; }
        public List<Booking> bookings { get; set; }
        public List<Feedback> feedbacks { get; set; }

    }
}
