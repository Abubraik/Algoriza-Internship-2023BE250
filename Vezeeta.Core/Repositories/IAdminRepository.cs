using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Core.Repositories
{
    public interface IAdminRepository :IBaseRepository<ApplicationUser>
    {
        Task<int> NumOfDoctors();
        Task<int> NumOfPatients();
        //Task<> NumOfPatients();
        Task<List<SpecializationDTO>> Top5Specializations();
        Task<List<Doctor>> Top5Doctors();
        
        //Doctors
        Task<bool> AddDoctor();
        Task<bool> EditDoctor();
        Task<bool> DeleteDoctor();

        //Settings
        Task<bool> AddDiscountCode(DiscountCode discountCode);
        Task<bool> UpdateDiscountCode();
        Task<bool> DeleteDiscountCode();
        Task<bool> DeactivateDiscountCode();
    }
}
