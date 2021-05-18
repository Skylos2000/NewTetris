using System;
using System.Collections.Generic;
using System.Linq;

namespace NewTetris_Lib {
  /// <summary>
  /// Encodes information about the playing
  /// field of the game. Uses a grid of rows
  /// and cols storing 1 for occupied and 0
  /// for vacant
  /// </summary>
  public class PlayingField
  {
    /// <summary>
    /// Singleton pattern instance
    /// </summary>
    private static PlayingField instance = null;

    public const int MaxRows = 22;
    public const int MaxCols = 15;

    private List<Piece> pieces;

    /// <summary>
    /// Observer pattern event for when a row is 
    /// cleared - currently unused
    /// </summary>
    public event Action OnRowClear;

    /// <summary>
    /// Default constructor initializing the field
    /// to 22 rows and 15 columns
    /// </summary>
    private PlayingField()
    {
      pieces = new List<Piece>(MaxCols * MaxRows);
    }

    /// <summary>
    /// Retrieves the Singleton pattern instance
    /// </summary>
    /// <returns>The Singleton instance</returns>
    public static PlayingField GetInstance()
    {
      if (instance == null)
      {
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
    public bool IsEmpty(int r, int c)
    {
      return !pieces.Any(piece => piece.fieldRow == r && piece.fieldCol == c);
    }

    public void AddPiece(Piece piece)
    {
      if (IsEmpty(piece.fieldRow, piece.fieldCol)) {
        pieces.Add(piece);
      } else
      {
        throw new ArgumentException($"Piece at (r={piece.fieldRow},c={piece.fieldCol}) already exists.");
      }
    }

    private void DeleteRow(int row)
    {
      var toRemove = pieces.Where(piece => piece.fieldRow == row).ToList();

      foreach (var piece in toRemove)
      {
        pieces.Remove(piece);
        piece.Delete();
      }

      foreach (var piece in pieces)
      {
        if (piece.fieldRow < row)
          piece.fieldRow = piece.fieldRow + 1;
      }
    }

    public bool IsRowFull(int row) => pieces.Count(piece => piece.fieldRow == row) >= MaxCols;

    /// <summary>
    /// Checks each row to see if any of them are filled and
    /// needs to be cleared, then clears those rows - currently
    /// unused and not implemented
    /// </summary>
    public void CheckClearAllRows()
    {
      for (int row = 0; row < MaxRows; row++)
      {
        if (IsRowFull(row))
        {
          DeleteRow(row);
        }
      }
    }
  }
}
