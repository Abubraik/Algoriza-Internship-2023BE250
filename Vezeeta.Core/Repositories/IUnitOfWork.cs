using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<ApplicationUser> Users { get; }
        IBaseRepository<Doctor> Doctors{ get; }
        IBaseRepository<Patient> Patients { get; }

        IBaseRepository<Specialization> Specializations { get; }
        IBaseRepository<Appointment> Appointments { get; }  
        IBaseRepository<DiscountCode> Discounts { get; }
        IBaseRepository<Booking> Bookings { get; }

        Task<int> Complete();
    }
}
