using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthCore.Labirinth
{
    public interface ILabirinthGenerator
    {
        LabirinthLevel GenerateLevel(int stairsX = 0, int stairsY = 0, int levelNumber = 0);
    }
}
