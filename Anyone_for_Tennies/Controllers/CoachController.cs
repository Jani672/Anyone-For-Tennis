using Anyone_for_Tennies.Models;
using Anyone_for_Tennies.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;

        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CoachRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var coach = await _coachService.RegisterAsync(model);
                    return Ok(new { message = "Registration successful", coach });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CoachLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var coach = await _coachService.LoginAsync(model);
                    return Ok(new { message = "Login successful", coach });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, CoachEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedCoach = await _coachService.EditAsync(id, model);
                    if (updatedCoach == null)
                        return NotFound(new { message = "Coach not found" });

                    return Ok(new { message = "Account updated successfully", updatedCoach });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _coachService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Coach not found" });

                return Ok(new { message = "Account deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
