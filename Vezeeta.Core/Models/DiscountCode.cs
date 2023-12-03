using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Models
{
    public class DiscountCode
    {
        [Key]
        public int discountCodeId { get; set; }
        [Required]
        public string discountCode { get; set; }
        [Required]
        public virtual DiscountType discountType{ get; set; }
        [Required]
        public decimal discountValue { get; set; }
        [Required]
        public int numberOfRequiredBookings { get; set; }
        [Required]
        public bool isValid { get; set; }

        public virtual List<Booking> bookings { get; set; }

    }
}
