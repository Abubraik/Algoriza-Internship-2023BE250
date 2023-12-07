using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Repository;
using static Vezeeta.Core.Enums.Enums;
using Vezeeta.Sevices.Services.Interfaces;

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

    public async Task<Appointment> AddAppointmentAsync(AddAppointmentDto appointmentDto, string doctorName)
    {
        //TimeOnly.ParseExact("5:00 pm", "h:mm tt", CultureInfo.InvariantCulture);

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
                else return null;
            }
        }



        // Save all the changes to the database in one go
        await _unitOfWork.Complete();

        // Return the newly created or updated appointment with its related entities
        return appointment; // Assuming you want to return the first updated or newly added appointment
    }



    public async Task<Appointment> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateTimeSlotDto, string doctorName)
    {
        var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;
        // Parse the new and old times
        var oldStartTime = TimeOnly.Parse(updateTimeSlotDto.OldStartTime);
        var oldEndTime = TimeOnly.Parse(updateTimeSlotDto.OldEndTime);
        var newStartTime = TimeOnly.Parse(updateTimeSlotDto.NewStartTime);
        var newEndTime = TimeOnly.Parse(updateTimeSlotDto.NewEndTime);

        // Check if the doctor has any appointments on that day
        var appointment = await _context.Appointments
            .Include(e => e.DaySchedules)
            .ThenInclude(e => e.TimeSlots)
            .SingleOrDefaultAsync(e => e.DoctorId == doctorId
        && e.DaySchedules.Any(d => d.DayOfWeek == updateTimeSlotDto.DayOfWeek));

        if (appointment == null)
        {
            // Doctor has no appointments on that day
            return null;
        }

        var daySchedule = appointment.DaySchedules
            .FirstOrDefault(ds => ds.DayOfWeek == updateTimeSlotDto.DayOfWeek);

        if (daySchedule == null)
        {
            // Doctor has an appointment, but not on that specific day
            return null;
        }

        // Find the existing timeslot to update
        var timeSlotToUpdate = daySchedule.TimeSlots
            .FirstOrDefault(ts => ts.StartTime == oldStartTime && ts.EndTime == oldEndTime);

        var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeSlotToUpdate.TiemSlotId);
        if (timeSlotToUpdate == null || timeSlotToUpdate.IsBooked
            || (checkBookingStatus != null && checkBookingStatus.Status != Status.Canceled))
        {
            //check if it is already booked
            // The old timeslot doesn't exist
            return null;
        }

        // Update the timeslot
        timeSlotToUpdate.StartTime = newStartTime;
        timeSlotToUpdate.EndTime = newEndTime;

        // Save the changes
        await _unitOfWork.Complete();

        return appointment; // Return the updated appointment
    }
    public async Task<bool> DeleteAppointmentAsync(int timeId, string doctorName)
    {
        var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;
        var appointment = await _unitOfWork.Appointments.Find(e => e.DoctorId == doctorId);
        var timeSlot = await _unitOfWork.TimeSlots.Find(e => e.TiemSlotId == timeId);

        if (appointment == null || timeSlot == null)
        {
            return false;
        }
        var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeId);

        if (checkBookingStatus != null)
        {
            if (checkBookingStatus.Status != Status.Canceled || checkBookingStatus.DoctorId != doctorId)
                return false;
        }

        _unitOfWork.TimeSlots.Remove(timeSlot);
        await _unitOfWork.Complete();
        return true;

    }
}
