using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Core.Models;

namespace Vezeeta.Sevices.Services.Interfaces

{
    public interface IDoctorService
    {
        //Task<IEnumerable<Patient>> getBookings(DayOfWeek day, int pageSize, int pageNumber);
        //Task<Booking> confirmBooking(int bookingId);
        Task<Appointment> AddAppointmentAsync(AddAppointmentDto appointment, string doctorName);
        Task<Appointment> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateDto, string doctorId);
        Task<bool> DeleteAppointmentAsync(int timeId, string doctorName);
    }
}
