using System.Security.Claims;
using Vezeeta.Sevices.Models.DTOs;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Services.Interfaces

{
    public interface IDoctorService
    {
        Task<(bool IsSuccess, string Message)> AddAppointmentAsync(AddAppointmentDto appointment, string doctorName);
        Task<(bool IsSuccess, string Message)> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateTimeSlotDto, string doctorName);
        Task<(bool IsSuccess, string Message)> DeleteAppointmentAsync(int timeId, string doctorName);
        Task<List<PatientModelDto>> GetAllPatientsAsync(ClaimsPrincipal user, Days day, PaginatedSearchModel paginatedSearch);
        Task<(bool isSuccess, string Message)> ConfirmCheckupAsync(int bookingId);

    }
}
