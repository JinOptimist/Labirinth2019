using System;
using System.Collections.Generic;
using System.Text;

namespace GameLabirinth.Heroes
{
    public class Hero
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Money { get; set; }
        public const char Chapter = '@';

        public Hero(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
