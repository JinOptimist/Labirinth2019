using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth.CellObject
{
    public class StairsDown : BaseCellObject
    {
        public StairsDown(int x, int y) : base(x, y, '>') { }

        public override bool TryToStepHere(Hero hero)
        {
            return true;
        }
    }
}