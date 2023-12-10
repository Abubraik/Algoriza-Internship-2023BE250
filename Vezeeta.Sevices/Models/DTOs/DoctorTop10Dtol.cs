namespace Vezeeta.Sevices.Models.DTOs
{
    public class DoctorTop10Dto
    {
        public required string Image { get; set; }
        public required string FullName { get; set; }
        public required string Specialization { get; set; }
        public int NumberOfrequests { get; set; }
    }
}
