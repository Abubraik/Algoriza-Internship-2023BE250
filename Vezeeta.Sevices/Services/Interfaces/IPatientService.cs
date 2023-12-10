using System.Security.Claims;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Services.Interfaces

{
    public interface IPatientService
    {
        Task<int> NumOfPatients();
        Task<PatientModelDto> GetPatientById(string id);
        Task<List<PatientModelDto>> GetAllPatients(PaginatedSearchModel paginatedSearch);
        Task<List<PatientModelDto>> GetAllDoctorPatientsAsync(string userId, DoctorPaginatedSearchModel doctorPaginatedSearchModel);
    }
}
