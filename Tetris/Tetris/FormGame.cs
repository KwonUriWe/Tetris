using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class FormGame : Form
    {
        Game game;
        int boardX;
        int boardY;
        int blockWidth;
        int blockHeight;

        public FormGame()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            game = Game.Singleton;
            boardX = GameRule.boardX;
            boardY = GameRule.boardY;
            blockWidth = GameRule.blockWidth;
            blockHeight = GameRule.blockHeight;
            this.SetClientSizeCore(boardX * blockWidth, boardY * blockHeight);
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            DoubleBuffered = true;
            DrawGraduation(e.Graphics);
            DrawBlock(e.Graphics);
            DrawBoard(e.Graphics);
        }

        private void DrawGraduation(Graphics graphics)
        {
            DrawHorizons(graphics);
            DrawVerticals(graphics);
        }

        private void DrawBlock(Graphics graphics)
        {
            Pen blockPen = new Pen(Color.Black, 4);
            Point now = game.NowPosition;
            int blockNum = game.BlockNum;
            int turnNum = game.Turn;

            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[blockNum, turnNum, xx, yy] != 0)
                    {
                        Rectangle nowRt = new Rectangle((now.X + xx) * blockWidth + 2, (now.Y + yy) * blockHeight + 2, blockWidth - 4, blockHeight - 4);
                        graphics.DrawRectangle(blockPen, nowRt);
                    }
                }
            }
        }

        private void DrawBoard(Graphics graphics)
        {
            for (int xx = 0; xx < boardX; xx++)
            {
                for (int yy=0; yy<boardY; yy++)
                {
                    if (game[xx, yy] != 0)
                    {
                        Rectangle nowRt = new Rectangle(xx * blockWidth + 2, yy * blockHeight + 2, blockWidth - 4, blockHeight - 4);
                        graphics.DrawRectangle(Pens.White, nowRt);
                        graphics.FillRectangle(Brushes.Black, nowRt);
                    }
                }
            }
        }

        private void DrawHorizons(Graphics graphics)
        {
            Point st = new Point();
            Point et = new Point();
            for (int cy = 0; cy < boardY; cy++)
            {
                st.X = 0;
                st.Y = cy * blockHeight;
                et.X = boardX * blockWidth;
                et.Y = st.Y;
                graphics.DrawLine(Pens.LightGray, st, et);
            }
        }

        private void DrawVerticals(Graphics graphics)
        {
            Point st = new Point();
            Point et = new Point();
            for(int cx = 0; cx < boardX; cx++)
            {
                st.X = cx * blockWidth;
                st.Y = 0;
                et.X = st.X;
                et.Y = boardY * blockHeight;
                graphics.DrawLine(Pens.LightGray, st, et);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:MoveRight(); 
                    return;
                
                case Keys.Left: MoveLeft(); 
                    return;
                
                case Keys.Down: MoveDown(); 
                    return;

                case Keys.Space: MoveSsDown();
                    return;

                case Keys.Up: MoveTurn(); 
                    return;
            }
        }

        private void MoveRight()
        {
            if (game.MoveRight())
            {
                Region rg = MakeRegion(-1, 0);
                Invalidate(rg);
            }
        }

        private void MoveLeft()
        {
            if (game.MoveLeft())
            {
                Region rg = MakeRegion(1, 0);
                Invalidate(rg);
            }
        }

        private void MoveTurn()
        {
            if (game.MoveTurn())
            {
                Region rg = MakeRegion();
                Invalidate(rg);
            }
        }

        private void MoveDown()
        {
            if (game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            else
            {
                EndCheck();
            }
        }

        private void MoveSsDown()
        {
            while (game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            EndCheck();
        }

        private void EndCheck()
        {
            if (game.Next())
            {
                Invalidate();
            }
            else
            {
                timerDrop.Enabled = false;

                DialogResult result = MessageBox.Show("다시 시작하시겠습니까??", "게임 끝", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    game.Restart();
                    timerDrop.Enabled = true;
                    Invalidate();
                }
                else
                {
                    Close();
                }
            }
        }

        private Region MakeRegion(int cx, int cy)
        {
            Point now = game.NowPosition;
            int blockNum = game.BlockNum;
            int turnNum = game.Turn;

            Region region = new Region();
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[blockNum, turnNum, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * blockWidth, (now.Y + yy) * blockHeight, blockWidth, blockHeight);
                        Rectangle rect2 = new Rectangle((now.X + cx + xx) * blockWidth, (now.Y + cy + yy) * blockHeight, blockWidth, blockHeight);
                        Region rg1 = new Region(rect1);
                        Region rg2 = new Region(rect2);

                        region.Union(rg1);
                        region.Union(rg2);
                    }
                }
            }
            return region;
        }

        private Region MakeRegion()
        {
            Point now = game.NowPosition;
            int blockNum = game.BlockNum;
            int turnNum = game.Turn;
            int TurnedNum = (turnNum + 3) % 4;

            Region region = new Region();
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[blockNum, turnNum, xx, yy] != 0)
                    {
                        Rectangle rect = new Rectangle((now.X + xx) * blockWidth, (now.Y + yy) * blockHeight, blockWidth, blockHeight);
                        Region rg = new Region(rect);

                        region.Union(rg);
                    }
                    if (BlockValue.bvalues[blockNum, TurnedNum, xx, yy] != 0)
                    {
                        Rectangle rect = new Rectangle((now.X + xx) * blockWidth, (now.Y + yy) * blockHeight, blockWidth, blockHeight);
                        Region rg = new Region(rect);

                        region.Union(rg);
                    }
                }
            }
            return region;
        }

        private void timerDrop_Tick(object sender, EventArgs e)
        {
            MoveDown();
        }
    }
}
