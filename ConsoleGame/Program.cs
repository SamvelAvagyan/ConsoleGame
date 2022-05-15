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

        static void Main(string[] args)
        {
            int boardWidth = 30;
            int boardHeight = 20;
            int[,] board = new int[boardHeight, boardWidth];

            for(int i = 0; i < boardHeight; i++)
            {
                for(int j = 0; j < boardWidth; j++)
                {
                    Draw(i + 1, j + 1, board[i, j].ToString()[0]);
                }
            }
        }
    }
}
