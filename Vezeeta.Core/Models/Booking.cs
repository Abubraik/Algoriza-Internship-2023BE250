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
        [Required]
        public decimal price { get; set; }
        [Required]
        public decimal finalPrice { get; set; }
        [Required]
        public Status status { get; set; }

        [Required]
        public int timeSlotId { get; set; }
        public virtual TimeSlot timeSlot { get; set; }
        [Required]
        public int discountCodeId { get; set; }
        public virtual DiscountCode? discountCode { get; set; }
        [Required]
        public string patientId { get; set; }
        public virtual Patient patient{ get; set; }
        [Required]
        public string doctorId { get; set; }
        public virtual Doctor doctor { get; set; }
        public virtual Feedback? feedback { get; set; }

    }
}
