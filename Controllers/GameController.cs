using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    public class GameController : Controller
    {
        private GameService _gameService = new GameService();

        [HttpGet]
        public IActionResult Index(UserModel user)
        {
            Guid sessionId = Guid.NewGuid();
            _ = _gameService.CreateGame(sessionId, 10, 15);

            // TODO: Convert to payload model(s) to simplify.
            ViewBag.user = user;
            ViewBag.sessionId = sessionId;

            return View();
        }

        [HttpGet]
        public IActionResult GetBoard(Guid sessionId)
        {
            return PartialView("_Board", _gameService.GetGameBySessionId(sessionId));
        }

        [HttpPost]
        public IActionResult PostClickBoard()
        {
            // TODO: BoardService reveal based on session.
            return View();
        }

        [HttpPost]
        public IActionResult PostFlagBoard()
        {
            // TODO: BoardService flag based on session.
            return View();
        }

        [HttpDelete]
        public IActionResult DeleteBoard()
        {
            // TODO: BoardService reset.
            return View();
        }
    }
}