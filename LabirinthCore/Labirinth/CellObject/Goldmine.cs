using LabirinthCore.Heroes;

namespace LabirinthCore.Labirinth.CellObject
{
    public class Goldmine : BaseCellObject
    {
        public int Money { get; private set; } = 3;

        public Goldmine(int x, int y) : base(x, y, '#', System.ConsoleColor.DarkYellow)
        {
            DescAction = "Money, money, money...";
        }

        public override bool TryToStepHere(Dungeon dungeon)
        {
            if (Money > 0)
            {
                Hero.GetHero.Money++;
                Money--;
                if (Money == 0)
                {
                    dungeon.ReplaceToGround(this);
                    DescAction = "This was last coin. This is sad";
                    return true;
                }
            }

            return false;
        }
    }
}