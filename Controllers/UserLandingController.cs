using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class UserLandingController : Controller
    {
        // Account Service
        AccountService _accountService = new AccountService();

        // Default view
        public IActionResult Index(string username)
        {
            // Will return the user to the landing page if there login is genuine
            UserModel? user = _accountService.GetUserByUsername(username);
            if (user == null) return RedirectToAction("Index", "Login");

            HttpContext.Session.SetString("user", user.Username);
            HttpContext.Session.SetString("id", user.Id.ToString());
            return View(user);
        }
    }
}
