using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LabirinthCore.Heroes;
using LabirinthCore.Labirinth;
using LabirinthCore.Labirinth.CellObject;
using GameLabirinth.Draw;

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
            var builder = new ConsoleStringBuilder();
            Console.Clear();
            var hero = Hero.GetHero;
            builder.AppendLine("Rule:");
            builder.AppendLine("1) Use arrow to move");
            builder.AppendLine("2) Press Esc to Exit");
            builder.Append($"Level: {dungeon.CurrentLevelNumber}");
            builder.Append($" Money: ");
            builder.AppendLine(hero.Money.ToString(), Coin.CoinColor);
            builder.AppendLine(dungeon.DescLastAction);
            builder.AppendLine();

            DrawLabirinth(dungeon.CurrentLevel, builder: builder);
        }

        /// <summary>
        /// Draw only labirint level
        /// </summary>
        /// <param name="labirinth"></param>
        /// <param name="screenClear"></param>
        public static void DrawLabirinth(LabirinthLevel labirinth, bool screenClear = false, ConsoleStringBuilder builder = null)
        {
            if (builder == null)
            {
                builder = new ConsoleStringBuilder();
            }

            if (screenClear) {
                Console.Clear();
            }

            var hero = Hero.GetHero;

            WriteWallLine(labirinth.Width + 2, builder);

            for (int y = 0; y < labirinth.Height; y++)
            {
                builder.Append("#");
                var row = new List<BaseCellObject>();
                for (int x = 0; x < labirinth.Width; x++)
                {
                    var cell = labirinth[x, y];
                    //if (!screenClear && !HeroCanSeeThis(cell))
                    //{
                    //    builder.Append("~");
                    //    continue;
                    //}

                    if (hero.X == x && hero.Y == y)
                    {
                        builder.Append(hero.Chapter, hero.Color);
                    }
                    else
                    {
                        builder.Append(cell.Chapter, cell.Color);
                    }
                }

                builder.Append("#");
                builder.AppendLine();
            }

            WriteWallLine(labirinth.Width + 2, builder);

            builder.WriteToConsole();
        }

        private static bool HeroCanSeeThis(BaseCellObject cellObject)
        {
            if (cellObject is StairsDown || cellObject is StairsUp)
            {
                return true;
            }

            var hero = Hero.GetHero;
            var distance = Math.Sqrt(Math.Pow(hero.X - cellObject.X, 2) + Math.Pow(hero.Y - cellObject.Y, 2));

            return distance < 4;
            //return true;
        }

        private static void WriteWallLine(int size, ConsoleStringBuilder builder)
        {
            for (var i = 0; i < size; i++)
            {
                builder.Append("#");
            }
            builder.AppendLine();
        }
    }
}
