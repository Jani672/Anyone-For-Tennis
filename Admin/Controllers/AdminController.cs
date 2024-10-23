using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using assignment3.Models;
using assignment3.Service;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize(Roles = "Admin")] // Restricts access to users in the Admin role
public class AdminController : Controller
{
    
    private readonly AdminService _adminService;

    public AdminController(AdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public IActionResult CreateSchedule()
    {
        var coaches = _adminService.GetCoaches();
        return View(coaches); // Pass coaches to the view for selection
    }

    [HttpPost]
    public IActionResult CreateSchedule(string name,DateTime date, string location, int coachId, string description)
    {
        _adminService.CreateSchedule(name, date,location, coachId, description);
        return RedirectToAction("ViewSchedules");
    }

    [HttpGet]
    public IActionResult ViewSchedules()
    {
        var schedules = _adminService.GetSchedules();
        return View(schedules); // Pass schedules to the view
    }

    [HttpGet]
    public IActionResult ViewMembers()
    {
        var members = _adminService.ViewMembers();
        return View(members); // Pass members to the view
    }

    [HttpPost]
    public IActionResult MatchCoach(int scheduleId, int coachId)
    {
        _adminService.MatchCoachToSchedule(scheduleId, coachId);
        return RedirectToAction("ViewSchedules");
    }

    [HttpGet]
    public IActionResult ViewCoaches()
    {
        var coaches = _adminService.GetCoaches();
        return View(coaches);
    }

    [HttpGet]
    public IActionResult CoachProfile(int coachId)
    {
        var coach = _adminService.GetCoachById(coachId);
        if (coach == null)
        {
            return NotFound(); // Handle the case where the coach is not found
        }
        return View(coach);
    }

    [HttpGet]
    public IActionResult ViewSchedulesByCoach(int coachId)
    {
        var schedules = _adminService.GetSchedulesByCoach(coachId);
        var coach = _adminService.GetCoachById(coachId);
        ViewBag.CoachName = coach != null ? $"{coach.FirstName} {coach.LastName}" : "Unknown Coach";

        if (schedules == null || !schedules.Any())
        {
            return View("NoSchedulesForCoach");
        }
        return View(schedules);
    }


    [HttpGet]
    public IActionResult EditSchedule(int id)
    {
        var schedule = _adminService.GetScheduleById(id);
        if (schedule == null)
        {
            return NotFound();
        }

        // Get the list of available coaches and create a SelectList with full names
        var coaches = _adminService.GetCoaches()
            .Select(c => new SelectListItem
            {
                Value = c.CoachID.ToString(),
                Text = $"{c.FirstName} {c.LastName}"
            }).ToList();

        ViewBag.Coaches = coaches;
        return View(schedule);
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditSchedule(int id, Schedule updatedSchedule)
    {
        if (ModelState.IsValid)
        {
            var result = _adminService.UpdateSchedule(id, updatedSchedule);
            if (result)
            {
                return RedirectToAction("ViewSchedules");
            }
            ModelState.AddModelError(string.Empty, "An error occurred while updating the schedule.");
        }

        // If model state is invalid, repopulate the coaches list for the dropdown
        var coaches = _adminService.GetCoaches()
            .Select(c => new SelectListItem
            {
                Value = c.CoachID.ToString(),
                Text = $"{c.FirstName} {c.LastName}"
            }).ToList();

        ViewBag.Coaches = coaches;

        return View(updatedSchedule);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteSchedule(int id)
    {
        var result = _adminService.DeleteSchedule(id);
        if (result)
        {
            return RedirectToAction("ViewSchedules"); // Redirect to the list of schedules after successful deletion
        }
        return NotFound(); // Show a 404 error if the schedule could not be found or deleted
    }


}
