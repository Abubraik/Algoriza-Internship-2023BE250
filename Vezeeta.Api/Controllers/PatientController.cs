using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;

        private readonly IMailService _mailService;

        public PatientController(IPatientService patientService, IMailService mailService, IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _patientService = patientService;
            _mailService = mailService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        [HttpGet("SearchForDoctorsAppointments")]
        public async Task<IActionResult> Search([FromQuery]PaginatedSearchModel paginatedSearch)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _doctorService.SearchForDoctorsAppointments(paginatedSearch);
            return Ok(result);
        }
        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _appointmentService.GetAllBookings(User.Identity!.Name!);
            return Ok(result);
        }
        [HttpPost("BookAppointment")]
        public async Task<IActionResult> BookApointment(int timeId, string discountCode = null)
        {
            var result = await _appointmentService.BookAppointment(timeId, User.Identity!.Name!, discountCode);
            if(result.IsSuccess)
                _mailService.SendEmail("Booking Confirmation", User.Identity.Name, result.Response);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("CancelAppointment")]
        public async Task<IActionResult> CancelAppointment(int BookingId)
        {
            var result = await _appointmentService.CancelBookingAsync(BookingId);
            _mailService.SendEmail("Booking Cancelation", User.Identity!.Name, result.Response);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
