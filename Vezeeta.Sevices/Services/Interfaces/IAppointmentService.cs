using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Models;

namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<BookingsInfoDto>> GetAllBookings(string userId);
        Task<ApiResponse<string>> BookAppointment(int timeId, string userId, string? discountCode = null);
        Task<ApiResponse<string>> CancelBookingAsync(int bookingId);

        Task<(bool IsSuccess, string Message)> AddAppointmentAsync(AddAppointmentDto appointment, string doctorName);
        Task<(bool IsSuccess, string Message)> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateTimeSlotDto, string doctorName);
        Task<(bool IsSuccess, string Message)> DeleteAppointmentAsync(int timeId, string doctorName);
        Task<(bool isSuccess, string Message)> ConfirmCheckupAsync(int bookingId);

    }
}