using Microsoft.AspNetCore.Mvc;
using assignment3.ViewModel;
using assignment3.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace assignment3.Controllers
{

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    // Login action
    [HttpGet]
    [AllowAnonymous] // Allow unauthenticated users to access this action
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); // Redirect to a default page after login
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View(model);
    }

    // Logout action
    [HttpPost]
    [ValidateAntiForgeryToken] // Helps prevent CSRF attacks
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home"); // Redirect after logout
    }

    // Other actions in HomeController...
}
}