// Controllers/EnrollmentController.cs
using Anyone_for_Tennies.Models;
using Anyone_for_Tennies.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // Create an enrollment
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] EnrollmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var enrollment = await _enrollmentService.CreateAsync(model);
                return Ok(new { message = "Enrollment successful", enrollment });
            }
            return BadRequest(ModelState);
        }

        // View enrollments by member
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMember(int memberId)
        {
            var enrollments = await _enrollmentService.GetByMemberIdAsync(memberId);
            return Ok(enrollments);
        }

        // Delete an enrollment
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _enrollmentService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Enrollment not found" });

            return Ok(new { message = "Enrollment deleted successfully" });
        }
    }
}
