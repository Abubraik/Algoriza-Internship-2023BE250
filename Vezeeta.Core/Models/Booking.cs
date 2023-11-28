using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models.Users;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Models
{
    public class Booking
    {
        [Key]
        public int bookingId { get; set; }
        public decimal price { get; set; }
        public decimal finalPrice { get; set; }
        public Status status { get; set; }

        public int timeSlotId { get; set; }
        public TimeSlot timeSlot { get; set; }
        //public int? discountCodeId { get; set; }
        public DiscountCode? discountCode { get; set; }
        //public int patientId { get; set; }
        public Patient patient{ get; set; }
        //public int doctorId { get; set; }
        public Doctor doctor { get; set; }
        public Feedback? feedback { get; set; }

    }
}
