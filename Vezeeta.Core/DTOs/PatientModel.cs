using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.DTOs
{
    public class PatientModel
    {
        public string? image { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public Gender gender { get; set; }
        public DateOnly dateOfBirth { get; set; }
        public List<BookingsDTO> bookings { get; set; }
    }
}
