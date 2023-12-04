using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Core.Services;
using Vezeeta.Repository.Repositories;
using Vezeeta.Sevices.Helpers;
using Vezeeta.Sevices.Models;

namespace Vezeeta.Sevices.Services
{
    public class PatientService : IPatientService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        public PatientService(UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, IMailService mailService, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._unitOfWork = unitOfWork;
            this._mailService = mailService;
            this._urlHelperFactory = urlHelperFactory;
            this._actionContextAccessor = actionContextAccessor;
        }
        public async Task<String> AddPatient(AccountModel model)
        {
            //Check user if exist
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user != null) return "This user is Already registered!!";
            string password = HelperFunctions.GenerateRandomPassword();
            Patient patient = new Patient()
            {
                UserName = model.email,
                firstName = model.firstName,
                lastName = model.lastName,
                Email = model.email,
                PhoneNumber = model.phoneNumber,
                gender = model.gender,
                dateOfBirth = model.dateOfBirth
            };
            var result = await _userManager.CreateAsync(patient, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(patient, "Patient");

                var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(patient);
                var confirmationLink = urlHelper.Action("ConfirmEmail", "Account",
                    new { token, email = patient.Email!}
                    ,_actionContextAccessor.ActionContext.HttpContext.Request.Scheme);
                var message = new Message(new string[] { patient.Email },
                    "Vezeeta Email Confirmation",
                    $@"<h2>Your UserName: {patient.Email}</h2><br>
<h2>Your Password: {password}</h2><br>
<h3>Please use this <a href=""{confirmationLink}"">Link</a> to confirm your email</h3>");
                _mailService.SendEmail(message);
                return "OK";
            }
                return "Error!";
            
        }

    }
}
