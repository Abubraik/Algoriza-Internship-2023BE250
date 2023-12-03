using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.DTOs
{
    public class BookingsDTO
    {
        public string image { get; set; }
        public string doctorName { get; set; }
        public string specialization { get; set; }
        public string day {  get; set; }
        public TimeOnly time { get; set; }
        public decimal price { get; set; }
        public string discountCode { get; set; }
        public decimal finalPrice { get; set; }
        public string status { get; set; }
    }
}
