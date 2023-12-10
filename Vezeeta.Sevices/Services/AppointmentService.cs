using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public AppointmentService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<List<BookingsInfoDto>> GetAllBookings(string userId)
        {
            var patient = await _unitOfWork.Patients.Find(e => e.Email == userId);
            var bookings = _unitOfWork.Bookings.FindAll(e => e.PatientId == patient.Id).Include(d => d.Doctor).ThenInclude(s => s.Specialization)
                .Include(a => a.Doctor.Appointments)
                .ThenInclude(a => a.DaySchedules)
                .ThenInclude(ds => ds.TimeSlots);
            ;
            var doctor = bookings.Select(e => new DoctorInfoDto(e.Doctor)).FirstOrDefault();
            var bookingsList = await bookings.Select(b => new BookingsInfoDto(doctor, b)).ToListAsync();
            return bookingsList;
        }

        public async Task<ApiResponse<string>> BookAppointment(int timeId, string userId, string? discountCode = "")
        {
            var timeSlot = await GetTimeSlotAsync(timeId);
            var appointment = await GetAppointmentAsync(timeId);

            DiscountCode? discount = null;
            if (!string.IsNullOrEmpty(discountCode))
            {
                discount = await GetDiscountCodeAsync(discountCode);
                if (discount == null) return new ApiResponse<string> { IsSuccess = false, Message = "Discount Code is not valid..!" };
            }
            if (!ValidateBooking(timeSlot, appointment, out var response))
            {
                return response;
            }
            var newBooking = await CreateBooking(userId, timeSlot, appointment, discount);
            await _unitOfWork.Bookings.AddAsync(newBooking);
            await _unitOfWork.Complete();
            return new ApiResponse<string> { IsSuccess = true, Message = "Booked Successfully..!", Response = newBooking.BookingId.ToString() };
        }

        public async Task<ApiResponse<string>> CancelBookingAsync(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.Find(e => e.BookingId == bookingId);
            if (booking.Status == Status.Canceled)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = "Already Canceled..!" };
            }
            booking.Status = Status.Canceled;
            var timeSlot = await _unitOfWork.TimeSlots.Find(t => t.TiemSlotId == booking.TimeSlotId);
            timeSlot.IsBooked = false;
            await _unitOfWork.Bookings.UpdateAsync(booking);
            var affectedRows = await _unitOfWork.Complete();
            return new ApiResponse<string> { IsSuccess = true, Message = "Cancelled Successfully..!", Response = bookingId.ToString() };
        }

        //TEST  !!
        public async Task<(bool IsSuccess, string Message)> AddAppointmentAsync(AddAppointmentDto appointmentDto, string doctorName)
        {

            var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;

            var iQueryableAppointments = _unitOfWork.Appointments.FindAll(a => a.DoctorId == doctorId);
            var appointment = await iQueryableAppointments
                .Include(e => e.DaySchedules)
                .ThenInclude(e => e.TimeSlots)
                .SingleOrDefaultAsync(a => a.DoctorId == doctorId);

            if (appointment == null)
            {
                // If the doctor has no appointment, create a new one
                appointment = new Appointment
                {
                    DoctorId = doctorId,
                    Price = appointmentDto.Price,
                    DaySchedules = new List<DaySchedule>()
                };

                await _unitOfWork.Appointments.AddAsync(appointment);

            }
            // Update or add day schedules and time slots
            foreach (var dsDto in appointmentDto.DaySchedules)
            {
                var daySchedule = appointment.DaySchedules
                    .FirstOrDefault(ds => ds.DayOfWeek == dsDto.DayOfWeek);

                if (daySchedule == null)
                {
                    // If the day does not exist, add the new day and time slots
                    daySchedule = new DaySchedule
                    {
                        DayOfWeek = dsDto.DayOfWeek,
                        TimeSlots = new List<TimeSlot>()
                    };
                    appointment.DaySchedules.Add(daySchedule);
                }

                // Add or update time slots
                foreach (var tsDto in dsDto.TimeSlots)
                {
                    var timeSlot = daySchedule.TimeSlots
                        .FirstOrDefault(ts => ts.StartTime == TimeOnly.Parse(tsDto.StartTime) || ts.EndTime == TimeOnly.Parse(tsDto.EndTime));

                    if (timeSlot == null)
                    {
                        // If time slot doesn't exist, add it
                        daySchedule.TimeSlots.Add(new TimeSlot
                        {
                            StartTime = TimeOnly.Parse(tsDto.StartTime),
                            EndTime = TimeOnly.Parse(tsDto.EndTime),
                            IsBooked = false
                        });
                    }
                    else return (IsSuccess: false, Message: $"TimeSlot, {tsDto.StartTime} or {tsDto.EndTime}, Already Available..!");
                }

            }



            await _unitOfWork.Complete();

            return (IsSuccess: true, Message: "Added Successfully..!");
        }

        public async Task<(bool IsSuccess, string Message)> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateTimeSlotDto, string doctorName)
        {
            #region CommentedCode
            //var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;
            //// Parse the new and old times
            ////var oldStartTime = TimeOnly.Parse(updateTimeSlotDto.OldStartTime);
            ////var oldEndTime = TimeOnly.Parse(updateTimeSlotDto.OldEndTime);
            //var newStartTime = TimeOnly.Parse(updateTimeSlotDto.NewStartTime);
            //var newEndTime = TimeOnly.Parse(updateTimeSlotDto.NewEndTime);

            //// Check if the doctor has any appointments on that day
            //var appointment = await _context.Appointments
            //    .Include(e => e.DaySchedules)
            //    .ThenInclude(e => e.TimeSlots)
            //    .SingleOrDefaultAsync(e => e.DoctorId == doctorId
            //&& e.DaySchedules.Any(d => d.DayOfWeek == updateTimeSlotDto.DayOfWeek));

            //if (appointment == null)
            //{
            //    // Doctor has no appointments on that day
            //    return null;
            //}

            //var daySchedule = appointment.DaySchedules
            //    .FirstOrDefault(ds => ds.DayOfWeek == updateTimeSlotDto.DayOfWeek);

            //if (daySchedule == null)
            //{
            //    // Doctor has an appointment, but not on that specific day
            //    return null;
            //}

            //// Find the existing timeslot to update
            //var timeSlotToUpdate = daySchedule.TimeSlots
            //    .FirstOrDefault(ts => ts.StartTime == oldStartTime && ts.EndTime == oldEndTime);

            //var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeSlotToUpdate.TiemSlotId);
            //if (timeSlotToUpdate == null || timeSlotToUpdate.IsBooked
            //    || (checkBookingStatus != null && checkBookingStatus.Status != Status.Canceled))
            //{
            //    //check if it is already booked
            //    // The old timeslot doesn't exist
            //    return null;
            //}

            //// Update the timeslot
            //timeSlotToUpdate.StartTime = newStartTime;
            //timeSlotToUpdate.EndTime = newEndTime;

            //// Save the changes
            //await _unitOfWork.Complete();

            //return appointment; // Return the updated appointment

            #endregion
            var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;
            TimeOnly newStartTime, newEndTime;
            try
            {
                newStartTime = TimeOnly.Parse(updateTimeSlotDto.NewStartTime);
                newEndTime = TimeOnly.Parse(updateTimeSlotDto.NewEndTime);
            }
            catch (Exception)
            {
                return (IsSuccess: false, Message: "Times is NOT valid..!");
            }
            var timeSlotToUpdate = await _unitOfWork.TimeSlots
                .Find(e => e.TiemSlotId == updateTimeSlotDto.TimeSlotId);

            var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeSlotToUpdate.TiemSlotId && (e.DoctorId == doctorId));
            //check if it is already booked || The old timeslot doesn't exist
            if (timeSlotToUpdate == null || timeSlotToUpdate.IsBooked || checkBookingStatus == null ||
                (checkBookingStatus != null && checkBookingStatus.Status != Status.Canceled))
            {
                return (IsSuccess: false, Message: "TimeSlot is not available or already booked..!");
            }

            //check if startTime is before endTime
            if (newStartTime > newEndTime)
            {
                return (IsSuccess: false, Message: "StartTime is AFTER EndTime..!");
            }
            timeSlotToUpdate.StartTime = newStartTime;
            timeSlotToUpdate.EndTime = newEndTime;
            return (IsSuccess: true, Message: "TimeSlot updated Successfully..!");
        }

        public async Task<(bool IsSuccess, string Message)> DeleteAppointmentAsync(int timeId, string doctorName)

        {
            var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;
            var appointment = await _unitOfWork.Appointments.Find(e => e.DoctorId == doctorId);
            var timeSlot = await _unitOfWork.TimeSlots.Find(e => e.TiemSlotId == timeId);
            if (appointment == null || timeSlot == null)
            {
                return (IsSuccess: false, Message: "TimeSlot is not available..!");
            }
            var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeId && e.DoctorId == doctorId);

            if (checkBookingStatus != null)
            {
                if (checkBookingStatus.Status != Status.Canceled)
                    return (IsSuccess: false, Message: "TimeSlot is already within patients records..!");
            }

            _unitOfWork.TimeSlots.Remove(timeSlot);
            await _unitOfWork.Complete();
            return (IsSuccess: true, Message: "TimeSlot Deleted..!");
        }
        public async Task<(bool isSuccess, string Message)> ConfirmCheckupAsync(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.FindAll(b => b.BookingId == bookingId).Include(e=>e.TimeSlot).FirstOrDefaultAsync();
            if (booking.Status == Status.Completed)
                return (false, "Already Confirmed..!");
            else if (booking.Status == Status.Canceled)
                return (false, "Already Canceled..!");
            booking.Status = Status.Completed;
            booking.TimeSlot.IsBooked = false;
            await _unitOfWork.Complete();
            return (true, "Confirmed Successfully..!");
        }
        //Helpers
        private async Task<TimeSlot> GetTimeSlotAsync(int timeId)
        {
            return await _unitOfWork.TimeSlots.Find(e => e.TiemSlotId == timeId);
        }

        private async Task<Appointment> GetAppointmentAsync(int timeId)
        {
            return await _unitOfWork.Appointments.Find(e => e.AppointmentId == e.DaySchedules
            .FirstOrDefault(d => d.TimeSlots.Any(d => d.TiemSlotId == timeId))!
            .AppointmentId);
        }

        private async Task<DiscountCode> GetDiscountCodeAsync(string discountCode)
        {
            return await _unitOfWork.DiscountCodes.Find(e => e.Code == discountCode);
        }

        private bool ValidateBooking(TimeSlot timeSlot, Appointment appointment, out ApiResponse<string> response)
        {
            if (timeSlot == null || timeSlot.IsBooked)
            {
                response = new ApiResponse<string> { IsSuccess = false, Message = "TimeSlot is not valid or available..!" };
                return false;
            }
            if (appointment == null)
            {
                response = new ApiResponse<string> { IsSuccess = false, Message = "Appointment is not valid..!" };
                return false;
            }
            response = new ApiResponse<string> { IsSuccess = true, Message = "Validated Successfully..!" };
            return true;
        }

        private async Task<Booking> CreateBooking(string userId, TimeSlot timeSlot, Appointment appointment, DiscountCode discountCode)
        {
            var patient = await _unitOfWork.Patients.Find(e => e.Email == userId);
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
