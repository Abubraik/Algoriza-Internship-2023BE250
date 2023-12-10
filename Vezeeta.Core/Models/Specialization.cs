using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Models
{
    public class Specialization
    {
        [Key]
        public int SpecializationId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Doctor> Doctors { get; set; }
    }
}
