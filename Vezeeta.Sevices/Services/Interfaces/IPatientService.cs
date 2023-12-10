using System.Security.Claims;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces

{
    public interface IPatientService
    {
        Task<int> NumOfPatients();
        Task<PatientModelDto> GetPatientById(string id);
        Task<List<PatientModelDto>> GetAllPatients(PaginatedSearchModel paginatedSearch);

        //Task<List<DoctorInfoDto>> SearchForDoctors(PaginatedSearchModel paginatedSearch);

        //Task<List<BookingsInfoDto>> GetAllBookings(string userId);
        //Task<ApiResponse<string>> BookAppointment(int timeId, string userId, string? discountCode = null);
        //Task<ApiResponse<string>> CancelAppointment(int bookingId);
    }
}
