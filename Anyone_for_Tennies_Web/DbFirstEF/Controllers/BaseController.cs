using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DbFirstEF.Controllers
{
    public class BaseController : Controller
    {
        // Method to obtain logged-in coach's ID
        protected int GetLoggedInCoachId()
        {
            // return ID from session or authentication
            return HttpContext.Session.GetInt32("CoachId") ?? 0;  // using session
        }

        // pass CoachId to ViewBag in every controller action
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // Set CoachId in ViewBag to be used in layout
            ViewBag.CoachId = GetLoggedInCoachId();
        }
    }
}
