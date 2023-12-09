using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces

{
    public interface IDoctorService
    {
        //Task<IEnumerable<Patient>> getBookings(DayOfWeek day, int pageSize, int pageNumber);
        //Task<Booking> confirmBooking(int bookingId);
        Task<(bool IsSuccess, string Message)> AddAppointmentAsync(AddAppointmentDto appointment, string doctorName);
        //Task<Appointment> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateDto, string doctorId);
        Task<(bool IsSuccess, string Message)> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateTimeSlotDto, string doctorName);
        //Task<bool> DeleteAppointmentAsync(int timeId, string doctorName);
        Task<(bool IsSuccess, string Message)> DeleteAppointmentAsync(int timeId, string doctorName);

    }
}
