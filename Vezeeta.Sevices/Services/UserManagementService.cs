using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Models;
using Vezeeta.Sevices.Helpers;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using AutoMapper;
using Vezeeta.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Numerics;
using System.Web.Providers.Entities;
using System.Security.Claims;

namespace Vezeeta.Sevices.Services
{
    public class UserManagementSevice : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserManagementSevice(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }
        //public async Task<ApiResponse<string>> CreateDoctorUser(DoctorModelDto registerDoctor)
        //{
        //    //Check user if exist
        //    var user = await _userManager.FindByEmailAsync(registerDoctor.Email);
        //    if (user != null)
        //        return new ApiResponse<string>
        //        { IsSuccess = false, StatusCode = 403, Message = "User already exists!" };

        //    //Check specialization
        //    Specialization specialization =
        //        await _unitOfWork
        //        .Specializations
        //        .Find(e => e.Name == registerDoctor.Specialization);
        //    if (specialization == null)
        //        return new ApiResponse<string>
        //        { IsSuccess = false, StatusCode = 403, Message = "Specialization NOT found!." };
        //    //Create user
        //    Doctor doctor = _mapper.Map<Doctor>(registerDoctor);
        //    string password = HelperFunctions.GenerateRandomPassword();
        //    var result = await _userManager.CreateAsync(doctor, password);
        //    if (!result.Succeeded)
        //    {
        //        return new ApiResponse<string>
        //        { IsSuccess = false, StatusCode = 500, Message = "User failed to create!" };
        //    }
        //    await _userManager.AddToRoleAsync(doctor, "Doctor");
        //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(doctor);

        //    return new ApiResponse<string>
        //    { IsSuccess = true, StatusCode = 201, Message = "User created successfully!", Response = token };
        //}

        //public async Task<ApiResponse<string>> CreatePatientUser(AccountModelDto registerPatient)
        //{
        //    //Check user if exist
        //    var user = await _userManager.FindByEmailAsync(registerPatient.Email);
        //    if (user != null)
        //        return new ApiResponse<string>
        //        { IsSuccess = false, StatusCode = 403, Message = "User already exists!" };
        //    //Create user
        //    Patient patient = _mapper.Map<Patient>(registerPatient);
        //    var result = await _userManager.CreateAsync(patient, registerPatient._password);
        //    if (!result.Succeeded)
        //    {
        //        return new ApiResponse<string>
        //        { IsSuccess = false, StatusCode = 500, Message = "User failed to create!" };
        //    }
        //    await _userManager.AddToRoleAsync(patient, "Patient");
        //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(patient);

        //    return new ApiResponse<string>
        //    { IsSuccess = true, StatusCode = 201, Message = "User created successfully!", Response = token };
        //}


        //    public async Task<ApiResponse<string>> CreateUser<T>(T registerUser) where T : AccountModelDto
        //    {
        //        var user = await _userManager.FindByEmailAsync(registerUser.Email);
        //        if (user != null)
        //        {
        //            return new ApiResponse<string>
        //            { IsSuccess = false, StatusCode = 403, Message = "User already exists!" };
        //        }

        //        ApplicationUser applicationUser = GetUserEntity(registerUser);
        //        var result = await _userManager.CreateAsync(applicationUser, registerUser._password);

        //        if (!result.Succeeded)
        //        {
        //            return new ApiResponse<string>
        //            { IsSuccess = false, StatusCode = 500, Message = "User failed to create!" };
        //        }

        //        string role = GetRoleForUserType<T>();
        //        await _userManager.AddToRoleAsync(applicationUser, role);
        //        var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

        //        return new ApiResponse<string>
        //        { IsSuccess = true, StatusCode = 201, Message = "User created successfully!", Response = token };
        //    }

        //    private ApplicationUser GetUserEntity<T>(T registerUser) where T : AccountModelDto
        //    {
        //        if (registerUser is DoctorModelDto doctorDto)
        //        {
        //            var specialization = _unitOfWork.Specializations.Find(e => e.Name == doctorDto.Specialization).Result;
        //            if (specialization == null)
        //            {
        //                throw new ArgumentException("Specialization NOT found!.");
        //            }

        //            var doctor = _mapper.Map<Doctor>(registerUser, opts => opts.Items["Specialization"] = specialization);
        //            return doctor;
        //        }
        //        else
        //        {
        //            return _mapper.Map<Patient>(registerUser);
        //        }
        //    }

        //    private static string GetRoleForUserType<T>() where T : AccountModelDto
        //    {
        //        return typeof(T) == typeof(DoctorModelDto) ? "Doctor" : "Patient";
        //    }
        public async Task<ApiResponse<string>> CreateUserAsync<T>(T registerUser) where T : AccountModelDto
        {
            if (await UserExistsAsync(registerUser.Email))
            {
                return new ApiResponse<string>
                { IsSuccess = false, StatusCode = 403, Message = "User already exists!" };

            }

            var applicationUser = GetUserEntity(registerUser);
            var createResult = await _userManager.CreateAsync(applicationUser, registerUser._password);

            if (!createResult.Succeeded)
            {
                return new ApiResponse<string>
                { IsSuccess = false, StatusCode = 500, Message = "User failed to create!" };
            }

            var roleResult = await AssignRoleAsync(applicationUser, GetRoleForUserType<T>());


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            return new ApiResponse<string>
            { IsSuccess = true, StatusCode = 201, Message = "User created successfully!", Response = token };
        }

        // Combine common logic for authentication and user creation
        public async Task<(bool IsSuccess, string Message)> AuthenticateUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return (false, "User Not Found!");
            }

            if (!user.EmailConfirmed)
            {
                return (false, "Email Not Confirmed!");
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return (true, "Signed in successfully");
            }

            return (false, "Password Not Correct");
        }
        public async Task<ApiResponse<string>> ConfirmUserEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ApiResponse<string>
                { IsSuccess = false, StatusCode = 404, Message = "User Not Found!" };
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new ApiResponse<string>
                { IsSuccess = true, StatusCode = 200, Message = "Email Confirmed Successfully!" };
            }
            return new ApiResponse<string>
            { IsSuccess = false, StatusCode = 500, Message = "Email Confirmation Failed!" };

        }

        public async Task<(bool IsSuccess, string Message)> LogoutUserAsync(ClaimsPrincipal User)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return (false, "User Not Signed In!");
            }
            await _signInManager.SignOutAsync();
            return (true, "Signed Out Successfully!");
        }

            // Helper methods
            private async Task<bool> UserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        private ApplicationUser GetUserEntity<T>(T registerUser) where T : AccountModelDto
        {
            if (registerUser is DoctorModelDto doctorDto)
            {
                var specialization = _unitOfWork.Specializations.Find(e => e.Name == doctorDto.Specialization).Result;
                if (specialization == null)
                {
                    throw new ArgumentException("Specialization NOT found!.");
                }

                var doctor = _mapper.Map<Doctor>(registerUser);
                doctor.Specialization = specialization;
                return doctor;
            }
            else
            {
                return _mapper.Map<Patient>(registerUser);
            }
        }

        private async Task<bool> AssignRoleAsync(ApplicationUser user, string role)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            return roleResult.Succeeded;
        }

        private static string GetRoleForUserType<T>() where T : AccountModelDto
        {
            return typeof(T) == typeof(DoctorModelDto) ? "Doctor" : "Patient";
        }
    }



}
