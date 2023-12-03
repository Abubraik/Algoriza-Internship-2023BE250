using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Core.Services;
using Vezeeta.Sevices;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Api.Controllers
{
    /*
     * {
  "firstName": "Abdullah",
  "lastName": "Admin",
  "email": "user@example.com",
  "password": "Abdullah@1234",
  "phoneNumber": "01066147039",
  "gender": "Male",
  "dateOfBirth": "2023-11-29"
}
    */

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (signInManager.IsSignedIn(User))
            {
                return BadRequest(User.Identity?.Name + " is already Logged in");
            }
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {

                    return Ok("Signed in successfully");
                }
            }
            return BadRequest("Invalid User");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!signInManager.IsSignedIn(User))
            {
                return BadRequest("You are already Logged out");
            }
            await signInManager.SignOutAsync();
            return Ok("Signed out successfully");
        }


        
        //[HttpGet]
        //public async Task<IActionResult> get([FromQuery] string id)
        //{
        //    DoctorModel user = await userManager.FindByIdAsync(id) as DoctorModel;

        //    if (user == null) return NotFound("User Not found");

        //    return Ok(user);
        //}
    }
}