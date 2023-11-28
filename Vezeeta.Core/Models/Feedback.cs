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
        public string feedback { get; set; }
        public int rating { get; set; }

        public int bookingId { get; set; }
        public Booking booking { get; set; }
        //public int patientId { get; set; }
        public Patient patient { get; set; }
        //public int doctorId { get; set; }
        public Doctor doctor { get; set; }  
    }
}
