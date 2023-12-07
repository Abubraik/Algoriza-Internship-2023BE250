using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Helpers;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Services.Interfaces;

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

        //public async Task<ApiResponse<string>> AddPatient(AccountModelDto model)
        //{
        //    //            //Check user if exist
        //    //            var user = await _userManager.FindByEmailAsync(model.Email);
        //    //            if (user != null) return "This user is Already registered!!";
        //    //            string password = HelperFunctions.GenerateRandomPassword();
        //    //            Patient patient = new Patient()
        //    //            {
        //    //                UserName = model.Email,
        //    //                FirstName = model.FirstName,
        //    //                LastName = model.LastName,
        //    //                Email = model.Email,
        //    //                PhoneNumber = model.PhoneNumber,
        //    //                Gender = model.Gender,
        //    //                DateOfBirth = model.DateOfBirth
        //    //            };
        //    //            var result = await _userManager.CreateAsync(patient, password);
        //    //            if (result.Succeeded)
        //    //            {
        //    //                await _userManager.AddToRoleAsync(patient, "Patient");

        //    //                var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

        //    //                var token = await _userManager.GenerateEmailConfirmationTokenAsync(patient);
        //    //                var confirmationLink = urlHelper.Action("ConfirmEmail", "Account",
        //    //                    new { token, email = patient.Email!}
        //    //                    ,_actionContextAccessor.ActionContext.HttpContext.Request.Scheme);
        //    //                var message = new Message(new string[] { patient.Email },
        //    //                    "Vezeeta Email Confirmation",
        //    //                    $@"<h2>Your UserName: {patient.Email}</h2><br>
        //    //<h2>Your Password: {password}</h2><br>
        //    //<h3>Please use this <a href=""{confirmationLink}"">Link</a> to confirm your email</h3>");
        //    //                _mailService.SendEmail(message);
        //    //                return "OK";
        //    //            }
        //    //                return "Error!";
        //    return ;

        //}

        public async Task<ApiResponse<string>> SearchForDoctors(int page, int pageSize, string search)
        {
            var result = await _unitOfWork.Doctors.GetData(page, pageSize);
            //use result to query the search string and return firstname or secondname or phone number of email that contains "search"
            var r = result.Select(e => new { name = $"{e.FirstName.Contains(search)} {e.LastName.Contains(search)}", });
            return new ApiResponse<string> { IsSuccess = true, StatusCode = 200, Message = "OK" };


        }
    }
}
