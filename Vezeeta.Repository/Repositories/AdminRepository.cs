using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Repository.Repositories
{
    internal class AdminRepository : BaseRepository<ApplicationUser>, IAdminRepository
    {
         public AdminRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> NumOfDoctors() => await _entities. OfType<Doctor>().CountAsync();

        public async Task<int> NumOfPatients() => await _entities.OfType<Patient>().CountAsync();
        public async Task<List<SpecializationDTO>> Top5Specializations()
        {
            //var top5Specializations =await _context.Specializations.OrderByDescending(e=>e.Doctors.Count()).ToListAsync();
            var top5Specializations = await _entities.OfType<Doctor>()
                .GroupBy(e => e.specialization)
                .OrderByDescending(e=>e.Count())
                .Take(5)
                .Select(e=>new SpecializationDTO { specializationName = e.Key.name,numberOfDoctors=e.Count()})
                .ToListAsync();
            return top5Specializations;
        }

        public async Task<List<Doctor>> Top5Doctors()
        {
            var top5Doctors = await _entities.OfType<Doctor>().OrderByDescending(e => e.bookings.Count(b => b.status == Status.Completed)).Take(5).ToListAsync();
            return top5Doctors;
        }

        public Task<bool> AddDoctor()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDoctor()
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditDoctor()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddDiscountCode(DiscountCode discountCode)
        {
            throw new NotImplementedException();

        }

        public Task<bool> UpdateDiscountCode()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscountCode()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateDiscountCode()
        {
            throw new NotImplementedException();
        }
    }
}
