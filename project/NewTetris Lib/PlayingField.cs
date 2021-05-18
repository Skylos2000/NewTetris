using System;
using System.Linq;

namespace NewTetris_Lib {
  /// <summary>
  /// Encodes information about the playing
  /// field of the game. Uses a grid of rows
  /// and cols storing 1 for occupied and 0
  /// for vacant
  /// </summary>
  public class PlayingField {
    /// <summary>
    /// Singleton pattern instance
    /// </summary>
    private static PlayingField instance = null;

    /// <summary>
    /// Grid holding 1 for occupied, 0 for vacant
    /// </summary>
    private Piece[,] field;

    /// <summary>
    /// Observer pattern event for when a row is 
    /// cleared - currently unused
    /// </summary>
    public event Action OnRowClear;
        private int rows = 22;
        private int columns = 15;

    /// <summary>
    /// Default constructor initializing the field
    /// to 22 rows and 15 columns
    /// </summary>
    private PlayingField() {
      field = new Piece[rows, columns];
    }

    /// <summary>
    /// Retrieves the Singleton pattern instance
    /// </summary>
    /// <returns>The Singleton instance</returns>
    public static PlayingField GetInstance() {
      if (instance == null) {
        instance = new PlayingField();
      }
      return instance;
    }

    /// <summary>
    /// Checks if a location in the field is empty (i.e. vacant)
    /// </summary>
    /// <param name="r">Row</param>
    /// <param name="c">Column</param>
    /// <returns>True if empty, False otherwise</returns>
    public bool IsEmpty(int r, int c) {
      if (r < 0 || r >= field.GetLength(0) || c < 0 || c >= field.GetLength(1)) {
        return false;
      }
      return field[r, c] == null;
    }

    public void DeletePiece(int row, int col) {
      field[row, col]?.Delete();
      field[row, col] = null;
    }

    public void SetPiece(int row, int col, Piece piece) {
      field[row, col] = piece;
    }

        public void ClearRow(int row)
        {
            for(int i = 0; i < columns; i++)
            {
                DeletePiece(row, i);
            }
        }

        /// <summary>
        /// Checks each row to see if any of them are filled and
        /// needs to be cleared, then clears those rows - currently
        /// unused and not implemented
        /// </summary>
        public void CheckClearAllRows() {
          
        }
  }
}
