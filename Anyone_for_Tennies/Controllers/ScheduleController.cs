using Anyone_for_Tennies.Models;
using Anyone_for_Tennies.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ScheduleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var schedule = await _scheduleService.CreateAsync(model);
                return Ok(new { message = "Schedule created successfully", schedule });
            }
            return BadRequest(ModelState);
        }

        [HttpGet("view/{id}")]
        public async Task<IActionResult> View(int id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);
            return schedule != null ? Ok(schedule) : NotFound(new { message = "Schedule not found" });
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ScheduleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var updatedSchedule = await _scheduleService.EditAsync(id, model);
                return updatedSchedule != null ? Ok(new { message = "Schedule updated successfully", updatedSchedule }) : NotFound(new { message = "Schedule not found" });
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _scheduleService.DeleteAsync(id);
            return result ? Ok(new { message = "Schedule deleted successfully" }) : NotFound(new { message = "Schedule not found" });
        }
    }
}
