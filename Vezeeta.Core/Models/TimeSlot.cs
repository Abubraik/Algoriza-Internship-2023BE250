using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public class TimeSlot
    {
        [Key]
        public int tiemSlotId { get; set; }
        [Required]
        public TimeOnly startTime { get; set; }
        [Required]
        public TimeOnly endTime { get; set; }
        [Required]
        public bool isBooked { get; set; }

        [Required]
        public int dayScheduleId { get; set; }
        public virtual DaySchedule daySchedule {  get; set; }

    }
}
