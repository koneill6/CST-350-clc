using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class GameController : Controller
    {
        private GameService _gameService = new GameService();

        private int _size = 10;
        private float _diff = .15f;

        public IActionResult Index(UserModel user)
        {
            Guid sessionId = Guid.NewGuid();
            _ = _gameService.CreateGame(sessionId, _size, _diff);

            // TODO: Convert to payload model(s) to simplify.
            ViewBag.user = user;
            ViewBag.sessionId = sessionId;

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
            // TODO: Determine if we want to alert on bad click/session?
            _ = _gameService.RevelCell(sessionId, row, col);
            return GetBoard(sessionId);
        }

        public IActionResult FlagBoard(Guid sessionId, int row, int col)
        {
            _ = _gameService.FlagCell(sessionId, row, col);
            return GetBoard(sessionId);
        }

        public IActionResult ResetBoard(Guid sessionId)
        {
            // TODO: It would be better to create an entirely new session.
            // This likely requires a different way to store the User across the browser session.
            // Cookies?

            _ = _gameService.DeleteGameBySessionId(sessionId);
            _ = _gameService.CreateGame(sessionId, _size, _diff);

            return GetBoard(sessionId);
        }
    }
}