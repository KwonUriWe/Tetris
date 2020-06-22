using System.Drawing;

namespace Tetris
{
    class Game
    {
        Brick current;
        internal Point currentPosition
        {
            get
            {
                if (current == null)
                {
                    return new Point(0,0);
                }
                return new Point(current.x, current.y);
            }
        }

        #region 단일체
        internal static Game Singleton { get; private set; }

        static Game()
        {
            Singleton = new Game();
        }

        Game()
        {
            current = new Brick();
        }

        #endregion
        //왼쪽 이동
        internal bool MoveLeft()
        {
            if (current.x > 0)
            {
                current.MoveLeft();
                return true;
            }
            return false;
        }

        //오른쪽 이동
        internal bool MoveRight()
        {
            if ((current.x+1) < GameRule.mapX)
            {
                current.MoveRight();
                return true;
            }
            return false;
        }

        //아래 이동
        internal bool MoveDown()
        {
            if ((current.y+1) < GameRule.mapY)
            {
                current.MoveDown();
                return true;
            }
            return false;
        }
    }
}