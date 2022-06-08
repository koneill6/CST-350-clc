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

        public IActionResult ShowLogin(LoginModel login)
        {
            //temp way to check authentication and redirect to user landing page
            if (_accountService.AuthenticateUser(login))
            {
                //needs the .. to go back to local host/default controller
                //
                Response.Redirect("../userlanding");
            }
            // TODO: Determine if UserModel is just better to use here.
            return View("LoginFailure");

        }
    }
}
