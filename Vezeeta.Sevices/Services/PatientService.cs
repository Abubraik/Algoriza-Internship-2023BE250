using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<int> NumOfPatients() => await _unitOfWork.Users.GetAll().OfType<Patient>().CountAsync();
        public async Task<List<PatientModelDto>> GetAllPatients(PaginatedSearchModel paginatedSearch)
        {
            paginatedSearch.PageNumber = Math.Max(paginatedSearch.PageNumber, 1);
            var patientQuery =await _unitOfWork.Patients.FindAll(p => p.FirstName.Contains(paginatedSearch.Search)
            || p.LastName.Contains(paginatedSearch.Search)
            || p.Email.Contains(paginatedSearch.Search)
            || p.PhoneNumber!.Contains(paginatedSearch.Search)
            , paginatedSearch.PageNumber
            , paginatedSearch.PageSize).ToListAsync();
            var patientList = patientQuery.Select(p => _mapper.Map<PatientModelDto>(p)).ToList();
            return patientList;
        }
        public async Task<PatientModelDto> GetPatientById(string id)
        {
            var patient = await _unitOfWork.Patients.FindAll(p => p.Id == id)
                .FirstOrDefaultAsync();
            if(patient == null)  return null; 
            return _mapper.Map<PatientModelDto>(patient);
        }


    }
}
