using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class TimeSlotInfoDto
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Available { get; set; }

        public TimeSlotInfoDto(TimeSlot timeSlot)
        {
            Id = timeSlot.TiemSlotId;
            StartTime = timeSlot.StartTime.ToString();
            EndTime = timeSlot.EndTime.ToString();
            Available = timeSlot.IsBooked ? "No" : "Yes";
        }
    }

}
