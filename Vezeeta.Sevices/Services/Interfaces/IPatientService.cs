using System.Security.Claims;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces

{
    public interface IPatientService
    {
        //Task<ApiResponse<string>> AddPatient(AccountModelDto model);
        //Task<ApiResponse<List<DoctorInfoDto>>> SearchForDoctors(int page, int pageSize, string search);
        Task<List<DoctorInfoDto>> SearchForDoctors(int pageNumber, int pageSize, string search);
        Task<ApiResponse<string>> BookAppointment(int timeId, ClaimsPrincipal User, string discountCode = null);
        Task<List<BookingsInfoDto>> GetAllBookings(ClaimsPrincipal User);
        Task<ApiResponse<string>> CancelAppointment(int bookingId);
    }
}
