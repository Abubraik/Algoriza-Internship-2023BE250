using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Service.Services;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Services.Models.DTOs
{
    public class DiscountCodeDto
    {
        [Required]
        public string Code { get; set; }
        [Required] 
        [ValidEnumValue]
        public DiscountType DiscountType { get; set; }
        [Required,Range(1,100)]
        public decimal DiscountValue { get; set; }
        [Required, Range(1, 100)]
        public int NumberOfRequiredBookings { get; set; }
    }
}
