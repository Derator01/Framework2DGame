using SimpleGame;
using System;
using System.Threading;

namespace Debug
{
    public static class DebugClass
    {
        private readonly static Game game = new Game();

        public static void Main()
        {
            Console.CursorVisible = false;

            char current = ' ';
            Thread gameSeq = new Thread(DrawMap);
            gameSeq.Start();

            while (true)
            {
                Console.WriteLine(current);

                switch (Console.ReadKey(false).KeyChar)
                {
                    case '/':
                        Exit();
                        return;                        
                    case 'w':
                        game.MoveUp();
                        break;
                    case 's':
                        game.MoveDown();
                        break;
                    case 'd':
                        game.MoveRight();
                        break;
                    case 'a':
                        game.MoveLeft();
                        break;
                }

                if (!game.IsAlive)
                    return;

            }
        }

        private static void DrawMap()
        {
            while (true)
            {
                var arr = game.GetMap();

                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                        Console.Write(arr[i, j]);
                    Console.SetCursorPosition(0, i + 1);
                }

                Thread.Sleep(1);
            }
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }
    }
}