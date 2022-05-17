﻿using System;

namespace ConsoleGame
{
    class Program
    {
        private static Random random = new Random();

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

            Console.SetCursorPosition(10, 10);
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

        static void Update(int[,] board, ref int currentLeft, ref int currentTop)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            var input = Console.ReadKey();
            board[currentTop, currentLeft] = 0;

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (currentTop > 0)
                    {
                        currentTop--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (currentTop < boardHeight - 1)
                    {
                        currentTop++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentLeft > 0)
                    {
                        currentLeft--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (currentLeft < boardWidth - 1)
                    {
                        currentLeft++;
                    }
                    break;
                default:
                    break;
            }

            board[currentTop, currentLeft] = 1;
        }

        static void CreateObstacles(int[,] board, int count)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            for (int i = 0; i < count; i++)
            {
                board[random.Next(0, boardHeight), random.Next(0, boardWidth)] = 2;
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
            int obstacleCount = 10;
            int[,] board = new int[boardHeight, boardWidth];

            Console.CursorVisible = false;

            DrawBorders(boardWidth, boardHeight);

            CreateObstacles(board, obstacleCount);

            // Define player
            board[currentTop, currentLeft] = 1;

            DrawBoard(board);

            do
            {
                Update(board, ref currentLeft, ref currentTop);
                DrawBoard(board);
            } while (true);
        }
    }
}
