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
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool isBooked { get; set; }

        public int dayScheduleId { get; set; }
        public DaySchedule daySchedule {  get; set; }

    }
}
