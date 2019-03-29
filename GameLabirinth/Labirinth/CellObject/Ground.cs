using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth.CellObject
{
    public class Ground : BaseCellObject
    {
        public Ground(int x, int y) : base(x, y, ' ') { }

        public override bool TryToStepHere(Dungeon dungeon)
        {
            return true;
        }
    }
}