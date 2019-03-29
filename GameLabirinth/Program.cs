﻿using System;
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
            
            var showLabGeneration = args.Any();
            var dungeon = new Dungeon(showLabGeneration, 8, 5);

            Drawer.DrawDungeon(dungeon);

            ConsoleKeyInfo key;
            do {
                key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.LeftArrow: {
                            dungeon.HeroDoStep(Heroes.Direction.Left);
                            break;
                        }
                    case ConsoleKey.RightArrow: {
                            dungeon.HeroDoStep(Heroes.Direction.Right);
                            break;
                        }
                    case ConsoleKey.UpArrow: {
                            dungeon.HeroDoStep(Heroes.Direction.Up);
                            break;
                        }
                    case ConsoleKey.DownArrow: {
                            dungeon.HeroDoStep(Heroes.Direction.Down);
                            break;
                        }
                }

                Drawer.DrawDungeon(dungeon);
            } while (key.Key != ConsoleKey.Escape);

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
