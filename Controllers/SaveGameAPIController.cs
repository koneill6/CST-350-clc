using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    [ApiController]
    [Route("api")]
    public class SaveGameAPIController : ControllerBase
    {
        private SaveGameDAO _dao;
        private GameService _gameService = new GameService();

        public SaveGameAPIController()
        {
            _dao = new SaveGameDAO();
        }

        [HttpGet("savedGames")]
        public IEnumerable<SaveGameModel> GetAllSavedGames()
        {
            return _dao.AllGames();
        }

        [HttpGet("savedGames/{id}")]
        public ActionResult<SaveGameModel> GetSavedGameById(int id)
        {
            return _dao.GetGameById(id);
        }

        [HttpDelete("savedGames/{id}")]
        public StatusCodeResult DeleteGameById(int id)
        {
            // If we deleted something, 204 - no content, else, 500 - server error.
            return _dao.DeleteGameById(id) > 0 ?
                StatusCode(204)
                :
                StatusCode(500);
        }

        [HttpGet("savedGames/{id}/load")]
        public StatusCodeResult LoadSavedGameById(int id)
        {
            // Get existing game.
            SaveGameModel model = _dao.GetGameById(id);
            Guid sessionId = _gameService.CreateGameWithSaveState(model.save_state);

            // Store new session id.
            HttpContext.Session.SetString("guid", sessionId.ToString());

            return StatusCode(204);
        }

        [HttpGet("savedGames/user/{userId}")]
        public IEnumerable<SaveGameModel> GetSavedGameByUserId(int userId)
        {
            return _dao.GetAllGamesByUserId(userId);
        }

        [HttpPost("savedGames")]
        public StatusCodeResult PostSaveGame([FromForm] string payload, [FromForm] string userId)
        {
            // If we created something, 204 - no content, else, 500 - server error.
            SaveGameModel model = new SaveGameModel(-1, int.Parse(userId), DateTime.Now, payload);
            return _dao.SaveGame(model) > 0
                ? StatusCode(204)
                : StatusCode(500);
        }
    }
}
