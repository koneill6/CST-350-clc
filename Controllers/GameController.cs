using System.Net;
using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class GameController : Controller
    {
        // Game service and controller class properties
        private GameService _gameService = new GameService();

        private int _size = 10;
        private float _diff = .15f;

        // Intial route 
        public IActionResult Index()
        {
            // Reject no-user.
            string? username = HttpContext.Session.GetString("user");
            if (username == null) RedirectToAction("Index", "Login");

            // Session.
            Guid sessionId;

            string? session = HttpContext.Session.GetString("guid");
            if (session == null)
            {
                sessionId = Guid.NewGuid();
                session = sessionId.ToString();

                _ = _gameService.CreateGame(sessionId, _size, _diff);
                HttpContext.Session.SetString("guid", session);
            }
            else
            {
                sessionId = Guid.Parse(session);
            }

            ViewBag.sessionId = sessionId;
            ViewBag.userId = int.Parse(HttpContext.Session.GetString("id")!);
            ViewBag.username = username!;


            return View();
        }

        // Method call to retrieve the board using a session ID
        public IActionResult GetBoard(Guid sessionId)
        {
            ViewBag.HasWon = _gameService.HasWon(sessionId);
            ViewBag.HasLost = _gameService.HasLost(sessionId);

            return PartialView("_Board", _gameService.GetGameBySessionId(sessionId));
        }

        // Method call to reveal the all the cells on a board
        public IActionResult RevealBoard(Guid sessionId, int row, int col)
        {
            _ = _gameService.RevelCell(sessionId, row, col);
            return GetBoard(sessionId);
        }

        // Method call to flag a board
        public IActionResult FlagBoard(Guid sessionId, int row, int col)
        {
            _ = _gameService.FlagCell(sessionId, row, col);
            return GetBoard(sessionId);
        }

        // Session call to reset the user's board
        public HttpStatusCode ResetBoard(Guid sessionId)
        {
            HttpContext.Session.Remove("guid");

            return _gameService.DeleteGameBySessionId(sessionId) ? 
                HttpStatusCode.NoContent 
                : 
                HttpStatusCode.InternalServerError;
        }

        // Method call to get the view containing all the user's games
        public IActionResult ViewGames()
        {
            return View();
        }
    }
}