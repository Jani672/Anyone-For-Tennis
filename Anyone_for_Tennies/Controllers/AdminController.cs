using Anyone_for_Tennies.Models;
using Anyone_for_Tennies.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // Register a new admin
        [HttpPost("register")]
        public async Task<IActionResult> Register(AdminRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var admin = await _adminService.RegisterAsync(model);
                    return Ok(new { message = "Registration successful", admin });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        // Log in an existing admin
        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var admin = await _adminService.LoginAsync(model);
                    return Ok(new { message = "Login successful", admin });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        // Edit admin information
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, AdminEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedAdmin = await _adminService.EditAsync(id, model);
                    if (updatedAdmin == null)
                        return NotFound(new { message = "Admin not found" });

                    return Ok(new { message = "Account updated successfully", updatedAdmin });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        // Delete admin account
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _adminService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Admin not found" });

                return Ok(new { message = "Account deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
