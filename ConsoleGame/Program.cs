using System;
using System.Timers;

namespace ConsoleGame
{
    class Program
    {
        private static Random random = new Random();
        private static Timer timer = new Timer();

        private static int boardWidth = 30;
        private static int boardHeight = 20;
        private static int startLeft = boardWidth / 2;
        private static int startTop = boardHeight / 2;
        private static int currentTop = startTop;
        private static int currentLeft = startLeft;
        private static int obstacleCount = 10;
        private static int score = 0;
        private static int remainingTime = 10;
        private static bool isGameRunning = true;
        private static int winningScore = 3;
        private static int[,] board = new int[boardHeight, boardWidth];

        static void Draw()
        {
            DrawBoard();
            DrawScore();
            DrawTimer();
        }

        static void DrawXY(int left, int top, char c)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(c);
        }

        static void DrawXY(int left, int top, string s)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(s);
        }

        static void DrawBorders()
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

        static void DrawBoard()
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    char symbol = GetSymbol(board[i, j]);
                    switch (symbol)
                    {
                        case 'B':
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case 'P':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                    }

                    DrawXY(j + 1, i + 1, symbol);

                    Console.ResetColor();
                }
            }

            Console.SetCursorPosition(10, 10);
        }

        static void DrawScore()
        {
            DrawXY(boardWidth + 3, 0, $"Score: {score}");
        }

        static void DrawTimer()
        {
            DrawXY(boardWidth + 3, 2, $"Time: {remainingTime}");
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

        static void BonusEatSound()
        {
            Console.Beep(300, 100);
        }

        static void Update()
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            var input = Console.ReadKey();
            board[currentTop, currentLeft] = 0;

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if(currentTop > 0 && board[currentTop - 1, currentLeft] == 3)
                    {
                        score++;
                        CreateBonus();
                        BonusEatSound();
                    }

                    if (currentTop > 0 && board[currentTop - 1, currentLeft] != 2)
                    {
                        currentTop--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (currentTop < boardHeight - 1 && board[currentTop + 1, currentLeft] == 3)
                    {
                        score++;
                        CreateBonus();
                        BonusEatSound();
                    }

                    if (currentTop < boardHeight - 1 && board[currentTop + 1, currentLeft] != 2)
                    {
                        currentTop++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentLeft > 0 && board[currentTop, currentLeft - 1] == 3)
                    {
                        score++;
                        CreateBonus();
                        BonusEatSound();
                    }

                    if (currentLeft > 0 && board[currentTop, currentLeft - 1] != 2)
                    {
                        currentLeft--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (currentLeft < boardWidth - 1 && board[currentTop, currentLeft + 1] == 3)
                    {
                        score++;
                        CreateBonus();
                        BonusEatSound();
                    }

                    if (currentLeft < boardWidth - 1 && board[currentTop, currentLeft + 1] != 2) 
                    {
                        currentLeft++;
                    }
                    break;
                default:
                    break;
            }

            board[currentTop, currentLeft] = 1;
        }

        static void CreateBonus()
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            board[random.Next(0, boardHeight), random.Next(0, boardWidth)] = 3;
        }

        static void CreateObstacles(int count)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            for (int i = 0; i < count; i++)
            {
                board[random.Next(0, boardHeight), random.Next(0, boardWidth)] = 2;
            }
        }

        static async void Main(string[] args)
        {
            Console.CursorVisible = false;
            DrawBorders();

            CreateObstacles(obstacleCount);
            CreateBonus();

            // Define player
            board[currentTop, currentLeft] = 1;

            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();

            do
            {
                if (isGameRunning)
                {
                    Draw();
                    Update();
                }
            } while (true);
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        { 
            if(remainingTime > 0)
            {
                remainingTime--;
            }
            else
            {
                isGameRunning = false;
                Console.Clear();
                if(score >= 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    DrawXY(0, 0, "You Won!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    DrawXY(0, 0, "You Lost!");
                }
                Console.ResetColor();
            }
        }
    }
}
