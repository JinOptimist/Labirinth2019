using GameLabirinth.Heroes;
using System;

namespace GameLabirinth.Labirinth.CellObject
{
    public class Coin : BaseCellObject
    {
        public const ConsoleColor CoinColor = ConsoleColor.Yellow;
        public Coin(int x, int y) : base(x, y, '©', CoinColor) {
            DescAction = "Here! I found coin";
        }

        

        public override bool TryToStepHere(Hero hero)
        {
            hero.Money++;
            return true;
        }
    }
}