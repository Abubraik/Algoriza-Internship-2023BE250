using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<object>> GetTotalRequests();
        Task<List<SpecializationDTO>> Top5Specializations();
    }
}
