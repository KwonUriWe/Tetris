using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Board
    {
        internal static Board GameBoard
        {
            get;
            private set;
        }

        static Board()
        {
            GameBoard = new Board();
        }

        Board()
        {
            
        }

        int[,] board = new int[GameRule.boardX, GameRule.boardY];

        internal int this[int x, int y]
        {
            get
            {
                return board[x, y];
            }
        }

        internal bool MoveEnable (int blockNum, int turnNum, int x, int y)
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[blockNum, turnNum, xx, yy] != 0)
                    {
                        if (board[x + xx, y + yy] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        internal void Store (int blockNum, int turnNum, int x, int y)
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if ((x + xx >= 0) && (x + xx < GameRule.boardX) && (y + yy >= 0) && (y + yy < GameRule.boardY))
                    {
                        board[x + xx, y + yy] += BlockValue.bvalues[blockNum, turnNum, xx, yy];
                    }
                }
            }
            CheckLines(y + 3);
        }

        private void CheckLines(int y)
        {
            int yy = 0;
            for (yy = 0; y < 4; yy++)
            {
                if (y - yy < GameRule.boardY)
                {
                    if (CheckLine(y - yy))
                    {
                        ClearLine(y - yy);
                        y++;
                    }
                }
            }
        }

        private void ClearLine(int y)
        { 
            for (; y > 0; y--)
            {
                for (int xx = 0; xx < GameRule.boardX; xx++)
                {
                    board[xx, y] = board[xx, y - 1];
                }
            }
        }

        private bool CheckLine(int y)
        {
            for (int xx = 0; xx < GameRule.boardX; xx++)
            {
                if (board[xx, y] == 0)
                {
                    return false;
                }
            }
            return true;
        }
    
        internal void ClearBoard()
        {
            for (int xx=0; xx < GameRule.boardX; xx++)
            {
                for (int yy=0; yy<GameRule.boardY; yy++)
                {
                    board[xx, yy] = 0;
                }
            }
        }    
    }
}
