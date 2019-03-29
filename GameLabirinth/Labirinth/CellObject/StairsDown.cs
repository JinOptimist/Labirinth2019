using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth.CellObject
{
    public class StairsDown : BaseCellObject
    {
        public StairsDown(int x, int y) : base(x, y, '>') {
            DescAction = "I go down!";
        }

        public override bool TryToStepHere(Dungeon dungeon)
        {
            dungeon.GoDown();
            return true;
        }
    }
}