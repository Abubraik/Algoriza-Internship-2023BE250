using Vezeeta.Sevices.Models;

namespace Vezeeta.Sevices.Services.Interfaces

{
    public interface IPatientService
    {
        //Task<ApiResponse<string>> AddPatient(AccountModelDto model);
        Task<ApiResponse<string>> SearchForDoctors(int page, int pageSize, string search);
    }
}
