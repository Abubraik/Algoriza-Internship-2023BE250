using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Services.Models.DTOs;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Service.Services
{
    public class AdminService : IAdminService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<int> NumOfDoctors() => await _unitOfWork.Users.GetAll().OfType<Doctor>().CountAsync();

        public async Task<int> NumOfPatients() => await _unitOfWork.Users.GetAll().OfType<Patient>().CountAsync();

        public async Task<List<SpecializationDTO>> Top5Specializations()
        {
            var topSpecializations = await _unitOfWork.Specializations.GetAll()
                .Select(s => new
                {
                    SpecializationName = s.Name,
                    RequestCount = s.Doctors
                        .SelectMany(d => d.Bookings)
                        .Count(r => r.Status == Status.Completed)
                })
                .OrderByDescending(x => x.RequestCount)
                .Take(5)
                .ToListAsync();
            return topSpecializations.Select(x => new SpecializationDTO
            {
                SpecializationName = x.SpecializationName,
                RequestCount = x.RequestCount
            }).ToList();

        }

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

        public async Task<List<object>> GetTotalRequests()
        {
            var totalRequests = await _unitOfWork.Bookings.GetAll().CountAsync();
            var totalCompletedRequests = await _unitOfWork.Bookings.GetAll().CountAsync(b => b.Status == Status.Completed);
            var totalPendingRequests = await _unitOfWork.Bookings.GetAll().CountAsync(b => b.Status == Status.Pending);
            var totalCancelledRequests = await _unitOfWork.Bookings.GetAll().CountAsync(b => b.Status == Status.Canceled);
            return new List<object> { new  { Total_Requests = totalRequests, Total_Completed_Requests = totalCompletedRequests
                ,Total_Pending_Requests = totalPendingRequests ,Total_Cancelled_Requests =totalCancelledRequests} };
        }

        public async Task<object> GetDoctorById(string id)
        {
            Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            _unitOfWork.Doctors.Explicit(doctor).Reference("Specialization").Load();
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
            //int x = _unitOfWork.Appointments.Find(e=>e.doctor.Id == doctor.Id);
            if (doctor != null)
            {
                _unitOfWork.Users.Remove(doctor);
                await _unitOfWork.Complete();
                return true;
            }
            return false;
        }


        //notfinished
        public async Task<PatientModelDto> GetPatientById(string id)
        {
            var patient = await _unitOfWork.Patients.FindAll(p => p.Id == id)
                .Include(e => e.Bookings)
                .ThenInclude(e => e.Doctor)
                .ThenInclude(e => e.Appointments)
                .ThenInclude(e => e.DaySchedules)
                .ThenInclude(e => e.TimeSlots)
                .FirstOrDefaultAsync();
            PatientModelDto patientModel = new PatientModelDto()
            {
                Image = patient.Photo,
                FullName = $"{patient.FirstName + patient.LastName}",
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Gender = patient.Gender,
                DateOfBirth = patient.DateOfBirth,
                Bookings = patient.Bookings.Select(e => new BookingsInfoDto(new DoctorInfoDto(e.Doctor), e))
                .ToList(),
            };

            return patientModel;
        }

        public async Task<List<PatientModelDto>> GetAllPatients(PaginatedSearchModel paginatedSearch)
        {
            paginatedSearch.pageNumber = Math.Max(paginatedSearch.pageNumber, 1);
            var patientQuery = _unitOfWork.Patients.FindAll(p => p.FirstName.Contains(paginatedSearch.search)
            || p.LastName.Contains(paginatedSearch.search)
            || p.Email.Contains(paginatedSearch.search)
            || p.PhoneNumber.Contains(paginatedSearch.search)
            , paginatedSearch.pageNumber
            , paginatedSearch.pageSize);
            var patientList = await patientQuery.Select(p => _mapper.Map<PatientModelDto>(p)).ToListAsync();
            return patientList;
        }
        public async Task<List<DoctorInfoDto>> GetAllDoctors(PaginatedSearchModel paginatedSearch)
        {
            paginatedSearch.pageNumber = Math.Max(paginatedSearch.pageNumber, 1);
            var patientQuery = _unitOfWork.Doctors.FindAll(p => p.FirstName.Contains(paginatedSearch.search)
            || p.LastName.Contains(paginatedSearch.search)
            || p.Email.Contains(paginatedSearch.search)
            || p.PhoneNumber.Contains(paginatedSearch.search)
            , paginatedSearch.pageNumber
            , paginatedSearch.pageSize);
            //EDIT FOR BIRTHDATE
            var patientList = await patientQuery.Select(p => _mapper.Map<DoctorInfoDto>(p)).ToListAsync();
            return patientList;
        }
        public async Task<bool> AddDiscountCode(DiscountCodeDto newDiscountCode)
        {
            DiscountCode isCodeRegistered = await _unitOfWork.DiscountCodes
                .Find(e => e.Code == newDiscountCode.Code);
            if (isCodeRegistered != null) return false;
            DiscountCode discountCode = _mapper.Map<DiscountCode>(newDiscountCode);
            await _unitOfWork.DiscountCodes.AddAsync(discountCode);
            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> UpdateDiscountCode(int discoundId, DiscountCodeDto updatedDiscountCode)
        {
            DiscountCode discountCode = await _unitOfWork.DiscountCodes.Find(e => e.DiscountCodeId == discoundId); //get Discount entity
            if (discountCode == null) return false;
            var isApplied = await _unitOfWork.Bookings.Find(e => e.DiscountCodeId == discountCode.DiscountCodeId);
            if (isApplied != null) return false;
            _mapper.Map(updatedDiscountCode, discountCode);
            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> DeleteDiscountCode(int discoundId)
        {
            DiscountCode discountCode = await _unitOfWork.DiscountCodes.Find(e => e.DiscountCodeId == discoundId); //get Discount entity
            if (discountCode == null) return false;
            var isApplied = await _unitOfWork.Bookings.Find(e => e.DiscountCodeId == discountCode.DiscountCodeId);
            if (isApplied != null) return false;
            _unitOfWork.DiscountCodes.Remove(discountCode);
            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> DeactivateDiscountCode(int discoundId)
        {
            DiscountCode discountCode = await _unitOfWork.DiscountCodes.Find(e => e.DiscountCodeId == discoundId); //get Discount entity
            if (discountCode == null) return false;
            discountCode.IsValid = false;
            await _unitOfWork.Complete();
            return true;
        }


    }
}
