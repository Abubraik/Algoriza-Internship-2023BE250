using System.Numerics;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Services.Models.DTOs;

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
        Task<List<PatientModelDto>> GetAllPatients(int pageNumber, int pageSize, string search);
        Task<List<DoctorInfoDto>> GetAllDoctors(int pageNumber, int pageSize, string search);
        //Settings
        Task<bool> AddDiscountCode(DiscountCodeDto discountCode);
        Task<bool> UpdateDiscountCode(int discoundId, DiscountCodeDto updatedDiscountCode);
        Task<bool> DeleteDiscountCode(int discoundId);
        Task<bool> DeactivateDiscountCode(int discoundId);

    }
}
