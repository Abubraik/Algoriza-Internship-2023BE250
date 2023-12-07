using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class UpdateAppointmentTimeDto
    {
        //[Required]
        //public Days DayOfWeek { get; set; }
        [Required]
        public int TimeSlotId { get; set; }
        //[Required]
        //public string OldStartTime { get; set; }
        //[Required]
        //public string OldEndTime { get; set; }
        [Required]
        public string NewStartTime { get; set; }
        [Required]
        public string NewEndTime { get; set; }
    }
}
