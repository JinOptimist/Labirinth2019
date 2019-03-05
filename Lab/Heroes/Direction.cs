using System;
using System.Collections.Generic;
using System.Text;

namespace Labirinth2019.Heroes
{
    [Flags]
    public enum Direction
    {
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }
}
