using System;
using Labirinth2019.Lab;

namespace Labirinth2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var lab = new Labirinth(10, 8);
            Drawer.DrawLab(lab);

            ConsoleKeyInfo key;
            do {
                key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.LeftArrow: {
                            lab.HeroTryStep(Heroes.Direction.Left);
                            break;
                        }
                    case ConsoleKey. RightArrow: {
                            lab.HeroTryStep(Heroes.Direction.Right);
                            break;
                        }
                    case ConsoleKey.UpArrow: {
                            lab.HeroTryStep(Heroes.Direction.Up);
                            break;
                        }
                    case ConsoleKey.DownArrow: {
                            lab.HeroTryStep(Heroes.Direction.Down);
                            break;
                        }
                }

                Drawer.DrawLab(lab);
            } while (key.Key != ConsoleKey.Escape);

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
