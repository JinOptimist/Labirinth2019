using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabirinthCore.Heroes;
using LabirinthCore.Labirinth.CellObject;

namespace LabirinthCore.Labirinth
{
    public class LabirinthLevel
    {
        public List<List<BaseCellObject>> Cells { get; set; } = new List<List<BaseCellObject>>();
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        public BaseCellObject this[int x, int y] {
            get
            {
                if (x < 0 || y < 0
                    || x > Width - 1 || y > Height - 1)
                {
                    return null;
                }

                return Cells[y][x];
            }

            set
            {
                Cells[y][x] = value;
            }
        }

        public List<BaseCellObject> AllCells()
        {
            return Cells.SelectMany(x => x).ToList();
        }

        private Random _random = new Random();

        public LabirinthLevel()
        {

        }

        public LabirinthLevel(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
