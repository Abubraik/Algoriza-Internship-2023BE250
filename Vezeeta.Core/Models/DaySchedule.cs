using System.ComponentModel.DataAnnotations;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Models
{
    public class DaySchedule
    {
        [Key]
        public int dayScheduleId { get; set; }
        [Required]
        public Days dayOfWeek { get; set; }

        public virtual List<TimeSlot>? timeSlots { get; set; }

        [Required]
        public int appointmentId { get; set; }
        public virtual Appointment? appointment { get; set; }
    }
}