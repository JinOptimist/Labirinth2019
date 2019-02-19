using System;
using System.Collections.Generic;
using System.Text;

namespace Labirinth2019.Lab
{
    public class LabSell
    {
        public LabSell(Wall wall, int x, int y)
        {
            Wall = wall;
            X = x;
            Y = y;
        }

        public Wall Wall { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

    }
}
