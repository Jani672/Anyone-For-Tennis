using Anyone_for_Tennies.Models;
using Anyone_for_Tennies.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Register a new member
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var member = await _accountService.RegisterAsync(model);
                    return Ok(new { message = "Registration successful", member });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        // Log in an existing member
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var member = await _accountService.LoginAsync(model);
                    return Ok(new { message = "Login successful", member });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        // Edit member information
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedMember = await _accountService.EditAsync(id, model);
                    if (updatedMember == null)
                        return NotFound(new { message = "Member not found" });

                    return Ok(new { message = "Account updated successfully", updatedMember });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        // Delete member account
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _accountService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Member not found" });

                return Ok(new { message = "Account deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
