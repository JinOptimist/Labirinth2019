﻿namespace Labirinth2019.Lab
{
    public class Coin
    {
        public Coin() { }

        public Coin(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public const char Chapter = '©';
    }
}