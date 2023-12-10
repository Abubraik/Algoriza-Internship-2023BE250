using System.ComponentModel.DataAnnotations;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Models
{
    public class DaySchedule
    {
        [Key]
        public int DayScheduleId { get; set; }
        [Required]
        public Days DayOfWeek { get; set; }

        public virtual List<TimeSlot> TimeSlots { get; set; }

        [Required]
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}