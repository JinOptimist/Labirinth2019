using System;
using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth.CellObject
{
    public abstract class BaseCellObject
    {
        public BaseCellObject() { }

        public BaseCellObject(int x, int y, char chapter, ConsoleColor color = ConsoleColor.White)
        {
            X = x;
            Y = y;
            Chapter = chapter;
            Color = color;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public char Chapter { get; set; }
        public ConsoleColor Color { get; set; }
        public string DescAction { get; set; }

        public abstract bool TryToStepHere(Dungeon dungeon);
    }
}