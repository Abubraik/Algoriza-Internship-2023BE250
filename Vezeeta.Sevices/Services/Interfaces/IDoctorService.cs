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
        Task<List<DoctorInfoDto>> SearchForDoctorsAppointments(PaginatedSearchModel paginatedSearch);

    }
}
