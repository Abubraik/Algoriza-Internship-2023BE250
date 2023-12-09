using Azure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<DoctorInfoDto>> SearchForDoctors(int pageNumber, int pageSize, string search)
        {
            pageNumber = Math.Max(pageNumber, 1);

            var doctorsQuery = _unitOfWork.Doctors.FindAll(d => d.FirstName.Contains(search)
            || d.LastName.Contains(search)
            || d.Email.Contains(search)
            || d.PhoneNumber.Contains(search)
            , pageNumber
            , pageSize)
                .Include(d => d.Appointments)
                .ThenInclude(a => a.DaySchedules)
            .ThenInclude(ds => ds.TimeSlots);

            var doctorList = await doctorsQuery.Select(d => new DoctorInfoDto(d)).ToListAsync();

            return doctorList;
        }
        public async Task<ApiResponse<string>> BookAppointment(int timeId, ClaimsPrincipal User, string discountCode = "")
        {
            var timeSlot = await _unitOfWork.TimeSlots.Find(e => e.TiemSlotId == timeId);
            if (timeSlot == null || timeSlot.IsBooked)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = "TimeSlot is not valid or available..!" };
            }
            var appointment = await _unitOfWork.Appointments.Find(e => e.AppointmentId == e.DaySchedules
            .FirstOrDefault(d => d.TimeSlots.Any(d => d.TiemSlotId == timeId))!
            .AppointmentId);

            DiscountCode? discount = null;
            if (!string.IsNullOrEmpty(discountCode))
            {
                discount = !string.IsNullOrEmpty(discountCode) ? await _unitOfWork.DiscountCodes.Find(e => e.Code == discountCode) : null;
                if (discount == null) return new ApiResponse<string> { IsSuccess = false, Message = "Discount Code is not valid..!" };
            }

            Booking newBooking = await CreateBooking(User, timeSlot, appointment, discount);
            await _unitOfWork.Bookings.AddAsync(newBooking);
            await _unitOfWork.Complete();
            return new ApiResponse<string> { IsSuccess = true, Message = "Booked Successfully..!",Response = newBooking.BookingId.ToString() };
        }

        public async Task<List<BookingsInfoDto>> GetAllBookings(ClaimsPrincipal User)
        {
            var patient = await _unitOfWork.Patients.Find(e => e.Email == User.Identity!.Name!);
            var bookings =  _unitOfWork.Bookings.FindAll(e => e.PatientId == patient.Id).Include(d => d.Doctor).ThenInclude(s=>s.Specialization)
                .Include(a => a.Doctor.Appointments)
                .ThenInclude(a => a.DaySchedules)
                .ThenInclude(ds => ds.TimeSlots);
            ;
            var doctor = bookings.Select(e => new DoctorInfoDto(e.Doctor)).FirstOrDefault();
            var bookingsList = await bookings.Select(b => new BookingsInfoDto(doctor, b)).ToListAsync();
            return bookingsList;
        }

        public async Task<ApiResponse<string>> CancelAppointment(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.Find(e => e.BookingId == bookingId);
            if(booking.Status == Status.Canceled)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = "Already Canceled..!" };
            }
            booking.Status = Status.Canceled;
            var timeSlot =await _unitOfWork.TimeSlots.Find(t=>t.TiemSlotId == booking.TimeSlotId);
            timeSlot.IsBooked = false;
            await _unitOfWork.Bookings.UpdateAsync(booking);
            var affectedRows = await _unitOfWork.Complete();
            return new ApiResponse<string> { IsSuccess = true, Message = "Cancelled Successfully..!" ,Response = bookingId.ToString() };
        }

        //Helpers
        private async Task<Booking> CreateBooking(ClaimsPrincipal user, TimeSlot timeSlot, Appointment appointment, DiscountCode discountCode)
        {
            var patient = await _unitOfWork.Patients.Find(e => e.Email == user.Identity!.Name!);
            var finalPrice = appointment.Price;
            if (discountCode != null)
            {
                finalPrice = discountCode.DiscountType == DiscountType.Value ?
                    finalPrice - discountCode.DiscountValue : finalPrice * ((100 - discountCode.DiscountValue) / 100);
            }

            timeSlot.IsBooked = true;

            return new Booking
            {
                Status = Status.Pending,
                PatientId = patient.Id,
                DiscountCodeId = discountCode?.DiscountCodeId,
                DoctorId = appointment.DoctorId,
                TimeSlotId = timeSlot.TiemSlotId,
                Price = appointment.Price,
                FinalPrice = finalPrice
            };
        }

    }
}
