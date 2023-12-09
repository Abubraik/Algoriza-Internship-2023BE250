using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Helpers;
using static Vezeeta.Core.Enums.Enums;
using Vezeeta.Services.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

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

        //need to switch numberOfDoctors with numofrequests or bookings.
        public async Task<List<SpecializationDTO>> Top5Specializations()
        {
            //var top5specializations =await _context.specializations.orderbydescending(e=>e.doctors.count()).tolistasync();
            IQueryable<SpecializationDTO> top5specializations = _unitOfWork.Doctors.GetAll()
                .GroupBy(e => e.Specialization)
                .OrderByDescending(e => e.Count())
                .Take(5)
                .Select(e => new SpecializationDTO { SpecializationName = e.Key.Name, NumberOfDoctors = e.Count() });
            return await top5specializations.ToListAsync();
        }

        //need to make is sort by name if requests is equal.
        public async Task<List<DoctorTop10Dto>> Top10Doctors()
        {
            var top5doctors = await _unitOfWork.Users.GetAll().OfType<Doctor>()
                .OrderByDescending(e => e.Bookings.Count(b => b.Status == Status.Completed))
                .Take(10).Select(e => new DoctorTop10Dto { Image = e.Photo, FullName = e.NormalizedUserName, Specialization = e.Specialization.Name, NumberOfrequests = e.Bookings.Count() })
                .ToListAsync();
            return top5doctors;
        }

        public async Task<Doctor> GetDoctorById(string id)
        {
            Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            return doctor;

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
        //public async Task<Patient> GetPatientById(string id)
        //{
        //    Patient patient = await _unitOfWork.Patients.GetByIdAsync(id);
        //    PatientModelDto patientModel = new PatientModelDto()
        //    {
        //        Image = patient.Photo,
        //        FullName = $"{patient.FirstName + patient.LastName}",
        //        Email = patient.Email,
        //        PhoneNumber = patient.PhoneNumber,
        //        Gender = patient.Gender,
        //        DateOfBirth = patient.DateOfBirth,
        //        Bookings = patient.Bookings.Select(e => new BookingsInfoDto
        //        {
        //            Image = e.Doctor.Photo,
        //            DoctorName = $"{e.Doctor.FirstName + e.Doctor.LastName}",
        //            Specialization = e.Doctor.Specialization.Name,

        //        })
        //        .ToList(),
        //    };

        //    return patient;
        //}
        public async Task<bool> AddDiscountCode(DiscountCodeDto newDiscountCode)
        {
            DiscountCode isCodeRegistered = await _unitOfWork.DiscountCodes
                .Find(e => e.Code == newDiscountCode.Code);
            if(isCodeRegistered != null) return false;
            DiscountCode discountCode = _mapper.Map<DiscountCode>(newDiscountCode);
            await _unitOfWork.DiscountCodes.AddAsync(discountCode);
            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> UpdateDiscountCode(int discoundId, DiscountCodeDto updatedDiscountCode)
        {
            DiscountCode discountCode = await _unitOfWork.DiscountCodes.Find(e=>e.DiscountCodeId == discoundId); //get Discount entity
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
