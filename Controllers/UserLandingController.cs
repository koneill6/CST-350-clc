using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class UserLandingController : Controller
    {
        AccountService _accountService = new AccountService();

        public IActionResult Index(string username)
        {
            UserModel? user = _accountService.GetUserByUsername(username);
            if (user == null) return RedirectToAction("Index", "Login");

            HttpContext.Session.SetString("user", user.Username);
            return View(user);
        }
    }
}
