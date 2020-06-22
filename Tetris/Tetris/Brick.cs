using System;

namespace Tetris
{
    class Brick
    {
        internal int x { get; private set; }
        internal int y { get; private set; }

        internal Brick()
        {
            Reset();
        }

        internal void Reset()
        {
            x = GameRule.startPointX;
            y = GameRule.startPointY;
        }

        internal void MoveLeft()
        {
            x--;
        }

        internal void MoveRight()
        {
            x++;
        }

        internal void MoveDown()
        {
            y++;
        }
    }
}
