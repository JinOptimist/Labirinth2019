using System;
using System.Collections.Generic;
using System.Text;

namespace Labirinth2019.Lab
{
    [Flags]
    public enum Wall
    {
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }
}
