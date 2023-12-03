using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;

namespace Vezeeta.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<ApplicationUser> Users {  get; private set; }
        public IBaseRepository<Doctor> Doctors { get; private set; }
        public IBaseRepository<Patient> Patients { get; private set; }

        public IBaseRepository<Specialization> Specializations { get; private set; }

        public IBaseRepository<Appointment> Appointments { get; private set; }

        public IBaseRepository<DiscountCode> Discounts { get; private set; }

        public IBaseRepository<Booking> Bookings { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new BaseRepository<ApplicationUser>(context);
            Doctors = new BaseRepository<Doctor>(context);
            Patients = new BaseRepository<Patient>(context);
            Specializations = new BaseRepository<Specialization>(context);
            Appointments = new BaseRepository<Appointment>(context);
            Discounts = new BaseRepository<DiscountCode>(context);
            Bookings = new BaseRepository<Booking>(context);
        }


        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
