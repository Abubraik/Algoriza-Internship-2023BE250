using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

namespace Vezeeta.Sevices.Services
{
    public class UserManagementSevice : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserManagementSevice(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._hostingEnvironment = hostingEnvironment;
        }

        public async Task<ApiResponse<string>> CreateUserAsync<T>(T registerUser, string _password, ClaimsPrincipal User) where T : AccountModelDto
        {
            if (await UserExistsAsync(registerUser.Email))
            {
                return new ApiResponse<string>
                { IsSuccess = false, StatusCode = 403, Message = "User already exists!" };
            }
            if (CheckIfUserSignedIn(User).Result)
                return new ApiResponse<string> { IsSuccess = false, StatusCode = 403, Message = "Please Logout first..!" };

            string photoPath = null;

            if (registerUser is CreateDoctorModelDto doctorDto && doctorDto.Photo != null)
            {
                // If it's a doctor registration, the photo is required.
                photoPath = await SavePhotoAsync(doctorDto.Photo);
                if (photoPath == null)
                {
                    return new ApiResponse<string>
                    { IsSuccess = false, StatusCode = 400, Message = "Photo is required for doctors." };
                }
            }
            else if (registerUser is CreatePatientModel patientDto && patientDto.Photo != null)
            {
                // If it's a patient registration and a photo is provided, it's optional.
                photoPath = await SavePhotoAsync(patientDto.Photo);
            }

            var applicationUser = GetUserEntity(registerUser, photoPath);
            var createResult = await _userManager.CreateAsync(applicationUser, _password);

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

        //    Combine common logic for authentication and user creation

        public async Task<(bool IsSuccess, string Message)> AuthenticateUserAsync(LoginModel model, ClaimsPrincipal User)
        {
            if (CheckIfUserSignedIn(User).Result)
                return (false, User.Identity?.Name + " is already Logged in");
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
            if (!CheckIfUserSignedIn(User).Result)
            {
                return (false, "User Not Signed In!");
            }
            await _signInManager.SignOutAsync();
            return (true, "Signed Out Successfully!");
        }
        // Helper methods
        private async Task<bool> CheckIfUserSignedIn(ClaimsPrincipal User)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return false;
            }
            return true;
        }
        private async Task<bool> UserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        private ApplicationUser GetUserEntity<T>(T registerUser, string photoPath = "") where T : AccountModelDto
        {
            ApplicationUser applicationUser = null;
            if (registerUser is CreateDoctorModelDto doctorDto)
            {
                var specialization = _unitOfWork.Specializations.Find(e => e.Name == doctorDto.Specialization).Result;
                if (specialization == null)
                {
                    throw new ArgumentException("Specialization NOT found!.");
                }

                var doctor = _mapper.Map<Doctor>(doctorDto, opts => opts.Items["Photo"] = photoPath); // Assuming the mapper is configured properly to handle Doctor mapping
                doctor.Specialization = specialization;
                //doctor.Photo = photoPath;
                applicationUser = doctor;
            }
            else if (registerUser is CreatePatientModel patientDto)
            {
                var patient = _mapper.Map<Patient>(patientDto, opts => opts.Items["Photo"] = photoPath); // Assuming the mapper is configured properly to handle Patient mapping
                //patient.Photo = photoPath;
                applicationUser = patient;
            }

            if (applicationUser == null)
            {
                throw new InvalidOperationException("The user type is not recognized for mapping.");
            }

            return applicationUser;
        }

        private async Task<bool> AssignRoleAsync(ApplicationUser user, string role)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            return roleResult.Succeeded;
        }

        private static string GetRoleForUserType<T>() where T : AccountModelDto
        {
            return typeof(T) == typeof(CreateDoctorModelDto) ? "Doctor" : "Patient";
        }
        private async Task<string> SavePhotoAsync(IFormFile photo)
        {
            if (photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                return uniqueFileName;
            }

            return null; // Return null if photo is not provided (optional for patients)
        }

    }

}
