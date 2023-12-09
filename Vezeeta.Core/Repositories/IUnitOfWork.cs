using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<ApplicationUser> Users { get; }
        IBaseRepository<Doctor> Doctors { get; }
        IBaseRepository<Patient> Patients { get; }

        IBaseRepository<Specialization> Specializations { get; }
        IBaseRepository<Appointment> Appointments { get; }
        IBaseRepository<DaySchedule> DaySchedules { get; }
        IBaseRepository<Feedback> Feedbacks { get; }
        IBaseRepository<TimeSlot> TimeSlots { get; }
        IBaseRepository<DiscountCode> DiscountCodes { get; }
        IBaseRepository<Booking> Bookings { get; }

        Task<int> Complete();
    }
}
