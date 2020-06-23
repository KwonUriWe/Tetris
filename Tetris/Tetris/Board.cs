using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Board     //도형이 움직일 영역에 대한 설정
    {
        //단일체 불러오기
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

        //보드의 크기 불러오기
        int[,] board = new int[GameRule.boardX, GameRule.boardY];

        //보드에 값에 접근할 수 있도록 크기를 따로 선언
        internal int this[int x, int y]
        {
            get
            {
                return board[x, y];
            }
        }

        //도형이 이동 가능한지 확인
        internal bool MoveEnable (int blockNum, int turnNum, int x, int y)
        {
            //도형의 16개 영역 모두 확인
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    //16개의 영역 중 1인영역(박스가 찬 영역)이 있으면 참
                    if (BlockValue.bvalues[blockNum, turnNum, xx, yy] != 0)
                    {
                        //현재 보드에 공간이 있으면 참
                        if (board[x + xx, y + yy] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //바닥부터 도형 쌓기
        internal void Store (int blockNum, int turnNum, int x, int y)
        {
            //도형의 16개 영역 모두 확인
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    //영역 내에 제대로 내려오면 참.
                    if ((x + xx >= 0) && (x + xx < GameRule.boardX) && (y + yy >= 0) && (y + yy < GameRule.boardY))
                    {
                        //보드에 있는 값에 도형의 값을 더해가기
                        board[x + xx, y + yy] += BlockValue.bvalues[blockNum, turnNum, xx, yy];
                    }
                }
            }
            CheckLines(y + 3);
        }

        //바닥부터 도형의 최대길이인 4개의 줄을 확인 후 지움
        private void CheckLines(int y)
        {
            //도형의 최대길이 4. 4개의 라인만 확인
            for (int yy = 0; yy < 4; yy++)
            {
                if ((y - yy) < GameRule.boardY)
                {
                    //현재 줄의 완성여부 확인. 완성이면 참
                    if (CheckLine(y - yy))
                    {
                        ClearLine(y - yy);
                        //지워진 줄의 윗줄을 복사해서 내려와야함
                        y++;
                    }
                }
            }
        }

        //라인을 지움
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

        //현재 줄의 완성 여부 확인
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
            for (int xx = 0; xx < GameRule.boardX; xx++)
            {
                for (int yy = 0; yy < GameRule.boardY; yy++)
                {
                    board[xx, yy] = 0;
                }
            }
        }    
    }
}
