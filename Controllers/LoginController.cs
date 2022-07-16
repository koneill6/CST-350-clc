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
        private readonly ILogger<LoginController> logger;

        public LoginController(ILogger<LoginController> logger)
        {
            this.logger = logger;
            this.logger.LogDebug("NLog injected into login controller");
        }
        
        public IActionResult Index()
        {
            // Log the user is trying to login
            this.logger.LogInformation("User is trying to login!");
            return View();
        }

        // Displays the users login, and handles the authenticy of an account
        public IActionResult ShowLogin(UserModel user)
        {

            if (!_accountService.AuthenticateUser(user))
            {
                this.logger.LogWarning("The user failed to login for " + user.Username);
                return View("LoginFailure");
            }

            HttpContext.Session.SetString("user", user.Username);
            this.logger.LogInformation("Set cookie value for: " + user.Username);
            this.logger.LogInformation("Successfully logged in as: " + user.Username);

            return RedirectToAction("Index", "UserLanding", new { username = user.Username });
        }
    }
}
