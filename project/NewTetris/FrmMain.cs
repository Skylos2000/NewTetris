using NewTetris.Properties;
using NewTetris_Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewTetris
{
  public partial class FrmMain : Form
  {
    public Game game;

    private bool paused;

    private System.Media.SoundPlayer soundLeftRight = new System.Media.SoundPlayer(Resources.left_right);

    private System.Media.SoundPlayer soundRotate = new System.Media.SoundPlayer(Resources.rotate);

    public FrmMain()
    {
      InitializeComponent();

      soundLeftRight.LoadAsync();
      soundRotate.LoadAsync();

      Bitmap[] imgs = {Resources.block_piece, Resources.block_piece_red, Resources.block_piece_yellow};
      Game.imgPieces = imgs;
      game = new Game();
      Game.field = lblPlayingField;
      game.NextShape();
      paused = false;
    }

    private void tmrCurrentPieceFall_Tick(object sender, EventArgs e)
    {
      if (Game.curShape != null)
      {
        if (!Game.curShape.TryMoveDown())
        {
          bool isOver = !Game.curShape.DissolveIntoField();
          Game.curShape = null;
          int rowsCleared = PlayingField.GetInstance().CheckClearAllRows();
          game.AddScore(rowsCleared);
          scoreLabel.Text = game.score.ToString();
          lblLevel.Text = game.level.ToString();
          isOver |= PlayingField.GetInstance().IsGameOver();

          if (isOver)
            isOver = PauseAndDisplayLoss();

          // TODO: This should probably be slowed down a lot
          tmrCurrentPieceFall.Interval = 500 / (int) Math.Pow(2, game.level);
          game.NextShape();
        }
      }
    }

    private void PauseGame()
    {
      paused = true;
      tmrCurrentPieceFall.Stop();
    }

    private void ResumeGame()
    {
      paused = false;
      tmrCurrentPieceFall.Start();
    }

    private void PauseAndShowDialogBox()
    {
      PauseGame();
      string message = "Press OK to resume.";
      string title = "Game Paused";
      MessageBoxButtons buttons = MessageBoxButtons.OK;
      DialogResult result = MessageBox.Show(message, title, buttons);
      if (result == DialogResult.OK)
      {
        //this.Close();
        ResumeGame();
      }
    }

    private bool PauseAndDisplayLoss()
    {
      PauseGame();
      var result = MessageBox.Show(
        "You're a loser. Would you like to try again?",
        "You lost",
        MessageBoxButtons.YesNo
      );
      if (result == DialogResult.Yes)
      {
        game.ResetGame();
        ResumeGame();
        return true;
      }
      else
      {
        this.Close();
        return false;
      }
    }

    private void FrmMain_KeyUp(object sender, KeyEventArgs e)
    {
      if (!paused && e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
      {
        soundLeftRight.Play();
        Game.curShape.TryMoveLeft();
      }
      else if (!paused && e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
      {
        soundLeftRight.Play();

        Game.curShape.TryMoveRight();
      }
      else if (!paused && e.KeyCode == Keys.Z)
      {
        soundRotate.Play();
        Game.curShape.RotateCCW();
      }
      else if (!paused && e.KeyCode == Keys.X)
      {
        soundRotate.Play();
        Game.curShape.RotateCW();
      }
      else if (!paused && e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
      {
        //soundRotate.PlaySync();
        Game.curShape.TryMoveDown();
      }
      else if (e.KeyCode == Keys.Space)
      {
        PauseAndShowDialogBox();
      }
    }
  }
}