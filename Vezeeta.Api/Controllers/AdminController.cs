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

    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;


        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
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

        [HttpGet("NumOfRequests")]
        public async Task<IActionResult> NumOfRequests()
        {
            return Ok(await adminService.GetTotalRequests());
        }

        [HttpGet("Top5Specializations")]
        public async Task<IActionResult> Top5Specializations()
        {
            return Ok(await adminService.Top5Specializations());
        }

        [HttpGet("Top10Doctors")]
        public async Task<IActionResult> Top10Doctors()
        {
            var result = await adminService.Top10Doctors();
            return Ok(result);
        }


        //  -------------------------------------------------------------------------------------------------------------------------------- //
        [HttpGet("Doctor/GetById")]
        public async Task<IActionResult> GetDoctorById([FromQuery] string id)
        {
            var doctor = await adminService.GetDoctorById(id);
            if (doctor == null) return NotFound("No Doctor with this ID");
            return Ok(doctor);
        }

        [HttpGet("SearchForDoctors")]
        public async Task<IActionResult> GetAllDoctors(PaginatedSearchModel paginatedSearch)
        {
            return Ok(await adminService.GetAllDoctors(paginatedSearch));
        }

        [HttpPut("Doctor/Edit")]
        public async Task<IActionResult> EditDoctor([FromQuery] string id, [FromBody] CreateDoctorModelDto newDoctorInfo)
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

        //--------------------------------------------------------------Patients------------------------------------------//
        [HttpGet("GetPatient")]
        public async Task<IActionResult> GetPatient([FromQuery] string id) 
        { return Ok(await adminService.GetPatientById(id)); }

        [HttpGet("SearchForPatients")]
        public async Task<IActionResult> GetAllPatients(PaginatedSearchModel paginatedSearch) 
        {
            return Ok(await adminService.GetAllPatients(paginatedSearch)); 
        }


        //-------------------------------------------------------------------------------Discount Codes------------------------------------------//
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
