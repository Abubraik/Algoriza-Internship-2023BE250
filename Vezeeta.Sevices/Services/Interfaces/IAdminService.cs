using Vezeeta.Core.Models.Users;
using Vezeeta.Services.Models.DTOs;
using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IAdminService
    {

        Task<int> NumOfDoctors();
        Task<int> NumOfPatients();
        Task<List<object>> GetTotalRequests();
        Task<List<SpecializationDTO>> Top5Specializations();
        Task<List<DoctorTop10Dto>> Top10Doctors();
        //Doctors
        Task<object> GetDoctorById(string id);
        Task<Doctor> EditDoctor(string id, CreateDoctorModelDto doctor);
        Task<bool> DeleteDoctor(string id);


        Task<PatientModelDto> GetPatientById(string id);
        Task<List<PatientModelDto>> GetAllPatients(PaginatedSearchModel paginatedSearch);
        Task<List<DoctorInfoDto>> GetAllDoctors(PaginatedSearchModel paginatedSearch);
        //Settings
        Task<bool> AddDiscountCode(DiscountCodeDto discountCode);
        Task<bool> UpdateDiscountCode(int discoundId, DiscountCodeDto updatedDiscountCode);
        Task<bool> DeleteDiscountCode(int discoundId);
        Task<bool> DeactivateDiscountCode(int discoundId);

    }
}
