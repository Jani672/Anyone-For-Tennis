using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Anyone_for_Tennies.Pages.Members
{
    public class LoginModel : PageModel
    {
        public IActionResult OnPostLogin()
        {
            // Access session data
            var userId = HttpContext.Session.GetString("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Members/Login");
            }

            // Logic based on the role
            return userRole == "Admin" ? RedirectToPage("/Admin/Dashboard") : RedirectToPage("/Members/Dashboard");
        }
    }

}
