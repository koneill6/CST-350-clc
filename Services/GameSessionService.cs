using Milestone_cst_350.Models;

namespace Milestone_cst_350.Services
{
    public sealed class GameSessionService
    {
        private static readonly Lazy<GameSessionService> _instance = new Lazy<GameSessionService>(() => new GameSessionService());
        public static GameSessionService Instance => _instance.Value;

        private static Dictionary<Guid, BoardModel> _sessions;

        private GameSessionService()
        {
            _sessions = new Dictionary<Guid, BoardModel>();
        }

        /// <summary>
        /// Try and put a session in the sessions dictionary.
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="board">board</param>
        /// <returns>true on success, false on failure</returns>
        public bool PutSession(Guid sessionId, BoardModel board)
        {
            Console.WriteLine($"GameSessionService: Putting Session - {sessionId}");
            return _sessions.TryAdd(sessionId, board);
        }

        /// <summary>
        /// Try and get a session in the sessions dictionary.
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="board">board</param>
        /// <returns>true on success, false on failure</returns>
        public bool GetSession(Guid sessionId, out BoardModel board)
        {
            Console.WriteLine($"GameSessionService: Getting Session - {sessionId}");
            return _sessions.TryGetValue(sessionId, out board);
        }
    }
}
