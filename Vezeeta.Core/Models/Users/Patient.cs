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
        public string? photoPath { get; set; }

        public virtual List<Booking> bookings{ get; set; }
        public virtual List<Feedback> feedbacks { get; set; }

    }
}
