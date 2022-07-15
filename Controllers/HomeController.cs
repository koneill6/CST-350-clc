using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using System.Diagnostics;

namespace Milestone_cst_350.Controllers
{
    public class HomeController : Controller
    {
        // Logger variable and method for home controller
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //redirect landing page to the login page
            //Response.Redirect("login");
            return View();
        }

        // A method that returns the test view. The test view was created for test purposes only 
        public IActionResult Test()
        {
            return View("test");
        }

        // A method that returns the privacy view
        public IActionResult Privacy()
        {
            return View();
        }

        // A method that returns the error view
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}