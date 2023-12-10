using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
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

        public async Task<List<object>> GetTotalRequests()
        {
            var totalRequests = await _unitOfWork.Bookings.GetAll().CountAsync();
            var totalCompletedRequests = await _unitOfWork.Bookings.GetAll().CountAsync(b => b.Status == Status.Completed);
            var totalPendingRequests = await _unitOfWork.Bookings.GetAll().CountAsync(b => b.Status == Status.Pending);
            var totalCancelledRequests = await _unitOfWork.Bookings.GetAll().CountAsync(b => b.Status == Status.Canceled);
            return new List<object> { new  { Total_Requests = totalRequests, Total_Completed_Requests = totalCompletedRequests
                ,Total_Pending_Requests = totalPendingRequests ,Total_Cancelled_Requests =totalCancelledRequests} };
        }

       
    }
}
