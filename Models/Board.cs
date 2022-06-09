using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone_cst_350.Models
{
    /// <summary>
    /// Class for handling all the game board logic.
    /// </summary>
    class Board
    {
        /// <summary>
        /// The 2 dimensional grid of cell.
        /// </summary>
        public Cell[,] Grid { get; set; }

        /// <summary>
        /// The size or length/width of the game board.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The bomb percentage ranging from 0-1 where 0 is 0% bombs and 1 is 100% bombs.
        /// </summary>
        public float BombPercentage { get; set; }

        /// <summary>
        /// Get the number of bombs after the grid has been populated.
        /// </summary>
        public int NumBombs { get; set; }

        /// <summary>
        /// The number of flagged cells.
        /// </summary>
        public int NumFlaggedCells { get; set; }

        /// <summary>
        /// Construct a game board with size and difficulty.
        /// </summary>
        /// <param name="size">the length/width of the board</param>
        /// <param name="bombPercentage">the percentage of bombs</param>
        public Board(int size, float bombPercentage)
        {
            // Initialize Fields
            Size = size;
            BombPercentage = bombPercentage;
            NumBombs = 0;
            NumFlaggedCells = 0;

            // Initialize Grid
            Grid = new Cell[Size, Size];
            InitializeCells();
        }

        /// <summary>
        /// Initilize the grid with cells.
        /// </summary>
        private void InitializeCells()
        {
            // Random for calculating bomb placement
            Random random = new Random();

            // Loop through entire grid
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // Create new cell
                    Cell cell = new Cell();
                    cell.Row = i;
                    cell.Col = j;
                    cell.IsLive = random.NextDouble() <= BombPercentage;
                    Grid[i, j] = cell;

                    // Incremenet total bomb count
                    if (cell.IsLive) NumBombs++;
                }
            }

            // Calculate Live Neighbors
            CalcLiveNeighbors();
        }

        /// <summary>
        /// Calculate each cell's number of live neighbors.
        /// </summary>
        private void CalcLiveNeighbors()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j].LiveNeighbors = GetLiveNeighbors(i, j);
                }
            }
        }

       
        /// <summary>
        /// Get the number of live neighbors for a cell.
        /// </summary>
        /// <param name="row">the cell's row position</param>
        /// <param name="col">the cell's column position</param>
        /// <returns>the cell's number of live neighbors</returns>
        private int GetLiveNeighbors(int row, int col)
        {
            int sum = 0;

            // Top row
            sum += IsLive(row - 1, col - 1);
            sum += IsLive(row, col - 1);
            sum += IsLive(row + 1, col - 1);

            // Mid row
            sum += IsLive(row - 1, col);
            sum += IsLive(row + 1, col);

            // Low row
            sum += IsLive(row - 1, col + 1);
            sum += IsLive(row, col + 1);
            sum += IsLive(row + 1, col + 1);

            return sum;
        }

        /// <summary>
        /// Get the cell at the provided position.
        /// </summary>
        /// <param name="row">the cell's row position</param>
        /// <param name="col">the cell's column position</param>
        /// <returns>the cell or null if out of bounds</returns>
        public Cell GetCell(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size) return null;
            return Grid[row, col];
        }

        /// <summary>
        /// Check if a cell is live.
        /// </summary>
        /// <param name="row">the cell's row position</param>
        /// <param name="col">the cell's column position</param>
        /// <returns>1 if the cell is live, otherwise returns 0</returns>
        private int IsLive(int row, int col)
        {
            Cell cell = GetCell(row, col);
            return cell == null || !cell.IsLive ? 0 : 1;
        }

        /// <summary>
        /// Reveal a cell at the provided position. This method will start
        /// flood filling if the cell has no live neighbors.
        /// </summary>
        /// <param name="row">the cell's row position</param>
        /// <param name="col">the cell's column position</param>
        /// <returns>true if the cell is not a bomb, otherwise returns false</returns>
        public bool RevealCell(int row, int col)
        {
            // Get possible cell
            Cell cell = GetCell(row, col);

            // No cell found at location
            if (cell == null) return true;

            // Cell is a bomb, return true to signify
            if (cell.IsLive) return false;

            // Set cell as visited
            cell.IsVisited = true;


            // Flood fill this cell if no neighbors
            if (cell.LiveNeighbors == 0) FloodFill(row, col);

            return true;
        }

        /// <summary>
        /// Start flood filling at the current position.
        /// </summary>
        /// <param name="row">the row</param>
        /// <param name="col">the column</param>
        private void FloodFill(int row, int col)
        {
            // North
            if (FloodReveal(row - 1, col)) FloodFill(row - 1, col);

            // East
            if (FloodReveal(row, col + 1)) FloodFill(row, col + 1);

            // South
            if (FloodReveal(row + 1, col)) FloodFill(row + 1, col);

            // West
            if (FloodReveal(row, col - 1)) FloodFill(row, col - 1);

            // North East
            if (FloodReveal(row - 1, col + 1)) FloodFill(row - 1, col + 1);

            // North West
            if (FloodReveal(row - 1, col - 1)) FloodFill(row - 1, col - 1);

            // South East
            if (FloodReveal(row + 1, col + 1)) FloodFill(row + 1, col + 1);

            // South West
            if (FloodReveal(row + 1, col - 1)) FloodFill(row + 1, col - 1);
        }

        /// <summary>
        /// Reveal cells during a flood fill recursive search.
        /// </summary>
        /// <param name="row">the cell's row position</param>
        /// <param name="col">the cell's column position</param>
        /// <returns>true if the cell has no live neighbors, otherwise return false</returns>
        private bool FloodReveal(int row, int col)
        {
            Cell cell = GetCell(row, col);
            if (cell != null && !cell.IsLive && !cell.IsVisited)
            {
                cell.IsVisited = true;
                return cell.LiveNeighbors == 0;
            }
            return false;
        }

        /// <summary>
        /// Check if the game is complete / has been solved.
        /// </summary>
        /// <returns>true if the game is complete, otherwise return false</returns>
        public bool IsComplete()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cell cell = Grid[i, j];

                    if (cell.IsLive)
                    {
                        // Live bombs need to be flagged
                        if (!cell.IsFlagged) return false;
                    }
                    else
                    {
                        // Non-live cells must be visited
                        if (!cell.IsVisited) return false;
                    }
                }
            }

            return true;
        }
    }
}
