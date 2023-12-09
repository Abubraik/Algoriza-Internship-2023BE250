using Microsoft.AspNetCore.Mvc;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this._doctorService = doctorService;

        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddAppointmentAsync([FromBody] AddAppointmentDto appointment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _doctorService.AddAppointmentAsync(appointment, User.Identity!.Name!);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAppointmentTimeAsync([FromBody] UpdateAppointmentTimeDto appointmentDto)
        {
            var appointment = await _doctorService.UpdateAppointmentTimeAsync(appointmentDto, User.Identity!.Name!);
            if (!appointment.IsSuccess) return BadRequest(appointment.Message);
            return Ok(appointment.Message);

        }

        [HttpDelete("delete")]
        public async Task<bool> DeletAppointmentAsync(int timeId)
        {
            return (await _doctorService.DeleteAppointmentAsync(timeId,User.Identity!.Name!)).IsSuccess? true : false;
        }
    }
}
