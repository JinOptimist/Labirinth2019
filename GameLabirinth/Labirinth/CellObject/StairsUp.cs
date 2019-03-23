using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth.CellObject
{
    public class StairsUp : BaseCellObject
    {
        public StairsUp(int x, int y) : base(x, y, '<') { }

        public override bool TryToStepHere(Hero hero)
        {
            return true;
        }
    }
}