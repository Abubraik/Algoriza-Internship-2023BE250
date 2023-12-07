using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Services.Models.DTOs;
using Vezeeta.Sevices.Helpers;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

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
        public async Task<IActionResult> RegisterAdmin([FromBody] AccountModelDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NormalizedUserName = model.FirstName + " " + model.LastName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    PhoneNumber = model.PhoneNumber,
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
                image = doctor.Photo,
                fullName = $"{doctor.FirstName} {doctor.LastName}",
                email = doctor.Email,
                phoneNumber = doctor.PhoneNumber,
                specialization = doctor.Specialization.Name,
                gender = doctor.Gender,
                dateOfBirth = doctor.DateOfBirth,
            };
            return Ok(model);
        }

        [HttpPost("Doctor/Add")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorModelDto model)
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
                    return Ok("Doctor added successfully with id: " + doctor.Id + " Password: " + password);
                }
                else return BadRequest(result);


            }
            return BadRequest(ModelState);
        }
      //  -------------------------------------------------------------------------------------------------------------------------------- //
        [HttpPut("Doctor/Edit")]
        public async Task<IActionResult> EditDoctor([FromQuery] string id, [FromBody] DoctorModelDto newDoctorInfo)
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

        [HttpPost("AddDiscountCode")]
        public async Task<IActionResult> AddDiscountCode([FromBody] DiscountCodeDto discountCode) { 
        
            if(ModelState.IsValid)
            {
                if(await adminService.AddDiscountCode(discountCode))
                    return Ok();
                else
                    return BadRequest();
            }    
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateDiscountCode")]
        public async Task<IActionResult> UpdateDiscounCode([FromQuery] int discountId, [FromBody] DiscountCodeDto discountCode)
        {
            if (ModelState.IsValid)
            {
                if (await adminService.UpdateDiscountCode(discountId, discountCode))
                    return Ok();
                else
                    return BadRequest();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteDiscounCode")]
        public async Task<IActionResult> DeleteDiscounCode([FromQuery] int discountId)
        {
            if (ModelState.IsValid)
            {
                if (await adminService.DeleteDiscountCode(discountId))
                    return Ok();
                else
                    return BadRequest();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("DeactivateDiscounCode")]
        public async Task<IActionResult> DeactivateDiscounCode([FromQuery] int discountId)
        {
            if (ModelState.IsValid)
            {
                if (await adminService.DeactivateDiscountCode(discountId))
                    return Ok();
                else
                    return BadRequest();
            }
            return BadRequest(ModelState);
        }
    }

    //public static List<T> GetDate(int pageNumber,int PageSize) 
    //{

    //    retrun context.stocks.Skip((pageNumber - 1) * PageSize).Take(PageSize);
    //}
}
