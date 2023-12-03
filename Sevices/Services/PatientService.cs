using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Services;
using Vezeeta.Sevices.Helpers;

namespace Vezeeta.Sevices.Services
{
    public class PatientService : IPatientService
    {
        //private readonly UserManager<ApplicationUser> userManager;
        //private readonly SignInManager<ApplicationUser> signInManager;
        //public PatientService(UserManager<ApplicationUser> userManager
        //    , SignInManager<ApplicationUser> signInManager,)
        //{
            
        //}
        public Task<AccountModel> AddPatient(AccountModel model)
        {
            //Patient patient = new Patient()
            //{
            //    photoPath = model.image,
            //    firstName = model.firstName, 
            //    lastName = model.lastName,
            //    Email = model.email,
            //    PhoneNumber = model.phoneNumber,
            //    gender = model.gender,
            //    dateOfBirth = model.dateOfBirth,
            //};
            //string password = HelperFunctions.GenerateRandomPassword();
            //var result = await userManager.CreateAsync(doctor, password);
            //if (result.Succeeded)
            //{
            //    await userManager.AddToRoleAsync(doctor, "Doctor");
            //    return Ok("Doctor added successfully with id: " + doctor.Id);
            //}
            throw new NotImplementedException();
        }

    }
}
