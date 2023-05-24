using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    class Game
    {
        private char[,] gameBoard;
        private PacMan pacMan;
        private Ghost[] ghosts;
        private int score;
        private bool gameOver;

        public Game()
        {
            score = 0;
            gameOver = false;

            InitializeGameBoard();
            InitializeEntities();
        }

        private void InitializeGameBoard()
        {
            gameBoard = new char[,]
            {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#' },
            { '#', '#', '#', '.', '#', '#', '.', '#', '.', '#', '#', '.', '#', '#', '.', '#', '#', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '#', '.', '#', '.', '.', '.', '.', '.', '.', '.', '#', '#', '#', '.', '#' },
            { '#', '#', '#', '#', '.', '#', '.', '#', '#', '#', '#', '#', '#', '#', '.', '#', '#', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '#', '.', '.', '.', '.', '.', '.', '.', '#', '.', '.', '.', '.', '.', '#' },
            { '#', '#', '#', '#', '.', '#', '#', '#', '.', '#', '#', '#', '.', '#', '.', '#', '#', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#', '.', '#', '.', '.', '.', '#', '.', '#' },
            { '#', '#', '#', '.', '#', '#', '.', '#', '#', '#', '.', '#', '.', '#', '.', '#', '.', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#', '.', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
            };
        }

        private void InitializeEntities()
        {
            // Setze die Position des Pac-Man
            pacMan = new PacMan(1, 1);
            gameBoard[pacMan.Y, pacMan.X] = 'P';

            // Erzeuge die Geister
            ghosts = new Ghost[]
            {
            new Ghost(9, 7),
            new Ghost(9, 8),
            new Ghost(10, 7),
            new Ghost(10, 8)
            };

            // Setze die Geister auf das Spielfeld
            foreach (var ghost in ghosts)
            {
                gameBoard[ghost.Y, ghost.X] = 'G';
            }
        }

        public void Run()
        {
            ConsoleKeyInfo keyInfo;

            do
            {
                Console.Clear();
                PrintGameBoard();

                keyInfo = Console.ReadKey(true);
                MovePacMan(keyInfo.Key);
                MoveGhosts();
                CheckCollisions();

                Thread.Sleep(200); // Warte 200 Millisekunden, um eine flüssige Bewegung zu ermöglichen
            }
            while (keyInfo.Key != ConsoleKey.Escape && !gameOver);
        }

        private void PrintGameBoard()
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    Console.Write(gameBoard[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("Score: " + score);

            if (gameOver)
            {
                Console.WriteLine("Game Over! Final Score: " + score);
            }
        }

        private void MovePacMan(ConsoleKey key)
        {
            int newX = pacMan.X;
            int newY = pacMan.Y;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    newX = pacMan.X - 1;
                    break;
                case ConsoleKey.RightArrow:
                    newX = pacMan.X + 1;
                    break;
                case ConsoleKey.UpArrow:
                    newY = pacMan.Y - 1;
                    break;
                case ConsoleKey.DownArrow:
                    newY = pacMan.Y + 1;
                    break;
            }

            if (CanMoveTo(newX, newY))
            {
                MoveEntity(pacMan, newX, newY);
            }
        }

        private void MoveGhosts()
        {
            foreach (var ghost in ghosts)
            {
                int direction = ghost.GetRandomDirection();
                int newX = ghost.X;
                int newY = ghost.Y;

                switch (direction)
                {
                    case 0: // Links
                        newX = ghost.X - 1;
                        break;
                    case 1: // Rechts
                        newX = ghost.X + 1;
                        break;
                    case 2: // Oben
                        newY = ghost.Y - 1;
                        break;
                    case 3: // Unten
                        newY = ghost.Y + 1;
                        break;
                }

                if (CanMoveTo(newX, newY))
                {
                    MoveEntity(ghost, newX, newY);
                }
            }
        }

        private bool CanMoveTo(int x, int y)
        {
            return gameBoard[y, x] != '#';
        }

        private void MoveEntity(Entity entity, int newX, int newY)
        {
            ClearCell(entity.X, entity.Y);
            entity.X = newX;
            entity.Y = newY;
            gameBoard[entity.Y, entity.X] = entity.Symbol;
        }

        private void ClearCell(int x, int y)
        {
            gameBoard[y, x] = ' ';
        }

        private void CheckCollisions()
        {
            if (gameBoard[pacMan.Y, pacMan.X] == '.')
            {
                score += 10;
                ClearCell(pacMan.X, pacMan.Y);
            }

            foreach (var ghost in ghosts)
            {
                if (ghost.X == pacMan.X && ghost.Y == pacMan.Y)
                {
                    gameOver = true;
                    break;
                }
            }
        }
    }
}
