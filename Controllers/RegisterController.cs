using Microsoft.AspNetCore.Mvc;

namespace Milestone_cst_350.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            // TODO: Attempt to register, route according to success/failure.
            return View();
        }
    }
}
