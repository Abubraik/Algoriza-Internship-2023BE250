using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;

namespace Vezeeta.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<ApplicationUser> Users { get; private set; }
        public IBaseRepository<Doctor> Doctors { get; private set; }
        public IBaseRepository<Patient> Patients { get; private set; }
        public IBaseRepository<TimeSlot> TimeSlots { get; private set; }
        public IBaseRepository<Feedback> Feedbacks { get; private set; }
        public IBaseRepository<Specialization> Specializations { get; private set; }
        public IBaseRepository<DiscountCode> DiscountCodes { get; private set; }
        public IBaseRepository<Appointment> Appointments { get; private set; }
        public IBaseRepository<DaySchedule> DaySchedules { get; private set; }

        public IBaseRepository<Booking> Bookings { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new BaseRepository<ApplicationUser>(context);
            Doctors = new BaseRepository<Doctor>(context);
            Patients = new BaseRepository<Patient>(context);
            Specializations = new BaseRepository<Specialization>(context);
            Appointments = new BaseRepository<Appointment>(context);
            DaySchedules = new BaseRepository<DaySchedule>(context);
            TimeSlots = new BaseRepository<TimeSlot>(context);
            Bookings = new BaseRepository<Booking>(context);
            Feedbacks = new BaseRepository<Feedback>(context);
            DiscountCodes = new BaseRepository<DiscountCode>(context);
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
