using System.Drawing;

namespace Tetris
{
    class Game
    {
        Block now;
        Board gameBoard = Board.GameBoard;

        #region Singleton
        internal static Game Singleton
        {
            get;
            private set;
        }

        internal int this[int x, int y]
        {
            get
            {
                return gameBoard[x, y];
            }

        }

        static Game()
        {
            Singleton = new Game();
        }

        Game()
        {
            now = new Block();
        }
        #endregion

        internal Point NowPosition
        {
            get
            {
                if (now == null)
                {
                    return new Point(0,0);
                }
                return new Point(now.X, now.Y);
            }
        }

        internal int BlockNum
        {
            get
            {
                return now.BlockNum;
            }
        }

        internal int Turn
        {
            get
            {
                return now.Turn;
            }
        }

        //왼쪽 이동
        internal bool MoveLeft()
        {
            for (int xx=0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if (now.X + xx <= 0)
                        {
                            return false;
                        }
                    }
                }
            }

            if (gameBoard.MoveEnable(now.BlockNum, Turn, now.X - 1, now.Y))
            {
                now.MoveLeft();
                return true;
            }
            return false;
        }

        //오른쪽 이동
        internal bool MoveRight()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if (now.X + xx + 1 >= GameRule.boardX)
                        {
                            return false;
                        }
                    }
                }
            }

            if (gameBoard.MoveEnable(now.BlockNum, Turn, now.X + 1, now.Y))
            {
                now.MoveRight();
                return true;
            }
            return false;
        }

        //아래 이동
        internal bool MoveDown()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if (now.Y + yy + 1 >= GameRule.boardY)
                        {
                            gameBoard.Store(now.BlockNum, Turn, now.X, now.Y);
                            return false;
                        }
                    }
                }
            }

            if (gameBoard.MoveEnable(now.BlockNum, Turn, now.X, now.Y + 1))
            {
                now.MoveDown();
                return true;
            }
            gameBoard.Store(now.BlockNum, Turn, now.X, now.Y);
            return false;              
        }

        //도형 회전
        internal bool MoveTurn()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[now.BlockNum, (Turn + 1) % 4, xx, yy] != 0)
                    {
                        if (((now.X + xx) < 0 || (now.X + xx) >= GameRule.boardX) || (now.Y + yy) >= GameRule.boardY)
                        {
                            return false;
                        }
                    }
                }
            }

            if (gameBoard.MoveEnable(now.BlockNum, (Turn + 1) % 4, now.X, now.Y))
            {
                now.MoveTurn();
                return true;
            }
            return false;
        }

        internal bool Next()
        {
            now.Reset();
            return gameBoard.MoveEnable(now.BlockNum, Turn, now.X, now.Y);
        }

        internal void Restart()
        {
            gameBoard.ClearBoard();
        }
    }
}