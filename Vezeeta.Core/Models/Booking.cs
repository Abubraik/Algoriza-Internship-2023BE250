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
        public int BookingId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal FinalPrice { get; set; }
        [Required]
        public Status Status { get; set; }

        [Required]
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
        
        public int? DiscountCodeId { get; set; }
        public DiscountCode? DiscountCode { get; set; }
        [Required]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }
        [Required]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Feedback? Feedback { get; set; }

    }
}
