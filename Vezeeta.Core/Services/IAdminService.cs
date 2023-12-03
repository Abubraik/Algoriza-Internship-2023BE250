using System.Numerics;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Services
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
        Task<Doctor> AddDoctor(DoctorModel doctor);
        Task<Doctor> EditDoctor(string id, DoctorModel doctor);
        Task<bool> DeleteDoctor(string id);

        //Settings
        Task<bool> AddDiscountCode(DiscountCode discountCode);
        Task<bool> UpdateDiscountCode();
        Task<bool> DeleteDiscountCode();
        Task<bool> DeactivateDiscountCode();

    }
}
