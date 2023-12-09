using System.ComponentModel.DataAnnotations;
using Vezeeta.Service.Services;
using Vezeeta.Sevices.Helpers;
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
        [Required, Range(1, 100)]
        public decimal DiscountValue { get; set; }
        [Required, Range(1, 100)]
        public int NumberOfRequiredBookings { get; set; }
    }
}
