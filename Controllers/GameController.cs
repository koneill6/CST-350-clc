using System.Net;
using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    [UserAuthorization]
    public class GameController : Controller
    {
        private GameService _gameService = new GameService();

        private int _size = 10;
        private float _diff = .15f;

        public IActionResult Index()
        {
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
            ViewBag.username = HttpContext.Session.GetString("user")!;


            return View();
        }

        public IActionResult GetBoard(Guid sessionId)
        {
            ViewBag.HasWon = _gameService.HasWon(sessionId);
            ViewBag.HasLost = _gameService.HasLost(sessionId);

            return PartialView("_Board", _gameService.GetGameBySessionId(sessionId));
        }

        public IActionResult RevealBoard(Guid sessionId, int row, int col)
        {
            _ = _gameService.RevelCell(sessionId, row, col);
            return GetBoard(sessionId);
        }

        public IActionResult FlagBoard(Guid sessionId, int row, int col)
        {
            _ = _gameService.FlagCell(sessionId, row, col);
            return GetBoard(sessionId);
        }

        public HttpStatusCode ResetBoard(Guid sessionId)
        {
            HttpContext.Session.Remove("guid");

            return _gameService.DeleteGameBySessionId(sessionId) ? 
                HttpStatusCode.NoContent 
                : 
                HttpStatusCode.InternalServerError;
        }

        public IActionResult ViewGames()
        {
            return View();
        }
    }
}