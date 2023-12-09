using System.ComponentModel.DataAnnotations;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Models
{
    public class DiscountCode
    {
        [Key]
        public int DiscountCodeId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public DiscountType DiscountType { get; set; }
        [Required]
        public decimal DiscountValue { get; set; }
        [Required]
        public int NumberOfRequiredBookings { get; set; }
        [Required]
        public bool IsValid { get; set; } = true;

        public List<Booking> Bookings { get; set; }

    }
}
