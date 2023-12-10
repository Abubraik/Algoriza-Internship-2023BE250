using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vezeeta.Core.Models.Users;
using Vezeeta.Services.Models.DTOs;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

namespace Vezeeta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IDiscountCodeService _discountCodeService;
        public AdminController(IAdminService adminService, IDoctorService doctorService,
            IPatientService patientService, IDiscountCodeService discountCodeService)
        {
            _adminService = adminService;
            _doctorService = doctorService;
            _patientService = patientService;
            _discountCodeService = discountCodeService;
        }
        [HttpGet("NumOfDoctors")]
        public async Task<IActionResult> NumOfDoctors()
        {
            return Ok(await _doctorService.NumOfDoctors());
        }

        [HttpGet("NumOfPatients")]
        public async Task<IActionResult> NumOfPatients()
        {
            return Ok(await _patientService.NumOfPatients());
        }

        [HttpGet("NumOfRequests")]
        public async Task<IActionResult> NumOfRequests()
        {
            return Ok(await _adminService.GetTotalRequests());
        }

        [HttpGet("Top5Specializations")]
        public async Task<IActionResult> Top5Specializations()
        {
            return Ok(await _adminService.Top5Specializations());
        }

        [HttpGet("Top10Doctors")]
        public async Task<IActionResult> Top10Doctors()
        {
            var result = await _doctorService.Top10Doctors();
            return Ok(result);
        }


        //  -------------------------------------------------------------------------------------------------------------------------------- //
        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> GetDoctorById([FromQuery] string id)
        {
            var doctor = await _doctorService.GetDoctorById(id);
            if (doctor == null) return NotFound("No Doctor with this ID");
            return Ok(doctor);
        }

        [HttpGet("SearchForDoctors")]
        public async Task<IActionResult> GetAllDoctors([FromQuery] PaginatedSearchModel paginatedSearch)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _doctorService.GetAllDoctors(paginatedSearch));
        }

        [HttpPut("EditDoctor")]
        public async Task<IActionResult> EditDoctor([FromQuery] string id, [FromBody] CreateDoctorModelDto newDoctorInfo)
        {
            if (ModelState.IsValid && !id.IsNullOrEmpty())
            {
                Doctor user = await _doctorService.EditDoctor(id, newDoctorInfo);
                if (user == null) return NotFound("User Not found");
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor([FromQuery] string id)
        {
            if (!id.IsNullOrEmpty())
            {
                return Ok(await _doctorService.DeleteDoctor(id));
            }
            return NotFound();
        }

        //--------------------------------------------------------------Patients------------------------------------------//
        [HttpGet("GetPatient")]
        public async Task<IActionResult> GetPatient([FromQuery] string id) 
        { return Ok(await _patientService.GetPatientById(id)); }

        [HttpGet("SearchForPatients")]
        public async Task<IActionResult> GetAllPatients([FromQuery] PaginatedSearchModel paginatedSearch)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _patientService.GetAllPatients(paginatedSearch)); 
        }


        //-------------------------------------------------------------------------------Discount Codes------------------------------------------//
        [HttpPost("AddDiscountCode")]
        public async Task<IActionResult> AddDiscountCode([FromBody] DiscountCodeDto discountCode) { 
        
            if(ModelState.IsValid)
            {
                if(await _discountCodeService.AddDiscountCode(discountCode))
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
                if (await _discountCodeService.UpdateDiscountCode(discountId, discountCode))
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
                if (await _discountCodeService.DeleteDiscountCode(discountId))
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
                if (await _discountCodeService.DeactivateDiscountCode(discountId))
                    return Ok();
                else
                    return BadRequest();
            }
            return BadRequest(ModelState);
        }

        #region AdminRegiste

        //[HttpPost("registerAdmin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] AccountModelDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = model.Email,
        //            Email = model.Email,
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            NormalizedUserName = model.FirstName + " " + model.LastName,
        //            Gender = model.Gender,
        //            DateOfBirth = model.DateOfBirth,
        //            PhoneNumber = model.PhoneNumber,
        //        };
        //        string password = HelperFunctions.GenerateRandomPassword();
        //        var result = await userManager.CreateAsync(user, password);
        //        if (result.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(user, "Admin");
        //            await signInManager.SignInAsync(user, isPersistent: false);

        //            return StatusCode(201, "Account created successfully with password " + password);
        //        }
        //        else return BadRequest(result);


        //    }
        //    return BadRequest(ModelState);
        //}

        #endregion
    }

}
