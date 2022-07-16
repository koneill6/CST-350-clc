using Milestone_cst_350.Models;

namespace Milestone_cst_350.Services
{
    /// <summary>
    /// This service allows the controller to interact with a Minesweeper game.
    /// </summary>
    public class GameService
    {
        // Game Session Service
        private GameSessionService _gameSessionService = GameSessionService.Instance;

        // Default Constructor 
        public GameService()
        {
            // ...
        }

        // TODO: Docs
        // A method that creates a new board and gives it a session ID
        public BoardModel CreateGame(Guid sessionId, int boardSize, float bombPercentage)
        {
            BoardModel board = new BoardModel(boardSize, bombPercentage);

            return _gameSessionService.PutSession(sessionId, board) ?
                board
                :
                throw new Exception($"Failed to store session: {sessionId}");
        }

        // A method adds a save state to a new game
        public Guid CreateGameWithSaveState(string saveState)
        {
            BoardModel board = new BoardModel(saveState);
            Guid guid = Guid.NewGuid();

            return _gameSessionService.PutSession(guid, board) ?
                guid
                :
                throw new Exception($"Failed to store session: {guid}");
        }

        /// <summary>
        /// Retrieves a game by it's session ID 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>game board on true, null on false</returns>
        public BoardModel? GetGameBySessionId(Guid sessionId)
        {
            BoardModel board;
            return _gameSessionService.GetSession(sessionId, out board) ?
                board
                :
                null;
        }

        /// <summary>
        /// Deletes the game by it's session ID
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>true if the game was deleted, false if it wasn't</returns>
        public bool DeleteGameBySessionId(Guid sessionId)
        {
            return _gameSessionService.RemoveSession(sessionId);
        }

        /// <summary>
        /// Will revel the cells on a board
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>will reveal all the cells if true, if false nothing will happen</returns>
        public bool RevelCell(Guid sessionId, int row, int col)
        {
            BoardModel? board = GetGameBySessionId(sessionId);
            return board != null && board.RevealCell(row, col);
        }

        /// <summary>
        /// Will flag cell in the game 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>cell will be flagged on the board if ture, else nothing will happen</returns>
        public bool FlagCell(Guid sessionId, int row, int col)
        {
            BoardModel? board = GetGameBySessionId(sessionId);
            return board != null && board.FlagCell(row, col);
        }

        /// <summary>
        /// Will determine if the user wins a game
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>a message will display if true, else nothing will happen</returns>
        public bool HasWon(Guid sessionId)
        {
            BoardModel? board = GetGameBySessionId(sessionId);
            return board != null && board.IsComplete();
        }

        /// <summary>
        /// Will determine if the user has lost in the game
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>a message will display if true, else nothing will happen</returns>
        public bool HasLost(Guid sessionId)
        {
            BoardModel? board = GetGameBySessionId(sessionId);
            return board != null && board.HasLost;
        }
    }
}
