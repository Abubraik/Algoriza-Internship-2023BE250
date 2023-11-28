using System.ComponentModel.DataAnnotations;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Models
{
    public class DaySchedule
    {
        [Key]
        public int dayScheduleId { get; set; }
        public Days dayOfWeek { get; set; }

        public List<TimeSlot> timeSlots { get; set; }

        public int appointmentId { get; set; }
        public Appointment appointment { get; set; }
    }
}