using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Models
{
    public class Specialization
    {
        [Key]
        public int specializationId { get; set; }
        [Required]
        public string name { get; set; }

        public virtual List<Doctor> Doctors { get; set; }
    }
}
