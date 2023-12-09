using System.ComponentModel.DataAnnotations;

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
