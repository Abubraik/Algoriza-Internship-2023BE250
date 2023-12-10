using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public virtual DaySchedule DaySchedule { get; set; }

    }
}
