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
        public new string? photoPath { get; set; }

        public List<Booking> bookings{ get; set; }
        public List<Feedback>? feedbacks { get; set; }

    }
}
