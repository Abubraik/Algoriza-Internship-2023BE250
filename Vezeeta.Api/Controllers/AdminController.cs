using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Core.Services;
using Vezeeta.Sevices.Helpers;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAdminService adminService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminController(UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager, IAdminService adminService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.adminService = adminService;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet("NumOfDoctors")]
        public async Task<IActionResult> NumOfDoctors()
        {
            return Ok(await adminService.NumOfDoctors());
        }

        [HttpGet("NumOfPatients")]
        public async Task<IActionResult> NumOfPatients()
        {
            return Ok(await adminService.NumOfPatients());
        }

        [HttpGet("Top5Specializations")]
        public async Task<IActionResult> Top5Specializations()
        {
            return Ok(await adminService.Top5Specializations());
        }
        [HttpGet("Top10Doctors")]
        public async Task<IActionResult> Top10Doctors()
        {
            return Ok(await adminService.Top10Doctors());
        }
    

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AccountModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.email,
                    Email = model.email,
                    firstName = model.firstName,
                    lastName = model.lastName,
                    NormalizedUserName = model.firstName + " " + model.lastName,
                    gender = model.gender,
                    dateOfBirth = model.dateOfBirth,
                    PhoneNumber = model.phoneNumber,
                };
                string password = HelperFunctions.GenerateRandomPassword();
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return StatusCode(201, "Account created successfully with password " + password);
                }
                else return BadRequest(result);


            }
            return BadRequest(ModelState);
        }

        [HttpGet("Doctor/GetById")]
        public async Task<IActionResult> GetDoctorById([FromQuery] string id)
        {
            Doctor doctor = await adminService.GetDoctorById(id);
            if (doctor == null) return NotFound("No Doctor with this ID");
            var model = new
            {
                image = doctor.photoPath,
                fullName = $"{doctor.firstName} {doctor.lastName}",
                email = doctor.Email,
                phoneNumber = doctor.PhoneNumber,
                specialization = doctor.specialization.name,
                gender = doctor.gender,
                dateOfBirth = doctor.dateOfBirth,
            };
            return Ok(model);
        }

        [HttpPost("Doctor/Add")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorModel model)
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

        [HttpPut("Doctor/Edit")]
        public async Task<IActionResult> EditDoctor([FromQuery] string id, [FromBody] DoctorModel newDoctorInfo)
        {
            if (ModelState.IsValid && !id.IsNullOrEmpty())
            {
                Doctor user = await adminService.EditDoctor(id, newDoctorInfo);
                if (user == null) return NotFound("User Not found");
                //DoctorModel d = _mapper.Map<DoctorModel>(user);
                return Ok(user);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("Doctor/Delete")]
        public async Task<IActionResult> DeleteDoctor([FromQuery] string id)
        {
            if (!id.IsNullOrEmpty())
            {
              return Ok(await adminService.DeleteDoctor(id));
            }
            return NotFound();
        }

        //public async Task<IActionResult> GetPatientById(string id)
        //{

        //}
    }

    //public static List<T> GetDate(int pageNumber,int PageSize) 
    //{

    //    retrun context.stocks.Skip((pageNumber - 1) * PageSize).Take(PageSize);
    //}
}
