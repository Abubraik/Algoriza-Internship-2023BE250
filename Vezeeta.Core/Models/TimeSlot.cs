using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public class TimeSlot
    {
        [Key]
        public int TiemSlotId { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        [DefaultValue("false")]
        public bool IsBooked { get; set; }

        [Required]
        public int DayScheduleId { get; set; }
        public DaySchedule DaySchedule { get; set; }

    }
}
