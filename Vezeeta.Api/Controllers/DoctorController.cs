using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        public DoctorController(IDoctorService doctorService, IAppointmentService appointmentService)
        {
            this._doctorService = doctorService;
            _appointmentService = appointmentService;
        }
        [HttpPost("AddAppointment")]
        public async Task<IActionResult> AddAppointmentAsync([FromBody] AddAppointmentDto appointment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _appointmentService.AddAppointmentAsync(appointment, User.Identity!.Name!);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpPut("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointmentTimeAsync([FromBody] UpdateAppointmentTimeDto appointmentDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var appointment = await _appointmentService.UpdateAppointmentTimeAsync(appointmentDto, User.Identity!.Name!);
            if (!appointment.IsSuccess) return BadRequest(appointment.Message);
            return Ok(appointment.Message);

        }

        [HttpDelete("DeletAppointment")]
        public async Task<IActionResult> DeletAppointmentAsync(int timeId)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(timeId, User.Identity!.Name!);
            return result.IsSuccess? Ok(result.Message) : NotFound(result.Message);
        }

        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatientsAsync([FromQuery]Days day, [FromQuery] PaginatedSearchModel paginatedSearch)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); ;
            return Ok(await _doctorService.GetAllDoctorPatientsAsync(User.Identity!.Name!, day, paginatedSearch));
        }

        [HttpPut("ConfirmCheckup")]
        public async Task<IActionResult> ConfirmCheckup(int bookingId)
        {
            var result = await _appointmentService.ConfirmCheckupAsync(bookingId);
            if (!result.isSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
