using System;
using System.Collections.Generic;
using System.Text;
using Labirinth2019.Lab;
using Labirinth2019.Heroes;

namespace Labirinth2019
{
    public class Drawer
    {
        public static void DrawLab(Labirinth labirinth)
        {
            Console.Clear();
            Console.WriteLine();
            for (int x = 0; x < labirinth.Width * 2 + 1; x++) {
                Console.Write("_");
            }
            Console.WriteLine();

            for (int y = 0; y < labirinth.Height; y++) {
                var row = new List<LabSell>();
                Console.Write("|");
                for (int x = 0; x < labirinth.Width; x++) {
                    var cell = labirinth[x, y];

                    if (labirinth.Hero.X == x
                        && labirinth.Hero.Y == y) {
                        Console.Write(Hero.Chapter);
                    } else {
                        if (cell.Wall.HasFlag(Wall.Down)) {
                            Console.Write("_");
                        } else {
                            Console.Write(" ");
                        }
                    }
                   

                    if (cell.Wall.HasFlag(Wall.Right)) {
                        Console.Write("|");
                    } else {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
