using Microsoft.AspNetCore.Mvc;
using Vezeeta.Sevices.Helpers;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IMailService _mailService;
        private string _password;
        public AuthenticationController(IUserManagementService userManagementService, IMailService mailService)
        {
            _userManagementService = userManagementService;
            _mailService = mailService;
            _password = HelperFunctions.GenerateRandomPassword();
        }

        [HttpPost("RegisterPatient")]
        public async Task<IActionResult> RegisterPatient([FromForm]CreatePatientModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _userManagementService.CreateUserAsync(model,_password,User);
            return response.IsSuccess ? Ok(CreateRegistrationResponse(model.Email,_password, response.Response))
                                      : BadRequest(response.Message);
        }

        [HttpPost("Admin/RegisterDoctor")]
        public async Task<IActionResult> RegisterDoctor([FromForm]CreateDoctorModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _userManagementService.CreateUserAsync(model, _password,User);
            return response.IsSuccess ? Ok(CreateRegistrationResponse(model.Email, _password, response.Response))
                                      : BadRequest(response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var (isSuccess, message) = await _userManagementService.AuthenticateUserAsync(model,User);
            return isSuccess ? Ok(message) : BadRequest(message);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var (isSuccess, message) = await _userManagementService.LogoutUserAsync(User);
            return isSuccess ? Ok(message) : BadRequest(message);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var response = await _userManagementService.ConfirmUserEmailAsync(token, email);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        private IActionResult CreateRegistrationResponse(string email,string password, string token)
        {
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token = token, email = email }, HttpContext.Request.Scheme);
            _mailService.SendEmail("Confirmation",email, password, token, confirmationLink);
            return Ok("Account created successfully, please check your email for confirmation");
        }


    }
}