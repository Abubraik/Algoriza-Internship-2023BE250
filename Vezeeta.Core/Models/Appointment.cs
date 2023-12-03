using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Models
{
    public class Appointment
    {
        [Key]
        public int appointmentId{ get; set; }
        public decimal price { get; set; }

        public string doctorId { get; set; }
        public virtual Doctor doctor{ get; set; }
        public virtual List<DaySchedule> daySchedules { get; set; }
    }
}