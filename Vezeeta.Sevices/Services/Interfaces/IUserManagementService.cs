using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models.Users;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IUserManagementService
    {
        //Task<ApiResponse<string>> CreateDoctorUser(DoctorModelDto registerDoctor);
        //Task<ApiResponse<string>> CreatePatientUser(AccountModelDto registerPatient);
        Task<ApiResponse<string>> CreateUserAsync<T>(T registerUser) where T : AccountModelDto;
        Task<(bool IsSuccess, string Message)> AuthenticateUserAsync(LoginModel model);
        Task<ApiResponse<string>> ConfirmUserEmailAsync(string token, string email);
        Task<(bool IsSuccess, string Message)> LogoutUserAsync(ClaimsPrincipal User);
    }
}
