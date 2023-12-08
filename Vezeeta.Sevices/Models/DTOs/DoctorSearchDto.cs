using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class DoctorSearchDto
    {
        public required IFormFile Image { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Specialization { get; set; }
        public decimal Price { get; set; }
        public Gender Gender { get; set; }
        List<DayScheduleDto> Schedule { get; set; }


    }
}
