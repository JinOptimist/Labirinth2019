using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GameLabirinth.Heroes;
using GameLabirinth.Labirinth;

namespace GameLabirinth
{
    public class Drawer
    {
        public static void DrawLab(LabirinthLevel labirinth)
        {
            Console.Clear();
            Console.WriteLine($"Money: {labirinth.Hero.Money}");

            Console.WriteLine();
            for (int x = 0; x < labirinth.Width * 2 + 1; x++) {
                Console.Write("_");
            }
            Console.WriteLine();

            for (int y = 0; y < labirinth.Height; y++) {
                var row = new List<LabirinthCell>();
                Console.Write("|");
                for (int x = 0; x < labirinth.Width; x++) {
                    var cell = labirinth[x, y];

                    if (labirinth.Hero.X == x
                        && labirinth.Hero.Y == y) {
                        Console.Write(Hero.Chapter);
                    } else if (labirinth.Coins.Any(coin=>coin.X == x && coin.Y == y)) {
                        Console.Write(Coin.Chapter);
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
