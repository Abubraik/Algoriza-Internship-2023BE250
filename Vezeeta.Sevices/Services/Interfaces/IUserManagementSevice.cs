using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IUserManagementSevice
    {
        Task<ApiResponse<string>> CreateDoctorUser(DoctorModelDto registerDoctor);
        Task<ApiResponse<string>> CreatePatientUser(AccountModelDto registerPatient);
    }
}
