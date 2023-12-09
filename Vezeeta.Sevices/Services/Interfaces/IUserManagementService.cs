using System.Security.Claims;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IUserManagementService
    {
        Task<ApiResponse<string>> CreateUserAsync<T>(T registerUser, string password, ClaimsPrincipal User) where T : AccountModelDto;
        Task<(bool IsSuccess, string Message)> AuthenticateUserAsync(LoginModel model, ClaimsPrincipal user);
        Task<ApiResponse<string>> ConfirmUserEmailAsync(string token, string email);
        Task<(bool IsSuccess, string Message)> LogoutUserAsync(ClaimsPrincipal User);
    }
}
