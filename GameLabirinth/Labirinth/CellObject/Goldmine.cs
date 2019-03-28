using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth.CellObject
{
    public class Goldmine : BaseCellObject
    {
        public int Money { get; private set; } = 3;

        public Goldmine(int x, int y) : base(x, y, '#', System.ConsoleColor.DarkYellow)
        {
            DescAction = "Money, money, money...";
        }

        public override bool TryToStepHere(Hero hero)
        {
            if (Money > 0)
            {
                hero.Money++;
                Money--;
                if (Money == 0)
                {
                    DescAction = "This was last coin. This is sad";
                }
            }

            return false;
        }
    }
}