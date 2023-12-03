using Vezeeta.Core.Services;
using Vezeeta.Core.Repositories;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
using static Vezeeta.Core.Enums.Enums;
using AutoMapper;
using Vezeeta.Core.DTOs;
using Vezeeta.Sevices.Helpers;

namespace Vezeeta.Service.Services
{
    public class AdminService : IAdminService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unitOfWork,IMapper mapper)
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
            IQueryable<SpecializationDTO> top5specializations = _unitOfWork.Users.GetAll().OfType<Doctor>()
                .GroupBy(e => e.specialization)
                .OrderByDescending(e => e.Count())
                .Take(5)
                .Select(e => new SpecializationDTO { specializationName = e.Key.name, numberOfDoctors = e.Count() });
            return await top5specializations.ToListAsync();
        }

        //need to make is sort by name if requests is equal.
        public async Task<List<DoctorTop10Dto>> Top10Doctors()
        {
            var top5doctors = await _unitOfWork.Users.GetAll().OfType<Doctor>()
                .OrderByDescending(e => e.bookings.Count(b => b.status == Status.Completed))
                .Take(10).Select(e=> new DoctorTop10Dto { image = e.photoPath,fullName = e.NormalizedUserName,specialization = e.specialization.name,numberOfrequests = e.bookings.Count()})
                .ToListAsync();
            return top5doctors;
        }

        public async Task<Doctor> GetDoctorById(string id)
        {
            Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            //if (doctor == null) return null;
            //DoctorModel model = new DoctorModel
            //{
            //    image = doctor.photoPath,
            //    fullName = doctor.NormalizedUserName,
            //    email = doctor.Email,
            //    phoneNumber = doctor.PhoneNumber,
            //    specialization = doctor.specialization.name,
            //    gender = doctor.gender,
            //    dateOfBirth = doctor.dateOfBirth,
            //};
            return doctor;

        }

        public async Task<Doctor> AddDoctor(DoctorModel model)
        {
            Specialization specialization =
                await _unitOfWork
                .Specializations
                .Find(e => e.name == model.specialization);
            if(specialization == null ) return null;
             Doctor doctor = new Doctor()
            {
                UserName = model.email,
                Email = model.email,
                firstName = model.firstName,
                lastName = model.lastName,
                NormalizedUserName = model.firstName + " " + model.lastName,
                gender = model.gender,
                dateOfBirth = model.dateOfBirth,
                PhoneNumber = model.phoneNumber,
                photoPath = "Doctor",
                specialization = specialization
            };
            string password = HelperFunctions.GenerateRandomPassword();

            //await _unitOfWork.Users.AddAsync(doctor);

            return doctor;
    }


        public async Task<Doctor> EditDoctor(string id,DoctorModel newDoctorInfo)
        {
            Doctor doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            doctor.firstName = newDoctorInfo.firstName;
            doctor.lastName = newDoctorInfo.lastName;
            doctor.dateOfBirth = newDoctorInfo.dateOfBirth;
            doctor.gender = newDoctorInfo.gender;
            doctor.Email = newDoctorInfo.email;
            doctor.specialization = await _unitOfWork.Specializations.Find(e=>e.name == newDoctorInfo.specialization);
            doctor.PhoneNumber = newDoctorInfo.phoneNumber;
            
            await _unitOfWork.Doctors.UpdateAsync(doctor);
            await _unitOfWork.Complete();
            return doctor;

        }
        public async Task<bool> DeleteDoctor(string id)
        {
            Doctor doctor= await _unitOfWork.Doctors.GetByIdAsync(id);
            //int x = _unitOfWork.Appointments.Find(e=>e.doctor.Id == doctor.Id);
            if(doctor!=null)
            {
                _unitOfWork.Users.Remove(doctor);
                await _unitOfWork.Complete();
                return true;
            }
            return false;
        }
        public async Task<Patient> GetPatientById(string id)
        {
            Patient patient = await _unitOfWork.Patients.GetByIdAsync(id);
            PatientModel patientModel = new PatientModel()
            {
                image = patient.photoPath,
                fullName = $"{patient.firstName + patient.lastName}",
                email = patient.Email,
                phoneNumber = patient.PhoneNumber,
                gender = patient.gender,
                dateOfBirth = patient.dateOfBirth,
                bookings = patient.bookings.Select(e=> new BookingsDTO 
                {
                    image = e.doctor.photoPath,
                    doctorName =$"{e.doctor.firstName + e.doctor.lastName}",
                    specialization = e.doctor.specialization.name,
                    
                })
                .ToList(), 
            };
            
            return patient;
        }
            public async Task<bool> AddDiscountCode(DiscountCode discountcode)
        {
            throw new NotImplementedException();

        }

        public Task<bool> UpdateDiscountCode()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscountCode()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateDiscountCode()
        {
            throw new NotImplementedException();
        }

      
    }
}
