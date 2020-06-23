using System;

namespace Tetris
{
    class Block
    {
        internal int X 
        { 
            get; 
            private set; 
        }
        
        internal int Y 
        { 
            get; 
            private set; 
        }

        internal int Turn
        {
            get;
            private set;
        }

        internal int BlockNum
        {
            get;
            private set;
        }

        internal Block()
        {
            Reset();
        }

        internal void Reset()
        {
            X = GameRule.startPointX;
            Y = GameRule.startPointY;
            
            Random random = new Random();
            Turn = random.Next() % 4;
            BlockNum = random.Next() % 7;
        }

        internal void MoveLeft()
        {
            X--;
        }

        internal void MoveRight()
        {
            X++;
        }

        internal void MoveDown()
        {
            Y++;
        }

        internal void MoveTurn()
        {
            Turn = (Turn + 1) % 4;
        }
    }
}
