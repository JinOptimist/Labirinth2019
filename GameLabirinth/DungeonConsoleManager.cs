using LabirinthCore.Heroes;
using LabirinthCore.Labirinth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLabirinth
{
    class DungeonConsoleManager
    {
        public static void Start(Dungeon dungeon)
        {
            Drawer.DrawDungeon(dungeon);

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            dungeon.HeroDoStep(Direction.Left);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            dungeon.HeroDoStep(Direction.Right);
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            dungeon.HeroDoStep(Direction.Up);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            dungeon.HeroDoStep(Direction.Down);
                            break;
                        }
                }

                Drawer.DrawDungeon(dungeon);
            } while (key.Key != ConsoleKey.Escape);

        }
    }
}
