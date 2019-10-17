using LabirinthCore.Heroes;

namespace LabirinthCore.Labirinth.CellObject
{
    public class Ground : BaseCellObject
    {
        public Ground(int x, int y) : base(x, y, ' ') { }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            return true;
        }
    }
}