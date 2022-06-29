using Microsoft.AspNetCore.Mvc;
using Milestone_cst_350.Models;
using Milestone_cst_350.Services;

namespace Milestone_cst_350.Controllers
{
    [ApiController]
    [Route("api")]
    public class SaveGameAPIController : Controller
    {
        SaveGameDAO repo = new SaveGameDAO();

        [HttpGet("showSavedGames")]
        public IEnumerable<SaveGameModel> Index()
        {
            List<SaveGameModel> saveGameList = repo.AllGames();
            return saveGameList;
        }

        [HttpGet("showSavedGames/{Id}")]
        public ActionResult<SaveGameModel> ShowOneProduct(int Id)
        {
            SaveGameModel product = repo.GetGameById(Id);
            return product;
        }

        [HttpGet("deleteOneGame/{Id}")]
        public ActionResult<SaveGameModel> DeleteOneProduct(int Id)
        {
            //returning null since nothing is being returned
            repo.DeleteGameById(Id);
            return null;
        }
    }
}
