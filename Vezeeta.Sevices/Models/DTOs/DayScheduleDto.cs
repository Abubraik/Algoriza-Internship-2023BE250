using System.ComponentModel.DataAnnotations;
using Vezeeta.Sevices.Helpers;
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
