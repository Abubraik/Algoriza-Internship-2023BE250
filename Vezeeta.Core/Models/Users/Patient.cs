namespace Vezeeta.Core.Models.Users
{
    public class Patient :ApplicationUser
    {
        public string? Photo{ get; set; }

        public List<Booking>? Bookings { get; set; }
        public List<Feedback>? Feedbacks { get; set; }

    }
}
