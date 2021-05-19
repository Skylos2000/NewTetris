using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewTetris_Lib {
  /// <summary>
  /// Oracle game class controling the entire game
  /// </summary>
  public class Game {
    /// <summary>
    /// Current level the player is on - currently unused
    /// </summary>
    public int level;

    /// <summary>
    /// Flag to see if player is currently playing the level
    /// and therefore level code should be running - currently unused
    /// </summary>
    private bool isPlaying;

    /// <summary>
    /// Random object used to randomly select next shape
    /// to appear in level
    /// </summary>
    private Random random;

    /// <summary>
    /// Current player score
    /// </summary>
    public int score;

    /// <summary>
    /// Current shape dropping onto the playing field
    /// </summary>
    public static Shape curShape;

    /// <summary>
    /// Link to widget displaying the playing field. 
    /// Used to place pieces and shapes inside of it.
    /// </summary>
    public static Control field;

    /// <summary>
    /// Holds the image for a piece that is used to 
    /// compose a shape. This is used so the New Tetris Library
    /// can retrieve the image for a shape.
    /// </summary>
    public static Image imgPiece;

    /// <summary>
    /// Default constructor initializing random field and setting
    /// curShape to null
    /// </summary>
    public Game() {
      random = new Random();
      curShape = null;
    }

    /// <summary>
    /// Generates the next shape to be put into the playing field
    /// </summary>
    public void NextShape() {
      int shapeNum = random.Next(7);
      ShapeType shapeType = (ShapeType)shapeNum;
      curShape = ShapeFactory.MakeShape(shapeType);
    }

    public static Bitmap[] imgPieces;

    public static Bitmap RandomImagePiece()
    {
      int length = imgPieces.Length;
      int v = new Random().Next(0,length);
      return imgPieces[v];
    }

    public void AddScore(int linesCleared)
    {
      if (linesCleared == 1)
        score += 40 * (level + 1);
      else if (linesCleared == 2)
        score += 100 * (level + 1);
      else if (linesCleared == 3)
        score += 300 * (level + 1);
      else if (linesCleared == 4)
        score += 1200 * (level + 1);

      // TODO: This could be better. It would help with speed too.
      level = score / 100;
    }
  }
}
