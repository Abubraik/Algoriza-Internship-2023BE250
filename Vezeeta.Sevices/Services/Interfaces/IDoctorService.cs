using System.Security.Claims;
using Vezeeta.Core.Models.Users;
using Vezeeta.Sevices.Models.DTOs;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Services.Interfaces

{
    public interface IDoctorService
    {
        Task<int> NumOfDoctors();
        Task<List<DoctorTop10Dto>> Top10Doctors();
        Task<List<DoctorInfoDto>> GetAllDoctors(PaginatedSearchModel paginatedSearch);
        Task<Doctor> EditDoctor(string id, CreateDoctorModelDto doctor);
        Task<object> GetDoctorById(string id);
        Task<bool> DeleteDoctor(string id);
        Task<List<DoctorInfoDto>> SearchForDoctors(PaginatedSearchModel paginatedSearch);

        //Task<(bool IsSuccess, string Message)> AddAppointmentAsync(AddAppointmentDto appointment, string doctorName);
        //Task<(bool IsSuccess, string Message)> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateTimeSlotDto, string doctorName);
        //Task<(bool IsSuccess, string Message)> DeleteAppointmentAsync(int timeId, string doctorName);
        Task<List<PatientModelDto>> GetAllDoctorPatientsAsync(string userId, Days day, PaginatedSearchModel paginatedSearch);
        //Task<(bool isSuccess, string Message)> ConfirmCheckupAsync(int bookingId);

    }
}
