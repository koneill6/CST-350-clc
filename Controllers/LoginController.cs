using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class LoginController : Controller
    {
        // Account service 
        private AccountService _accountService = new AccountService();

        // Default view
        public IActionResult Index()
        {
            return View();
        }

        // Displays the users login, and handles the authenticy of an account
        public IActionResult ShowLogin(UserModel user)
        {

            if (!_accountService.AuthenticateUser(user)) return View("LoginFailure");

            HttpContext.Session.SetString("user", user.Username);
            return RedirectToAction("Index", "UserLanding", new { username = user.Username });
        }
    }
}
