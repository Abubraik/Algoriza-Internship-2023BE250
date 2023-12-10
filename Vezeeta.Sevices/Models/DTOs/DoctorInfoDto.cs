using Vezeeta.Core.Models.Users;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class DoctorInfoDto
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Specialize { get; set; }
        public decimal Price { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public List<AppointmentInfoDto>? Appointments { get; set; }
        public DoctorInfoDto()
        {

        }
        public DoctorInfoDto(Doctor doctor)
        {
            Image = doctor.Photo;
            FullName = $"{doctor.FirstName} {doctor.LastName}";
            Email = doctor.Email;
            Phone = doctor.PhoneNumber!;
            Specialize =doctor.Specialization.Name;
            Price = doctor.Appointments.Price;
            Gender = doctor.Gender.ToString();
            Appointments = doctor.Appointments.DaySchedules.Select(ds => new AppointmentInfoDto(ds))?.ToList();
        }
    }

}
