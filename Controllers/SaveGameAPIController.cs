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

        [HttpGet("savedGames/user/{userId}")]
        public IEnumerable<SaveGameModel> GetSavedGameByUserId(int userId)
        {
            return _dao.GetAllGamesByUserId(userId);
        }

        // TODO: Determine if this payload is even valid.
        // The alternative / simpler solution might be to instead set a global variable in the js (eg. $ms.serialized)
        // and then pass in the the serialized string to with the UserId instead. eg. ( { userId, board } ) where board is serialized.
        [HttpPost("savedGames")]
        public StatusCodeResult PostSaveGame(SaveGamePayload payload)
        {
            // If we created something, 204 - no content, else, 500 - server error.
            SaveGameModel model = new SaveGameModel(-1, payload.UserId, DateTime.Now, payload.Board.Serialize());
            return _dao.SaveGame(model) > 0
                ? StatusCode(204)
                : StatusCode(500);
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
    }
}
