using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

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
            //@e25pq9K!U
            Appointment newAppointment = await _doctorService.AddAppointmentAsync(appointment, User.Identity!.Name!);
            if (newAppointment == null) return BadRequest("Could not add appointment");
            return Ok(newAppointment);
            //return View();
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
