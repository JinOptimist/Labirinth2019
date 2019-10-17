using LabirinthCore.Heroes;
using LabirinthCore.Labirinth.CellObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthCore.Labirinth
{
    public interface IDungeon
    {
        void GoDown();

        void GoUp();

        void HeroDoStep(Direction direction);

        void ReplaceToGround(BaseCellObject cellToStep);
    }
}
