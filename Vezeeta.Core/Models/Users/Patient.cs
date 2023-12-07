using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models.Users
{
    public class Patient :ApplicationUser
    {
        public string? Photo{ get; set; }

        public List<Booking> Bookings { get; set; }
        public List<Feedback> Feedbacks { get; set; }

    }
}
