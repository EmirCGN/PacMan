using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    class PacMan : Entity
    {
        public PacMan(int x, int y) : base(x, y, 'P')
        {
        }

        public void MoveUp(char[,] gameBoard)
        {
            if (Y > 0 && gameBoard[Y - 1, X] != '#')
            {
                Y--;
                if (gameBoard[Y, X] == '.')
                    gameBoard[Y, X] = ' ';
            }
        }

        public void MoveDown(char[,] gameBoard)
        {
            if (Y < gameBoard.GetLength(0) - 1 && gameBoard[Y + 1, X] != '#')
            {
                Y++;
                if (gameBoard[Y, X] == '.')
                    gameBoard[Y, X] = ' ';
            }
        }

        public void MoveLeft(char[,] gameBoard)
        {
            if (X > 0 && gameBoard[Y, X - 1] != '#')
            {
                X--;
                if (gameBoard[Y, X] == '.')
                    gameBoard[Y, X] = ' ';
            }
        }

        public void MoveRight(char[,] gameBoard)
        {
            if (X < gameBoard.GetLength(1) - 1 && gameBoard[Y, X + 1] != '#')
            {
                X++;
                if (gameBoard[Y, X] == '.')
                    gameBoard[Y, X] = ' ';
            }
        }
    }
}
