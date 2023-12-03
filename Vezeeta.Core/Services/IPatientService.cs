using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Core.Services
{
    public interface IPatientService
    {
        Task<AccountModel> AddPatient(AccountModel model);

    }
}
