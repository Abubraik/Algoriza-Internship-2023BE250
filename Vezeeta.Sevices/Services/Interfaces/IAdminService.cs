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
        //Task<> NumOfPatients();
        Task<List<SpecializationDTO>> Top5Specializations();
        Task<List<DoctorTop10Dto>> Top10Doctors();

        //Doctors
        Task<Doctor> GetDoctorById(string id);
        Task<Doctor> AddDoctor(DoctorModelDto doctor);
        Task<Doctor> EditDoctor(string id, DoctorModelDto doctor);
        Task<bool> DeleteDoctor(string id);

        //Settings
        Task<bool> AddDiscountCode(DiscountCodeDto discountCode);
        Task<bool> UpdateDiscountCode(int discoundId, DiscountCodeDto updatedDiscountCode);
        Task<bool> DeleteDiscountCode(int discoundId);
        Task<bool> DeactivateDiscountCode(int discoundId);

    }
}
