using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Models
{
    public class Appointment
    {
        [Key]
        public int appointmentId{ get; set; }
        public decimal price { get; set; }

        //public int docotorId { get; set; }
        public Doctor doctor{ get; set; }
        public List<DaySchedule> daySchedules { get; set; }
    }
}