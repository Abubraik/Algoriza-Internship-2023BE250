using Vezeeta.Core.Models;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class AppointmentInfoDto
    {
        public string Day { get; set; }
        public List<TimeSlotInfoDto> Times { get; set; }

        public AppointmentInfoDto(DaySchedule daySchedule)
        {
            Day = daySchedule.DayOfWeek.ToString();
            Times = daySchedule.TimeSlots.Select(ts => new TimeSlotInfoDto(ts)).ToList();
        }

    }

}
