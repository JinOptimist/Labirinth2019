using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabirinthCore.Labirinth;

namespace GameLabirinth
{
    class Program
    {
        static void Main(string[] args)
        {
            var dungeon = new Dungeon(7, 9, x => Drawer.DrawLabirinth(x, true));
            DungeonConsoleManager.Start(dungeon);
        }
    }
}
