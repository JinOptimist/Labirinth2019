using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLabirinth.Labirinth;
using GameLabirinth.Labirinth.CellObject;

namespace GameLabirinth
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var showLabGeneration = true;
            var hero = new Hero(0, 0);
            var generator = new LabirinthGenerator(10, 5, showLabGeneration: showLabGeneration);
            var lab = generator.Generate(hero);

            Drawer.DrawLab(lab);

            ConsoleKeyInfo key;
            do {
                key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.LeftArrow: {
                            lab.HeroDoStep(Heroes.Direction.Left);
                            break;
                        }
                    case ConsoleKey.RightArrow: {
                            lab.HeroDoStep(Heroes.Direction.Right);
                            break;
                        }
                    case ConsoleKey.UpArrow: {
                            lab.HeroDoStep(Heroes.Direction.Up);
                            break;
                        }
                    case ConsoleKey.DownArrow: {
                            lab.HeroDoStep(Heroes.Direction.Down);
                            break;
                        }
                    case ConsoleKey.R: {
                            hero.X = 0;
                            hero.Y = 0;
                            lab = generator.Generate(hero);
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
