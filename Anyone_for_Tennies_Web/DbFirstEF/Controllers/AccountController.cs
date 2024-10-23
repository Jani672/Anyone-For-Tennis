using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbFirstEF.Data;
using DbFirstEF.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DbFirstEF.Controllers
{
    public class AccountController : Controller
    {
        private readonly NewLocalDbContext1 _localContext1;
        private readonly Hitdb1Context _hitdbContext; // Correct context for Coach login

        public AccountController(NewLocalDbContext1 localContext1, Hitdb1Context hitdbContext)
        {
            _localContext1 = localContext1;
            _hitdbContext = hitdbContext; // Use correct context name
        }

        // Login1 Action (GET)
        public IActionResult Login1()
        {
            return View(); // Returns Login1.cshtml for user login
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login1(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _localContext1.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    return RedirectToAction("CoachLogin", "Account");
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View();
        }

        // CoachLogin Action(Obtain)- Displays all coaches
        public async Task<IActionResult> CoachLogin()
        {
            var coaches = await _hitdbContext.Coaches.ToListAsync();
            ViewData["CoachesList"] = coaches; // Pass the list of coaches to the view
            return View(); // Returns CoachLogin.cshtml for coach login
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CoachLogin(string firstName, string lastName)
        {
            if (ModelState.IsValid)
            {
                var coach = await _hitdbContext.Coaches
                    .FirstOrDefaultAsync(c => c.FirstName == firstName && c.LastName == lastName);

                if (coach != null)
                {
                    HttpContext.Session.SetInt32("CoachId", coach.CoachId);
                    return RedirectToAction("Details", "Coaches", new { id = coach.CoachId });
                }

                ModelState.AddModelError("", "Coach not found.");
            }

            // Repopulate coach list if login fails
            var coaches = await _hitdbContext.Coaches.ToListAsync();
            ViewData["CoachesList"] = coaches;
            return View();
        }

        //Register Action(Take)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Register Action(POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != ConfirmPassword)
                {
                    ViewData["PasswordMismatch"] = "Passwords do not match.";
                    return View(model);
                }

                var existingUser = await _localContext1.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "An account with this email already exists.");
                    return View(model);
                }

                _localContext1.Users.Add(model);
                await _localContext1.SaveChangesAsync();

                return RedirectToAction("Login1");
            }

            return View(model);
        }

        //Display Account Details Action(GET)
        public async Task<IActionResult> Display()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login1");
            }

            var user = await _localContext1.Users.FirstOrDefaultAsync(u => u.UserId == userId.Value);

            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Returns Display.cshtml for account details
        }

        // GET:Account/Edit
        public async Task<IActionResult> Edit()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login1");
            }

            var user = await _localContext1.Users.FirstOrDefaultAsync(u => u.UserId == userId.Value);

            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Return user data to the Edit view
        }

        // POST:Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var userInDb = await _localContext1.Users.FirstOrDefaultAsync(u => u.UserId == model.UserId);
                if (userInDb == null)
                {
                    return NotFound();
                }

                //Update user properties
                userInDb.FirstName = model.FirstName;
                userInDb.LastName = model.LastName;
                userInDb.Address = model.Address;
                userInDb.PhoneNumber = model.PhoneNumber;
                userInDb.DateOfBirth = model.DateOfBirth;

                //Save changes
                await _localContext1.SaveChangesAsync();

                return RedirectToAction("Display"); // Redirect to the Display page after update
            }

            return View(model); // Return the model to the view if validation fails
        }

        //Logout action
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login1");
        }
    }
}
