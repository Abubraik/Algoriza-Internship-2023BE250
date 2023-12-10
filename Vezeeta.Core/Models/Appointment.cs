using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId{ get; set; }
        public decimal Price { get; set; }

        public string DoctorId { get; set; }
        public  virtual Doctor Doctor{ get; set; }
        public virtual List<DaySchedule> DaySchedules { get; set; }
    }
}