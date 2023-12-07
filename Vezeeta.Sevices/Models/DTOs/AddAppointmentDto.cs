using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Sevices.Models.DTOs
{

    public class AddAppointmentDto
    {
        [Required]

        public decimal Price { get; set; }
        [Required]
        public List<DayScheduleDto> DaySchedules { get; set; }

    }

}