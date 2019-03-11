using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLabirinth.Labirinth;

namespace GameLabirinth
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var lab = new LabirinthLevel(10, 8);
            Drawer.DrawLab(lab);

            ConsoleKeyInfo key;
            do {
                key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.LeftArrow: {
                            lab.HeroTryStep(Heroes.Direction.Left);
                            break;
                        }
                    case ConsoleKey.RightArrow: {
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
