using System;

namespace ConsoleGame
{
    class Program
    {
        static void Draw(int left, int top, char c)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(c);
        }

        static void DrawBorders(int boardWidth, int boardHeight)
        {
            for (int i = 0; i < boardWidth + 2; i++)
            {
                Draw(i, 0, '*');
                Draw(i, boardHeight + 1, '*');
            }

            for (int i = 0; i < boardHeight; i++)
            {
                Draw(0, i + 1, '*');
                Draw(boardWidth + 1, i + 1, '*');
            }
        }

        static char GetSymbol(int number)
        {
            // 0 - Empty
            // 1 - Player
            // 2 - Obstacle
            // 3 - Bonus
            // 4 - Enemy
            switch (number)
            {
                case 0:
                    return ' ';
                case 1:
                    return 'P';
                case 2:
                    return '*';
                case 3:
                    return 'B';
                case 4:
                    return 'E';
                default:
                    return ' ';
            }
        }

        static void Main(string[] args)
        {
            int boardWidth = 30;
            int boardHeight = 20;
            int startLeft = boardWidth / 2;
            int startTop = boardHeight / 2;
            int[,] board = new int[boardHeight, boardWidth];

            Console.CursorVisible = false;

            DrawBorders(boardWidth, boardHeight);

            // Define player
            board[startTop, startLeft] = 1;

            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    Draw(j + 1, i + 1, GetSymbol(board[i, j]));
                }
            }

            Console.ReadKey();
        }
    }
}
