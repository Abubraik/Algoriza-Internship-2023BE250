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
        var patientQuery = await _unitOfWork.Doctors.FindAll(p => p.FirstName.Contains(paginatedSearch.Search)
        || p.LastName.Contains(paginatedSearch.Search)
        || p.Email.Contains(paginatedSearch.Search)
        || p.PhoneNumber.Contains(paginatedSearch.Search)
        , paginatedSearch.PageNumber
        , paginatedSearch.PageSize).ToListAsync();

        var patientList = patientQuery.Select(p => new DoctorInfoDto(p)).ToList();
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
        , paginatedSearch.PageSize);

        var doctorList = await doctorsQuery.Select(d => new DoctorInfoDto(d)).ToListAsync();

        return doctorList;
    }

    public async Task<object> GetDoctorById(string id)
    {
        Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor == null) return new {Message = "ID not valid..!"};
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
        doctor.Specialization = await _unitOfWork.Specializations.FindAsync(e => e.Name == newDoctorInfo.Specialization);
        doctor.PhoneNumber = newDoctorInfo.PhoneNumber;

        await _unitOfWork.Doctors.UpdateAsync(doctor);
        await _unitOfWork.CompleteAsync();
        return doctor;

    }
    public async Task<bool> DeleteDoctor(string id)
    {
        Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor != null && !doctor.Bookings.Any())
        {
            _unitOfWork.Users.Remove(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        return false;
    }


    public async Task<List<PatientModelDto>> GetAllDoctorPatientsAsync(string userId, Days day,PaginatedSearchModel paginatedSearch)
    {
        paginatedSearch.PageNumber = Math.Max(paginatedSearch.PageNumber, 1);
        var doctor = await _unitOfWork.Doctors.FindAsync(e => e.Email == userId);
        var bookings = await _unitOfWork.Bookings.FindAll(e => e.DoctorId == doctor.Id
        && e.Doctor.Appointments.DaySchedules.FirstOrDefault(d => d.TimeSlots.Any(t => t.StartTime == e.TimeSlot.StartTime))!.DayOfWeek == day
        && (e.Status == Status.Pending || e.Status == Status.Completed)).ToListAsync();

        var patientList = bookings
            .Where(b => b.Patient.FirstName.Contains(paginatedSearch.Search)
            || b.Patient.LastName.Contains(paginatedSearch.Search)
            || b.Patient.Email.Contains(paginatedSearch.Search)
            || b.Patient.PhoneNumber!.Contains(paginatedSearch.Search))
            .Select(b => new PatientModelDto(b.Patient, b)).ToList();
        var result =  patientList
            .Skip((paginatedSearch.PageNumber - 1) * paginatedSearch.PageSize)
            .Take(paginatedSearch.PageSize).ToList();
        return result;
    }

}
