using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth.CellObject
{
    public class Wall : BaseCellObject
    {
        public Wall(int x, int y) : base(x, y, '#', System.ConsoleColor.DarkGray) { }

        public override bool TryToStepHere(Hero hero)
        {
            return false;
        }
    }
}