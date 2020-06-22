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
    public partial class FormMain : Form
    {
        Game game;
        int mapX;
        int mapY;
        int brickWidth;
        int brickHeight;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            game = Game.Singleton;
            mapX = GameRule.mapX;
            mapY = GameRule.mapY;
            brickWidth = GameRule.brickWidth;
            brickHeight = GameRule.brickHeight;
            this.SetClientSizeCore(mapX * brickWidth, mapY * brickHeight);
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            DrawGraduation(e.Graphics);
            DrawBrick(e.Graphics);
        }

        private void DrawBrick(Graphics graphics)
        {
            Pen brickPen = new Pen(Color.Red, 4);
            Point current = game.currentPosition;
            Rectangle currentRt = new Rectangle(current.X * brickWidth + 2, current.Y * brickHeight + 2, brickWidth - 4, brickHeight - 4);
            graphics.DrawRectangle(brickPen, currentRt);
        }

        private void DrawGraduation(Graphics graphics)
        {
            DrawHorizons(graphics);
            DrawVerticals(graphics);
        }

        private void DrawHorizons(Graphics graphics)
        {
            Point st = new Point();
            Point et = new Point();
            for (int cy = 0; cy < mapY; cy++)
            {
                st.X = 0;
                st.Y = cy * brickHeight;
                et.X = mapX * brickWidth;
                et.Y = st.Y;
                graphics.DrawLine(Pens.LightGray, st, et);
            }
        }

        private void DrawVerticals(Graphics graphics)
        {
            Point st = new Point();
            Point et = new Point();
            for(int cx = 0; cx < mapX; cx++)
            {
                st.X = cx * brickWidth;
                st.Y = 0;
                et.X = st.X;
                et.Y = mapY * brickHeight;
                graphics.DrawLine(Pens.LightGray, st, et);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:MoveRight(); return;
                case Keys.Left: MoveLeft(); return;
                case Keys.Space: MoveDown(); return;
                case Keys.Up: MoveTurn(); return;
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

        private void MoveDown()
        {
            if (game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
        }

        private void MoveTurn()
        {

        }

        private Region MakeRegion(int cx, int cy)
        {
            Point current = game.currentPosition;
            Rectangle rect1 = new Rectangle(current.X * brickWidth, current.Y * brickHeight, brickWidth, brickHeight);
            Rectangle rect2 = new Rectangle((current.X + cx) * brickWidth, (current.Y + cy) * brickHeight, brickWidth, brickHeight);
            Region rg1 = new Region(rect1);
            Region rg2 = new Region(rect2);
            rg1.Union(rg2);
            return rg1;
        }

        private void timerDrop_Tick(object sender, EventArgs e)
        {
            MoveDown();
        }
    }
}
