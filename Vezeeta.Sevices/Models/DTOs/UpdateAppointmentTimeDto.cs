using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class UpdateAppointmentTimeDto
    {
        [Required]
        public int TimeSlotId { get; set; }
        [Required]
        public required string NewStartTime { get; set; }
        [Required]
        public required string NewEndTime { get; set; }
    }
}
