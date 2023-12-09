using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Repository;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Doctor> _userManager;
    private readonly ApplicationDbContext _context;
    public DoctorService(IUnitOfWork unitOfWork, UserManager<Doctor> userManager, ApplicationDbContext context)
    {
        this._unitOfWork = unitOfWork;
        this._userManager = userManager;
        this._context = context;
    }
    //TimeOnly.ParseExact("5:00 pm", "h:mm tt", CultureInfo.InvariantCulture);

    public async Task<(bool IsSuccess, string Message)> AddAppointmentAsync(AddAppointmentDto appointmentDto, string doctorName)
    {

        var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;

        var appointment = await _context.Appointments
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
            //await _unitOfWork.Complete();
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



        // Save all the changes to the database in one go
        await _unitOfWork.Complete();

        return (IsSuccess: true, Message: "Added Successfully..!");
    }




    //public async Task<Appointmetn> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateTimeSlotDto, string doctorName)
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
        var newStartTime = TimeOnly.Parse(updateTimeSlotDto.NewStartTime);
        var newEndTime = TimeOnly.Parse(updateTimeSlotDto.NewEndTime);
        var timeSlotToUpdate = await _unitOfWork.TimeSlots
            .Find(e => e.TiemSlotId == updateTimeSlotDto.TimeSlotId);

        var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeSlotToUpdate.TiemSlotId && (e.DoctorId == doctorId));
        //check if it is already booked || The old timeslot doesn't exist
        if (timeSlotToUpdate == null || timeSlotToUpdate.IsBooked || checkBookingStatus == null ||
            (checkBookingStatus != null && checkBookingStatus.Status != Status.Canceled))
        {
            return (IsSuccess: false, Message: "TimeSlot is not available..!");
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
    //public async Task<bool> DeleteAppointmentAsync(int timeId, string doctorName)
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
}
