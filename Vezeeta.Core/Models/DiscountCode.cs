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
        public string discountCode { get; set; }
        public DiscountType discountType{ get; set; }
        public decimal discountValue { get; set; }
        public int numberOfRequiredBooking { get; set; }
        public bool isValid { get; set; }

    }
}
