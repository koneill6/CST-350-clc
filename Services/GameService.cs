using Milestone_cst_350.Models;

namespace Milestone_cst_350.Services
{
    /// <summary>
    /// This service allows the controller to interact with a Minesweeper game.
    /// </summary>
    public class GameService
    {
        // TODO: GameSessionService
        private GameSessionService _gameSessionService = GameSessionService.Instance;

        public GameService()
        {
            // ...
        }

        /// <summary>
        /// Create a board game.
        /// </summary>
        /// <param name="boardSize">the length/width of the board</param>
        /// <param name="bombPercentage">the percentage of cells containing bombs</param>
        /// <returns>the new board</returns>
        public BoardModel CreateGame(Guid sessionId, int boardSize, float bombPercentage)
        {
            BoardModel board = new BoardModel(boardSize, bombPercentage);

            return _gameSessionService.PutSession(sessionId, board) ?
                board
                :
                throw new Exception($"Failed to store session: {sessionId}");
        }

        public BoardModel? GetGameBySessionId(Guid sessionId)
        {
            BoardModel board;
            return _gameSessionService.GetSession(sessionId, out board) ?
                board
                :
                null;
        }

        /// <summary>
        /// Revel a cell on the game board.
        /// </summary>
        /// <param name="boardModel">the board model</param>
        /// <param name="row">the cell's row position</param>
        /// <param name="col">the cell's column position</param>
        /// <returns>true if the cell is a bomb, otherwise returns false</returns>
        public bool RevelCell(BoardModel boardModel, int row, int col)
        {
            return boardModel.RevealCell(row, col);
        }

        /// <summary>
        /// Toggle the flag status of a cell.
        /// </summary>
        /// <param name="boardModel">the board model</param>
        /// <param name="row">the cell's row position</param>
        /// <param name="col">the cell's column position</param>
        /// <returns>true if the cell is now flagged, otherwise returns false</returns>
        public bool FlagCell(BoardModel boardModel, int row, int col)
        {
            return boardModel.FlagCell(row, col);
        }

        /// <summary>
        /// Check if the user has won the game.
        /// </summary>
        /// <param name="boardModel">the board model</param>
        /// <returns>true if the user has won the game, otherwise return false</returns>
        public bool HasWon(BoardModel boardModel)
        {
            return boardModel.IsComplete();
        }
    }
}
