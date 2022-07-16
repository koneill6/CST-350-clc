using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class RegisterController : Controller
    {
        // Account Service
        private AccountService _accountService = new AccountService();

        public IActionResult Index()
        {
            return View();
        }

        // A method call to register a user
        public IActionResult RegisterUser(UserModel user)
        {
            return _accountService.RegisterUser(user) ?
                View("RegisterSuccess")
                :
                View("RegisterFailure");
        }
    }
}
