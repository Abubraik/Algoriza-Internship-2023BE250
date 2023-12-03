using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Service.Services;
using Vezeeta.Sevices.Helpers;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterDoctor([FromBody] AccountModel model)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = await adminService.AddDoctor(model);
                if (doctor == null) return NotFound("Specialization not found");
                string password = HelperFunctions.GenerateRandomPassword();
                var result = await userManager.CreateAsync(doctor, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctor, "Doctor");
                    return Ok("Doctor added successfully with id: " + doctor.Id);
                }
                else return BadRequest(result);


            }
            return BadRequest(ModelState);
        }
    }
}
