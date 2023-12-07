using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Models;
using Vezeeta.Sevices.Helpers;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using AutoMapper;
using Vezeeta.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Vezeeta.Sevices.Services
{
    public class UserManagementSevice : IUserManagementSevice
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserManagementSevice(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._mapper = mapper;
        }
        public async Task<ApiResponse<string>> CreateDoctorUser(DoctorModelDto registerDoctor)
        {
            //Check user if exist
            var user = await _userManager.FindByEmailAsync(registerDoctor.Email);
            if (user != null)
                return new ApiResponse<string>
                { IsSuccess = false, StatusCode = 403, Message = "User already exists!" };

            //Check specialization
            Specialization specialization =
                await _unitOfWork
                .Specializations
                .Find(e => e.Name == registerDoctor.Specialization);
            if (specialization == null) 
                return  new ApiResponse<string> 
                {IsSuccess = false,StatusCode = 403,Message = "Specialization NOT found!."};
            //Create user
            Doctor doctor = _mapper.Map<Doctor>(registerDoctor);
            string password = HelperFunctions.GenerateRandomPassword();
            var result = await _userManager.CreateAsync(doctor, password);
            if (!result.Succeeded)
            {
                return new ApiResponse<string>
                { IsSuccess = false, StatusCode = 500, Message = "User failed to create!" };
            }
            await _userManager.AddToRoleAsync(doctor, "Doctor");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(doctor);

            return new ApiResponse<string>
            { IsSuccess = true, StatusCode = 201, Message = "User created successfully!",Response=token };
        }

        public async Task<ApiResponse<string>> CreatePatientUser(AccountModelDto registerPatient)
        {
            //Check user if exist
            var user = await _userManager.FindByEmailAsync(registerPatient.Email);
            if (user != null)
                return new ApiResponse<string>
                { IsSuccess = false, StatusCode = 403, Message = "User already exists!" };
            //Create user
            Patient patient = _mapper.Map<Patient>(registerPatient);
            var result = await _userManager.CreateAsync(patient, registerPatient._password);
            if (!result.Succeeded)
            {
                return new ApiResponse<string>
                { IsSuccess = false, StatusCode = 500, Message = "User failed to create!" };
            }
            await _userManager.AddToRoleAsync(patient, "Patient");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(patient);

            return new ApiResponse<string>
            { IsSuccess = true, StatusCode = 201, Message = "User created successfully!", Response = token };
        }
    }
}
