using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices;
using Vezeeta.Sevices.Helpers;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IUserManagementSevice _userManagementSevice;
        //private readonly IMailService _mailService;

        //public AccountController(UserManager<ApplicationUser> userManager
        //    , SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork,
        //    IUserManagementSevice userManagementSevice, IMailService mailService)
        //{
        //    this._userManager = userManager;
        //    this._signInManager = signInManager;
        //    this._unitOfWork = unitOfWork;
        //    this._userManagementSevice = userManagementSevice;
        //    this._mailService = mailService;

        //}

        //[HttpPost("/RegisterPaitent")]
        //public async Task<IActionResult> RegisterUser(AccountModelDto user)
        //{
        //    string password = HelperFunctions.GenerateRandomPassword();
        //    user._password = password;
        //    var token = await _userManagementSevice.CreateUser(user);
        //    if (token.IsSuccess)
        //    {
        //        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account"
        //            , new { token = token.Response, email = user.Email }, HttpContext.Request.Scheme);
        //        _mailService.TestSendEmail("Confirmation", user.Email, password, token.Response, confirmationLink);
        //        return Ok("Account created successfully, please check you email for confirmation");
        //    }
        //    //_mailService.SendEmail(message);
        //    return BadRequest(token.Message);

        //}

        //[HttpPost("Admin/RegisterDoctor")]
        //public async Task<IActionResult> RegisterDoctor(DoctorModelDto user)
        //{
        //    string password = HelperFunctions.GenerateRandomPassword();
        //    user._password = password;
        //    var token = await _userManagementSevice.CreateUser(user);
        //    if (token.IsSuccess)
        //    {
        //        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authenticate"
        //            , new { token = token.Response, email = user.Email }, HttpContext.Request.Scheme);
        //        _mailService.TestSendEmail("Confirmation", user.Email, password, token.Response, confirmationLink);
        //        return Ok("Account created successfully, please check you email for confirmation");
        //    }
        //    //_mailService.SendEmail(message);
        //    return BadRequest(token.Message);

        //}

        //[HttpPost("/login")]
        //public async Task<IActionResult> Login(LoginModel model)
        //{

        //    if (_signInManager.IsSignedIn(User))
        //    {
        //        return BadRequest(User.Identity?.Name + " is already Logged in");
        //    }
        //    if (ModelState.IsValid)
        //    {   //Check if User Exists
        //        var user = await _userManager.FindByNameAsync(model.Email);
        //        if (user == null) return BadRequest("User Not Found!");

        //        if (user.EmailConfirmed == true)
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, false);
        //            if (result.Succeeded)
        //            {
        //                return Ok("Signed in successfully");
        //            }
        //            return BadRequest("Password Not Correct");
        //        }

        //        return BadRequest("Emai Not Confirmed!");
        //    }
        //    return BadRequest(ModelState);
        //}

        //[HttpPost("/logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    if (!_signInManager.IsSignedIn(User))
        //    {
        //        return BadRequest("You are already Logged out");
        //    }
        //    await _signInManager.SignOutAsync();
        //    return Ok("Signed out successfully");
        //}

        //[HttpGet("ConfirmEmail")]
        //public async Task<IActionResult> ConfirmEmail([FromQuery] string token,string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if(user!=null)
        //    {
        //        var result = await _userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            return Ok("Thankyou, Email Confirmed Successfully! ");
        //        }
        //    }
        //    return BadRequest("This user is NOT valid! ");
        //}

        private readonly IUserManagementService _userManagementService;
        private readonly IMailService _mailService;

        public AuthenticationController(IUserManagementService userManagementService, IMailService mailService)
        {
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpPost("RegisterPatient")]
        public async Task<IActionResult> RegisterPatient(AccountModelDto model)
        {
            var response = await _userManagementService.CreateUserAsync(model);
            return response.IsSuccess ? Ok(CreateRegistrationResponse(model, response.Response))
                                      : BadRequest(response.Message);
        }

        [HttpPost("Admin/RegisterDoctor")]
        public async Task<IActionResult> RegisterDoctor(DoctorModelDto model)
        {
            var response = await _userManagementService.CreateUserAsync(model);
            return response.IsSuccess ? Ok(CreateRegistrationResponse(model, response.Response))
                                      : BadRequest(response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var (isSuccess, message) = await _userManagementService.AuthenticateUserAsync(model);
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

        private object CreateRegistrationResponse(AccountModelDto model, string token)
        {
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = model.Email }, HttpContext.Request.Scheme);
            _mailService.SendEmail("Confirmation",model.Email, model._password, token, confirmationLink);
            return new { Message = "Account created successfully, please check your email for confirmation" };
        }


    }
}