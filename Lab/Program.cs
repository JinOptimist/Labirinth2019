using Labirinth2019.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth
{
    class Program
    {
        static void Main(string[] args)
        {
            //Rec(4);
            Console.WriteLine("Start");
            var lab = new Labirinth2019.Lab.Labirinth(10, 8);

            var labGen = new Lab.LabirinthGenerator(lab);
            labGen.Step(lab[0, 0]);

            Drawer.DrawLab(lab);

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            lab.HeroTryStep(Labirinth2019.Heroes.Direction.Left);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            lab.HeroTryStep(Labirinth2019.Heroes.Direction.Right);
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            lab.HeroTryStep(Labirinth2019.Heroes.Direction.Up);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            lab.HeroTryStep(Labirinth2019.Heroes.Direction.Down);
                            break;
                        }
                }

                Drawer.DrawLab(lab);
            } while (key.Key != ConsoleKey.Escape);

            Console.WriteLine("End");
            Console.ReadLine();
        }

        public static void Rec(int number)
        {
            number--;

            if (number > 0)
            {
                Rec(number);
            }

            Console.WriteLine(number);
        }
    }
}
