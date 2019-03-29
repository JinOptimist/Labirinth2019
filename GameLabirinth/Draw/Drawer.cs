using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GameLabirinth.Heroes;
using GameLabirinth.Labirinth;
using GameLabirinth.Labirinth.CellObject;

namespace GameLabirinth
{
    public class Drawer
    {
        /// <summary>
        /// Draw rule, hero info and labirinth level
        /// </summary>
        /// <param name="dungeon">Pass dungeon that you want to draw</param>
        public static void DrawDungeon(Dungeon dungeon)
        {
            Console.Clear();
            var hero = Hero.GetHero;
            Console.WriteLine("Rule:");
            Console.WriteLine("1) Use arrow to move");
            Console.WriteLine("2) Press Esc to Exit");
            Console.Write($"Level: {dungeon.CurrentLevelNumber}");
            Console.Write($" Money: ");
            var color = Console.ForegroundColor;
            Console.ForegroundColor = Coin.CoinColor;
            Console.WriteLine(hero.Money);
            Console.ForegroundColor = color;
            Console.WriteLine(dungeon.DescLastAction);
            Console.WriteLine();

            DrawLabirinth(dungeon.CurrentLevel);
        }

        /// <summary>
        /// Draw only labirint level
        /// </summary>
        /// <param name="labirinth"></param>
        /// <param name="screenClear"></param>
        public static void DrawLabirinth(LabirinthLevel labirinth, bool screenClear = false)
        {
            if (screenClear) {
                Console.Clear();
            }

            var color = Console.ForegroundColor;
            var hero = Hero.GetHero;

            WriteWallLine(labirinth.Width + 2);

            for (int y = 0; y < labirinth.Height; y++)
            {
                Console.Write("#");
                var row = new List<BaseCellObject>();
                for (int x = 0; x < labirinth.Width; x++)
                {
                    var cell = labirinth[x, y];
                    if (!screenClear && !HeroCanSeeThis(cell))
                    {
                        Console.Write("~");
                        continue;
                    }

                    color = Console.ForegroundColor;
                    if (hero.X == x && hero.Y == y)
                    {
                        Console.ForegroundColor = hero.Color;
                        Console.Write(hero.Chapter);
                    }
                    else
                    {
                        Console.ForegroundColor = cell.Color;
                        Console.Write(cell.Chapter);
                    }

                    Console.ForegroundColor = color;
                }

                Console.Write("#");
                Console.WriteLine();
            }

            WriteWallLine(labirinth.Width + 2);
        }

        private static bool HeroCanSeeThis(BaseCellObject cellObject)
        {
            if (cellObject is StairsDown || cellObject is StairsUp)
            {
                return true;
            }

            var hero = Hero.GetHero;
            var distance = Math.Sqrt(Math.Pow(hero.X - cellObject.X, 2) + Math.Pow(hero.Y - cellObject.Y, 2));

            //return distance < 4;
            return true;
        }

        private static void WriteWallLine(int size)
        {
            for (var i = 0; i < size; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }
    }
}
