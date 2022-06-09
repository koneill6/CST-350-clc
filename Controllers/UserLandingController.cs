using Microsoft.AspNetCore.Mvc;

namespace Milestone_cst_350.Controllers
{
    public class UserLandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
