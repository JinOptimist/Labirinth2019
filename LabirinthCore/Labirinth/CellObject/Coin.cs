using LabirinthCore.Heroes;
using System;

namespace LabirinthCore.Labirinth.CellObject
{
    public class Coin : BaseCellObject
    {
        public const ConsoleColor CoinColor = ConsoleColor.Yellow;
        public Coin(int x, int y) : base(x, y, '0', CoinColor) {
            DescAction = "Here! I found coin";
        }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            dungeon.ReplaceToGround(this);
            Hero.GetHero.Money++;
            return true;
        }
    }
}