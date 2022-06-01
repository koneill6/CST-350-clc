using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;

namespace Milestone_cst_350.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowLogin(LoginModel login)
        {
            if(login.Username == "tester" && login.Password == "test1")
            {
                return View("loginValid", login);
            }
            else
            {
                return View("loginInvalid", login);
            }
            
        }
    }
}
