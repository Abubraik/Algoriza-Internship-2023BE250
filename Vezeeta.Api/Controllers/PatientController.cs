using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

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
            , SignInManager<ApplicationUser> signInManager, IPatientService patientService, IUnitOfWork unitOfWork, IMailService mailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._patientService = patientService;
            this._unitOfWork = unitOfWork;
            this._mailService = mailService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery]int pageNumber, [FromQuery]int pageSize, [FromQuery]string search)
        {
            var result =await _patientService.SearchForDoctors(pageNumber, pageSize, search);
            return Ok(result);
        }
        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _patientService.GetAllBookings(User);
            return Ok(result);
        }
        [HttpPost("BookAppointment")]
        public async Task<IActionResult> BookApointment(int timeId ,string discountCode = null)
        {
            var result =await _patientService.BookAppointment(timeId,User, discountCode);
            _mailService.SendEmail("BookingConfirmation",User.Identity.Name,result.Response);
            return result.IsSuccess? Ok(result) : BadRequest(result);
        }
        [HttpDelete("CancelAppointment")]
        public async Task<IActionResult> CancelAppointment(int BookingId)
        {
            var result = await _patientService.CancelAppointment(BookingId);
            _mailService.SendEmail("BookinCancelation", User.Identity.Name, result.Response);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
