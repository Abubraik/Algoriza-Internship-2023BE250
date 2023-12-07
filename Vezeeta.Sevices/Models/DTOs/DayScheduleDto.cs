using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Service.Services;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class DayScheduleDto
    {
        [Required]
        [ValidEnumValue]
        public Days DayOfWeek { get; set; }
        [Required]
        public List<TimeSlotDto> TimeSlots { get; set; }
    }

}
