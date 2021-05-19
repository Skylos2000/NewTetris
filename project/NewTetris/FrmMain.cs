﻿using NewTetris.Properties;
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

    private void PlayBackgroundMusic()
    {
      System.Media.SoundPlayer Player = new System.Media.SoundPlayer(NewTetris.Properties.Resources.bg_music);

      //Player.SoundLocation = "bg_music.mp3";
      Player.PlayLooping();
    }
    public FrmMain()
    {
      InitializeComponent();
      Bitmap[] imgs = { Resources.block_piece, Resources.block_piece_red, Resources.block_piece_yellow };
      Game.imgPieces = imgs;
      game = new Game();
      Game.field = lblPlayingField;
      game.NextShape();
      PlayBackgroundMusic();
      paused = false;
    }

    private void tmrCurrentPieceFall_Tick(object sender, EventArgs e)
    {
      if (Game.curShape != null)
      {
        if (!Game.curShape.TryMoveDown())
        {
          Game.curShape.DissolveIntoField();
          Game.curShape = null;
          int rowsCleared = PlayingField.GetInstance().CheckClearAllRows();
          game.AddScore(rowsCleared);
          scoreLabel.Text = game.score.ToString();
          lblLevel.Text = game.level.ToString();

          // TODO: This should probably be slowed down a lot
          tmrCurrentPieceFall.Interval = 500 / (int)Math.Pow(2, game.level);
          game.NextShape();
        }
      }
    }

    private void ResumeGame()
    {
      paused = false;
      tmrCurrentPieceFall.Start();
    }

    private void PauseAndShowDialogBox()
    {
      paused = true;
      tmrCurrentPieceFall.Stop();
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

    private void FrmMain_KeyUp(object sender, KeyEventArgs e)
    {
      System.Media.SoundPlayer leftRight = new System.Media.SoundPlayer(NewTetris.Properties.Resources.left_right);
      System.Media.SoundPlayer rotate = new System.Media.SoundPlayer(Properties.Resources.rotate);
      if (!paused && e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
      {
        leftRight.PlaySync();
        Game.curShape.TryMoveLeft();
      }
      else if (!paused && e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
      {
        leftRight.PlaySync();

        Game.curShape.TryMoveRight();
      }
      else if (!paused && e.KeyCode == Keys.Z)
      {
        rotate.PlaySync();
        Game.curShape.RotateCCW();
      }
      else if (!paused && e.KeyCode == Keys.X)
      {
        rotate.PlaySync();
        Game.curShape.RotateCW();
      }
      else if (!paused && e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
      {
        //rotate.PlaySync();
        Game.curShape.TryMoveDown();
      }
      else if (e.KeyCode == Keys.Space)
      {

        PauseAndShowDialogBox();
      }
    }
  }
}
