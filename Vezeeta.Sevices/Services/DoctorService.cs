using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Doctor> _userManager;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, UserManager<Doctor> userManager, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<int> NumOfDoctors() => await _unitOfWork.Users.GetAll().OfType<Doctor>().CountAsync();
    public async Task<List<DoctorTop10Dto>> Top10Doctors()
    {
        var top5doctors = await _unitOfWork.Users.GetAll().OfType<Doctor>()
            .OrderByDescending(e => e.Bookings.Count())
            .Take(10)
            .Select(e => new DoctorTop10Dto
            {
                Image = e.Photo
            ,
                FullName = e.NormalizedUserName
            ,
                Specialization = e.Specialization.Name
            ,
                NumberOfrequests = e.Bookings.Count(b => b.Status == Status.Completed)
            })
            .ToListAsync();
        return top5doctors;
    }
    public async Task<List<DoctorInfoDto>> GetAllDoctors(PaginatedSearchModel paginatedSearch)
    {
        paginatedSearch.PageNumber = Math.Max(paginatedSearch.PageNumber, 1);
        var patientQuery = _unitOfWork.Doctors.FindAll(p => p.FirstName.Contains(paginatedSearch.Search)
        || p.LastName.Contains(paginatedSearch.Search)
        || p.Email.Contains(paginatedSearch.Search)
        || p.PhoneNumber.Contains(paginatedSearch.Search)
        , paginatedSearch.PageNumber
        , paginatedSearch.PageSize).Include(e=>e.Appointments).Include(e=>e.Specialization);
        //EDIT FOR BIRTHDATE
        var patientList = await patientQuery.Select(p => _mapper.Map<DoctorInfoDto>(p)).ToListAsync();
        return patientList;
    }
    public async Task<List<DoctorInfoDto>> SearchForDoctors(PaginatedSearchModel paginatedSearch)
    {
        paginatedSearch.PageNumber = Math.Max(paginatedSearch.PageNumber, 1);

        var doctorsQuery = _unitOfWork.Doctors.FindAll(d => d.FirstName.Contains(paginatedSearch.Search)
        || d.LastName.Contains(paginatedSearch.Search)
        || d.Email.Contains(paginatedSearch.Search)
        || d.PhoneNumber!.Contains(paginatedSearch.Search)
        , paginatedSearch.PageNumber
        , paginatedSearch.PageSize)
            .Include(d => d.Appointments)
            .ThenInclude(a => a.DaySchedules)
        .ThenInclude(ds => ds.TimeSlots);

        var doctorList = await doctorsQuery.Select(d => new DoctorInfoDto(d)).ToListAsync();

        return doctorList;
    }

    public async Task<object> GetDoctorById(string id)
    {
        Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        await _unitOfWork.Doctors.Explicit(doctor).Reference("Specialization").LoadAsync();
        return new
        {
            image = doctor.Photo,
            fullName = $"{doctor.FirstName} {doctor.LastName}",
            email = doctor.Email,
            phoneNumber = doctor.PhoneNumber,
            specialization = doctor.Specialization!.Name,
            gender = doctor.Gender,
            dateOfBirth = doctor.DateOfBirth,
        }; ;

    }
    public async Task<Doctor> EditDoctor(string id, CreateDoctorModelDto newDoctorInfo)
    {
        Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        doctor.FirstName = newDoctorInfo.FirstName;
        doctor.LastName = newDoctorInfo.LastName;
        doctor.DateOfBirth = newDoctorInfo.DateOfBirth;
        doctor.Gender = newDoctorInfo.Gender;
        doctor.Email = newDoctorInfo.Email;
        doctor.Specialization = await _unitOfWork.Specializations.Find(e => e.Name == newDoctorInfo.Specialization);
        doctor.PhoneNumber = newDoctorInfo.PhoneNumber;

        await _unitOfWork.Doctors.UpdateAsync(doctor);
        await _unitOfWork.Complete();
        return doctor;

    }
    public async Task<bool> DeleteDoctor(string id)
    {
        Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor != null)
        {
            _unitOfWork.Users.Remove(doctor);
            await _unitOfWork.Complete();
            return true;
        }
        return false;
    }


    ////TEST  !!
    //public async Task<(bool IsSuccess, string Message)> AddAppointmentAsync(AddAppointmentDto appointmentDto, string doctorName)
    //{

    //    var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;

    //    var iQueryableAppointments = _unitOfWork.Appointments.FindAll(a => a.DoctorId == doctorId);
    //    var appointment = await iQueryableAppointments
    //        .Include(e => e.DaySchedules)
    //        .ThenInclude(e => e.TimeSlots)
    //        .SingleOrDefaultAsync(a => a.DoctorId == doctorId);

    //    if (appointment == null)
    //    {
    //        // If the doctor has no appointment, create a new one
    //        appointment = new Appointment
    //        {
    //            DoctorId = doctorId,
    //            Price = appointmentDto.Price,
    //            DaySchedules = new List<DaySchedule>()
    //        };

    //        await _unitOfWork.Appointments.AddAsync(appointment);

    //    }
    //    // Update or add day schedules and time slots
    //    foreach (var dsDto in appointmentDto.DaySchedules)
    //    {
    //        var daySchedule = appointment.DaySchedules
    //            .FirstOrDefault(ds => ds.DayOfWeek == dsDto.DayOfWeek);

    //        if (daySchedule == null)
    //        {
    //            // If the day does not exist, add the new day and time slots
    //            daySchedule = new DaySchedule
    //            {
    //                DayOfWeek = dsDto.DayOfWeek,
    //                TimeSlots = new List<TimeSlot>()
    //            };
    //            appointment.DaySchedules.Add(daySchedule);
    //        }

    //        // Add or update time slots
    //        foreach (var tsDto in dsDto.TimeSlots)
    //        {
    //            var timeSlot = daySchedule.TimeSlots
    //                .FirstOrDefault(ts => ts.StartTime == TimeOnly.Parse(tsDto.StartTime) || ts.EndTime == TimeOnly.Parse(tsDto.EndTime));

    //            if (timeSlot == null)
    //            {
    //                // If time slot doesn't exist, add it
    //                daySchedule.TimeSlots.Add(new TimeSlot
    //                {
    //                    StartTime = TimeOnly.Parse(tsDto.StartTime),
    //                    EndTime = TimeOnly.Parse(tsDto.EndTime),
    //                    IsBooked = false
    //                });
    //            }
    //            else return (IsSuccess: false, Message: $"TimeSlot, {tsDto.StartTime} or {tsDto.EndTime}, Already Available..!");
    //        }

    //    }



    //    await _unitOfWork.Complete();

    //    return (IsSuccess: true, Message: "Added Successfully..!");
    //}

    //public async Task<(bool IsSuccess, string Message)> UpdateAppointmentTimeAsync(UpdateAppointmentTimeDto updateTimeSlotDto, string doctorName)
    //{
    //    #region CommentedCode
    //    //var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;
    //    //// Parse the new and old times
    //    ////var oldStartTime = TimeOnly.Parse(updateTimeSlotDto.OldStartTime);
    //    ////var oldEndTime = TimeOnly.Parse(updateTimeSlotDto.OldEndTime);
    //    //var newStartTime = TimeOnly.Parse(updateTimeSlotDto.NewStartTime);
    //    //var newEndTime = TimeOnly.Parse(updateTimeSlotDto.NewEndTime);

    //    //// Check if the doctor has any appointments on that day
    //    //var appointment = await _context.Appointments
    //    //    .Include(e => e.DaySchedules)
    //    //    .ThenInclude(e => e.TimeSlots)
    //    //    .SingleOrDefaultAsync(e => e.DoctorId == doctorId
    //    //&& e.DaySchedules.Any(d => d.DayOfWeek == updateTimeSlotDto.DayOfWeek));

    //    //if (appointment == null)
    //    //{
    //    //    // Doctor has no appointments on that day
    //    //    return null;
    //    //}

    //    //var daySchedule = appointment.DaySchedules
    //    //    .FirstOrDefault(ds => ds.DayOfWeek == updateTimeSlotDto.DayOfWeek);

    //    //if (daySchedule == null)
    //    //{
    //    //    // Doctor has an appointment, but not on that specific day
    //    //    return null;
    //    //}

    //    //// Find the existing timeslot to update
    //    //var timeSlotToUpdate = daySchedule.TimeSlots
    //    //    .FirstOrDefault(ts => ts.StartTime == oldStartTime && ts.EndTime == oldEndTime);

    //    //var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeSlotToUpdate.TiemSlotId);
    //    //if (timeSlotToUpdate == null || timeSlotToUpdate.IsBooked
    //    //    || (checkBookingStatus != null && checkBookingStatus.Status != Status.Canceled))
    //    //{
    //    //    //check if it is already booked
    //    //    // The old timeslot doesn't exist
    //    //    return null;
    //    //}

    //    //// Update the timeslot
    //    //timeSlotToUpdate.StartTime = newStartTime;
    //    //timeSlotToUpdate.EndTime = newEndTime;

    //    //// Save the changes
    //    //await _unitOfWork.Complete();

    //    //return appointment; // Return the updated appointment

    //    #endregion
    //    var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;
    //    TimeOnly newStartTime, newEndTime;
    //    try
    //    {
    //        newStartTime = TimeOnly.Parse(updateTimeSlotDto.NewStartTime);
    //        newEndTime = TimeOnly.Parse(updateTimeSlotDto.NewEndTime);
    //    }catch (Exception)
    //    {
    //        return (IsSuccess: false, Message:"Times is NOT valid..!");
    //    }
    //    var timeSlotToUpdate = await _unitOfWork.TimeSlots
    //        .Find(e => e.TiemSlotId == updateTimeSlotDto.TimeSlotId);

    //    var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeSlotToUpdate.TiemSlotId && (e.DoctorId == doctorId));
    //    //check if it is already booked || The old timeslot doesn't exist
    //    if (timeSlotToUpdate == null || timeSlotToUpdate.IsBooked || checkBookingStatus == null ||
    //        (checkBookingStatus != null && checkBookingStatus.Status != Status.Canceled))
    //    {
    //        return (IsSuccess: false, Message: "TimeSlot is not available..!");
    //    }

    //    //check if startTime is before endTime
    //    if (newStartTime > newEndTime)
    //    {
    //        return (IsSuccess: false, Message: "StartTime is AFTER EndTime..!");
    //    }
    //    timeSlotToUpdate.StartTime = newStartTime;
    //    timeSlotToUpdate.EndTime = newEndTime;
    //    return (IsSuccess: true, Message: "TimeSlot updated Successfully..!");
    //}

    //public async Task<(bool IsSuccess, string Message)> DeleteAppointmentAsync(int timeId, string doctorName)

    //{
    //    var doctorId = _userManager.FindByNameAsync(doctorName).Result!.Id;
    //    var appointment = await _unitOfWork.Appointments.Find(e => e.DoctorId == doctorId);
    //    var timeSlot = await _unitOfWork.TimeSlots.Find(e => e.TiemSlotId == timeId);

    //    if (appointment == null || timeSlot == null)
    //    {
    //        return (IsSuccess: false, Message: "TimeSlot is not available..!");
    //    }
    //    var checkBookingStatus = await _unitOfWork.Bookings.Find(e => e.TimeSlotId == timeId && e.DoctorId == doctorId);

    //    if (checkBookingStatus != null)
    //    {
    //        if (checkBookingStatus.Status != Status.Canceled)
    //            return (IsSuccess: false, Message: "TimeSlot is already within patients records..!");
    //    }

    //    _unitOfWork.TimeSlots.Remove(timeSlot);
    //    await _unitOfWork.Complete();
    //    return (IsSuccess: true, Message: "TimeSlot Deleted..!");
    //}

    public async Task<List<PatientModelDto>> GetAllDoctorPatientsAsync(string userId, Days day,PaginatedSearchModel paginatedSearch)
    {
        paginatedSearch.PageNumber = Math.Max(paginatedSearch.PageNumber, 1);
        var doctor = await _unitOfWork.Doctors.Find(e => e.Email == userId);
        var bookings = _unitOfWork.Bookings.FindAll(e => e.DoctorId == doctor.Id
        && e.Doctor.Appointments.DaySchedules.FirstOrDefault(d => d.TimeSlots.Any(t => t.StartTime == e.TimeSlot.StartTime))!.DayOfWeek == day
        && (e.Status == Status.Pending || e.Status == Status.Completed))
            .Include(d => d.Doctor).Include(e=>e.Doctor.Specialization).Include(e => e.Doctor.Appointments).ThenInclude(e => e.DaySchedules).ThenInclude(e => e.TimeSlots);
        //.ThenInclude(s => s.Specialization)
        //    .Include(a => a.Doctor.Appointments)
        //    .ThenInclude(a => a.DaySchedules)
        //    .ThenInclude(ds => ds.TimeSlots);
        var patientList = bookings
            .Where(b => b.Patient.FirstName.Contains(paginatedSearch.Search)
            || b.Patient.LastName.Contains(paginatedSearch.Search)
            || b.Patient.Email.Contains(paginatedSearch.Search)
            || b.Patient.PhoneNumber!.Contains(paginatedSearch.Search))
            .Select(b => new PatientModelDto(b.Patient, b));
        var result = await patientList
            .Skip((paginatedSearch.PageNumber - 1) * paginatedSearch.PageSize)
            .Take(paginatedSearch.PageSize)
            .ToListAsync();
        return result;
    }

}
