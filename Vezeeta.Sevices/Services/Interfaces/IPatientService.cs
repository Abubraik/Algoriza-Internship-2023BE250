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

    }
}
