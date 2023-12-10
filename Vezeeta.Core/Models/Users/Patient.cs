namespace Vezeeta.Core.Models.Users
{
    public class Patient :ApplicationUser
    {
        public string? Photo{ get; set; }

        public virtual List<Booking>? Bookings { get; set; }
        public virtual List<Feedback>? Feedbacks { get; set; }

    }
}
