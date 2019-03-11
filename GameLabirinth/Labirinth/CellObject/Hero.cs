using System;
using System.Collections.Generic;
using System.Text;

namespace GameLabirinth.Labirinth.CellObject
{
    public class Hero : BaseCellObject
    {
        public int Money { get; set; }

        public Hero(int x, int y) : base(x, y, '@') { }

        public override bool TryToStepHere(Hero hero)
        {
            return false;
        }
    }
}
