using Milestone_cst_350.Models;

namespace Milestone_cst_350.Services
{
    /// <summary>
    /// This service allows the controller to interact with a Minesweeper game.
    /// </summary>
    public class GameService
    {
        private GameSessionService _gameSessionService = GameSessionService.Instance;

        public GameService()
        {
            // ...
        }

        // TODO: Docs

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

        public bool DeleteGameBySessionId(Guid sessionId)
        {
            return _gameSessionService.RemoveSession(sessionId);
        }


        public bool RevelCell(Guid sessionId, int row, int col)
        {
            BoardModel? board = GetGameBySessionId(sessionId);
            return board != null && board.RevealCell(row, col);
        }

        public bool FlagCell(Guid sessionId, int row, int col)
        {
            BoardModel? board = GetGameBySessionId(sessionId);
            return board != null && board.FlagCell(row, col);
        }

        public bool HasWon(Guid sessionId)
        {
            BoardModel? board = GetGameBySessionId(sessionId);
            return board != null && board.IsComplete();
        }

        public bool HasLost(Guid sessionId)
        {
            BoardModel? board = GetGameBySessionId(sessionId);
            return board != null && board.HasLost;
        }
    }
}
