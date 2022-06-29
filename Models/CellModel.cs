using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone_cst_350.Models
{
    /// <summary>
    /// Class for storing all the cell data.
    /// </summary>
    public class CellModel
    {
        /// <summary>
        /// The column position of the cell.
        /// </summary>
        public int Col { get; set; }

        /// <summary>
        /// The row position of the cell.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// The number of neighboring cells with bombs.
        /// </summary>
        public int LiveNeighbors { get; set; }

        /// <summary>
        /// Check if the cell has already been visited / left clicked on.
        /// </summary>
        public bool IsVisited { get; set; }

        /// <summary>
        /// Check if the cell has already been flagged / right clicked on.
        /// </summary>
        public bool IsFlagged { get; set; }

        /// <summary>
        /// Check if the cell has a bomb.
        /// </summary>
        public bool IsLive { get; set; }

        /// <summary>
        /// Default constructor for a cell.
        /// </summary>
        public CellModel()
        {
            this.Col = -1;
            this.Row = -1;
            this.LiveNeighbors = -1;
            this.IsVisited = false;
            this.IsFlagged = false;
            this.IsLive = false;
        }

        /// <summary>
        /// Get the cell's identifier which corresponds to its state.
        /// </summary>
        /// <returns>a string representing the cell's state</returns>
        public string GetStatus()
        {
            return (IsFlagged ? "&" : "") + (IsVisited ? "+" : IsLive ? "*" : "-");
        }
    }
}
