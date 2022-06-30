namespace Milestone_cst_350.Models;

public class SaveGamePayload
{
    public int UserId { get; set; }
    public BoardModel Board { get; set; }

    public SaveGamePayload()
    {
        // ...
    }

    public SaveGamePayload(int userId, BoardModel board)
    {
        UserId = userId;
        Board = board;
    }
}