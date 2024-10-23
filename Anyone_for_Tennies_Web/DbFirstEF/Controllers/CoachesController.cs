using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbFirstEF.Data;
using DbFirstEF.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic; // Include this for List<T>

namespace DbFirstEF.Controllers
{
    public class CoachesController : Controller
    {
        private readonly Hitdb1Context _hitdbContext;
        private readonly NewLocalDbContext _localContext;

        public CoachesController(Hitdb1Context hitdbContext, NewLocalDbContext localContext)
        {
            _hitdbContext = hitdbContext;
            _localContext = localContext;
        }

        // GET: Coaches/Index
        public async Task<IActionResult> Index()
        {
            var coaches = await _hitdbContext.Coaches.ToListAsync(); // Fetch list of coaches from database
            return View(coaches); // Pass list to view
        }

        //GET:Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _hitdbContext.Coaches.FirstOrDefaultAsync(c => c.CoachId == id);

            if (coach == null)
            {
                return NotFound();
            }

            var localProfile = await _localContext.CoachProfiles.FirstOrDefaultAsync(lp => lp.CoachId == coach.CoachId);
            if (localProfile != null)
            {
                coach.Biography = localProfile.Biography;
            }

            return View(coach);
        }

        //GET:Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localProfile = await _localContext.CoachProfiles.FirstOrDefaultAsync(lp => lp.CoachId == id);
            if (localProfile == null)
            {
                return NotFound();
            }

            return View(localProfile);
        }

        //POST:Coaches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoachId,Biography")] CoachProfile localProfile)
        {
            if (id != localProfile.CoachId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _localContext.Update(localProfile);
                    await _localContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_localContext.CoachProfiles.Any(e => e.CoachId == localProfile.CoachId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = localProfile.CoachId });
            }
            return View(localProfile);
        }

        //GET:Coaches/Schedules
        public IActionResult Schedules(int? id)
        {
            // If ID is provided, redirect to corresponding schedules page.
            if (id.HasValue)
            {
                return RedirectToAction("Schedules", "Home", new { id = id.Value });
            }
            // If no ID, you could return a default view or handle accordingly.
            return RedirectToAction("Schedules", "Home");
        }

        //GET:Coaches/ViewMembers/{id}
        public IActionResult ViewMembers(int id)
        {
            var members = new List<MemberViewModel>
            {
                new MemberViewModel { FullName = "John Doe", Email = "john.doe@example.com" },
                new MemberViewModel { FullName = "Jane Smith", Email = "jane.smith@example.com" }
                // Populate with real data from database
            };

            return View(members);
        }
    }
    // Create simple ViewModel for member data
    public class MemberViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
