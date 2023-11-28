using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Models
{
    public class Specialization
    {
        [Key]
        public int specializationId { get; set; }
        public string name { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
