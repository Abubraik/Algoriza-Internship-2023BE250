using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class BookingsDTO
    {
        public string Image { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public string Day { get; set; }
        public TimeOnly Time { get; set; }
        public decimal Price { get; set; }
        public string DiscountCode { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; }
    }
}
