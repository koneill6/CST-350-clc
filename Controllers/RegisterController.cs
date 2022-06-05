using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class RegisterController : Controller
    {
        private AccountService _accountService = new AccountService();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterUser(UserModel user)
        {
            // TODO: Adjust routing in later milestones?
            return _accountService.RegisterUser(user) ?
                View("RegisterSuccess")
                :
                View("RegisterFailure");
        }
    }
}
