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

        private void PlayBackgroundMusic()
        {
            System.Media.SoundPlayer Player = new System.Media.SoundPlayer(NewTetris.Properties.Resources.bg_music);

            //Player.SoundLocation = "bg_music.mp3";
            Player.PlayLooping();
        }
        public FrmMain()
        {
            InitializeComponent();
            Game.imgPiece = Resources.block_piece;
            game = new Game();
            Game.field = lblPlayingField;
            game.NextShape();
            PlayBackgroundMusic();
        }

        private void tmrCurrentPieceFall_Tick(object sender, EventArgs e)
        {
            if (Game.curShape != null)
            {
                if (!Game.curShape.TryMoveDown())
                {
                    Game.curShape.DissolveIntoField();
                    Game.curShape = null;
                    PlayingField.GetInstance().CheckClearAllRows();
                    game.NextShape();
                }
            }
        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            System.Media.SoundPlayer leftRight = new System.Media.SoundPlayer(NewTetris.Properties.Resources.left_right);
            System.Media.SoundPlayer rotate = new System.Media.SoundPlayer(Properties.Resources.rotate);
            if (e.KeyCode == Keys.Left)
            {
                leftRight.PlaySync();
                Game.curShape.TryMoveLeft();
            }
            else if (e.KeyCode == Keys.Right)
            {
                leftRight.PlaySync();

                Game.curShape.TryMoveRight();
            }
            else if (e.KeyCode == Keys.Z)
            {
                rotate.PlaySync();
                Game.curShape.RotateCCW();
            }
            else if (e.KeyCode == Keys.X)
            {
                rotate.PlaySync();
                Game.curShape.RotateCW();
            }
        }
    }
}
