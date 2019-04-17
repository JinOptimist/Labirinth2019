using LabirinthCore.Heroes;

namespace LabirinthCore.Labirinth.CellObject
{
    public class Wall : BaseCellObject
    {
        public Wall(int x, int y) : base(x, y, '#', System.ConsoleColor.DarkGray) {
            DescAction = "Boom. Hey! There is wall here";
        }

        public override bool TryToStepHere(Dungeon dungeon)
        {
            return false;
        }
    }
}