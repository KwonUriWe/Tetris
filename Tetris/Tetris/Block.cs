using System;

namespace Tetris
{
    class Block     //도형에 대한 설정
    {
        //X축 값
        internal int X 
        { 
            get; 
            private set; 
        }
        
        //Y축 값
        internal int Y 
        { 
            get; 
            private set; 
        }

        //회전
        internal int Turn
        {
            get;
            private set;
        }

        //도형 번호
        internal int BlockNum
        {
            get;
            private set;
        }

        //다음으로 떨어질 도형 설정 호출
        internal Block()
        {
            Reset();
        }

        //다음으로 떨어질 도형 설정
        internal void Reset()
        {
            X = GameRule.startPointX;
            Y = GameRule.startPointY;
            
            Random random = new Random();
            Turn = random.Next() % 4;
            BlockNum = random.Next() % 7;
        }

        //왼쪽으로 이동 값 설정
        internal void MoveLeft()
        {
            X--;
        }

        //오른쪽으로 이동 값 설정
        internal void MoveRight()
        {
            X++;
        }

        //아래로 이동 값 설정
        internal void MoveDown()
        {
            Y++;
        }

        //도형 회전 값 설정
        internal void MoveTurn()
        {
            Turn = (Turn + 1) % 4;
        }
    }
}
