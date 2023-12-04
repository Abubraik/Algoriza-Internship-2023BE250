using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Core.Services;
using Vezeeta.Service.Services;
using Vezeeta.Sevices.Helpers;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Services;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;

        public PatientController(UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager, IPatientService patientService,IUnitOfWork unitOfWork, IMailService mailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._patientService = patientService;
            this._unitOfWork = unitOfWork;
            this._mailService = mailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPatient([FromBody] AccountModel model)
        {
            if (ModelState.IsValid)
            {

                string result= await _patientService.AddPatient(model);
                if (result == "OK") 
                    return Ok("Registered successfully, please check you email for confirmation");
                
                return BadRequest(result);
                
            }
            return BadRequest(ModelState);
        }
        [HttpGet("TEST")]
        public IActionResult TestEmail()
        {
            var message = new Message(new string[] { "Abdullah.abubraik@gmail.com" }, "Testing", "<h2>This is a TEst</h2>");
            _mailService.SendEmail(message);
            return Ok("Sent");
        }
    }
}
