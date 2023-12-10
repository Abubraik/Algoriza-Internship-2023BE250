using System.ComponentModel.DataAnnotations;
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
        public virtual TimeSlot TimeSlot { get; set; }
        
        public int? DiscountCodeId { get; set; }
        public virtual DiscountCode? DiscountCode { get; set; }
        [Required]
        public string PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        [Required]
        public string DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Feedback? Feedback { get; set; }

    }
}
