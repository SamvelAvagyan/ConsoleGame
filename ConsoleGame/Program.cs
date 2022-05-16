using System;

namespace ConsoleGame
{
    class Program
    {
        static void DrawXY(int left, int top, char c)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(c);
        }

        static void DrawBorders(int boardWidth, int boardHeight)
        {
            for (int i = 0; i < boardWidth + 2; i++)
            {
                DrawXY(i, 0, '*');
                DrawXY(i, boardHeight + 1, '*');
            }

            for (int i = 0; i < boardHeight; i++)
            {
                DrawXY(0, i + 1, '*');
                DrawXY(boardWidth + 1, i + 1, '*');
            }
        }

        static void DrawBoard(int[,] board)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    DrawXY(j + 1, i + 1, GetSymbol(board[i, j]));
                }
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
            int currentTop = startTop;
            int currentLeft = startLeft;
            int[,] board = new int[boardHeight, boardWidth];

            Console.CursorVisible = false;

            DrawBorders(boardWidth, boardHeight);

            // Define player
            board[currentTop, currentLeft] = 1;

            DrawBoard(board);

            do
            {
                var input = Console.ReadKey();
                board[currentTop, currentLeft] = 0;

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentTop--;
                        break;
                    case ConsoleKey.DownArrow:
                        currentTop++;
                        break;
                    case ConsoleKey.LeftArrow:
                        currentLeft--;
                        break;
                    case ConsoleKey.RightArrow:
                        currentLeft++;
                        break;
                    default:
                        break;
                }

                board[currentTop, currentLeft] = 1;
                DrawBoard(board);

            } while (true);
        }
    }
}
