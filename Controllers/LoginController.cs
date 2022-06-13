using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class LoginController : Controller
    {
        private AccountService _accountService = new AccountService();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowLogin(UserModel user)
        {
            return _accountService.AuthenticateUser(user) ?
              RedirectToAction("Index", "UserLanding", new { username = user.Username })
              :
              View("LoginFailure");
        }
    }
}
