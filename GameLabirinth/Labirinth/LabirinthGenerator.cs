using GameLabirinth.Heroes;
using GameLabirinth.Labirinth.CellObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameLabirinth.Labirinth
{
    public class LabirinthGenerator
    {
        private int Width;
        private int Height;
        private int ChanseOfCoin;
        private bool ShowLabGeneration;
        private LabirinthLevel LabLevel;
        private List<Wall> WallsToDemolish = new List<Wall>();
        private List<Wall> FinishedWalls = new List<Wall>();

        private Random _rand = new Random();

        public LabirinthGenerator(int width, int height, int chanseOfCoin = 20, bool showLabGeneration = false)
        {
            Width = width;
            Height = height;
            ChanseOfCoin = chanseOfCoin;
            ShowLabGeneration = showLabGeneration;
        }

        /// <summary>
        /// Generate new labirinth level
        /// </summary>
        /// <param name="stairsX">X Coordinate for stairs to up</param>
        /// <param name="stairsY">Y Coordinate for stairs to up</param>
        /// <returns></returns>
        public LabirinthLevel GenerateLevel(int stairsX = 0, int stairsY = 0)
        {

            LabLevel = new LabirinthLevel(Width, Height);
            for (int y = 0; y < LabLevel.Height; y++)
            {
                var row = new List<BaseCellObject>();
                for (int x = 0; x < LabLevel.Width; x++)
                {
                    var wall = new Wall(x, y);
                    row.Add(wall);
                }
                LabLevel.Cells.Add(row);
            }

            GeneratePathes(stairsX, stairsY);

            GenerateCoins(stairsX, stairsY);

            var cell = GetRandom(
                    LabLevel.AllCells()
                    .OfType<Ground>()
                    .Where(groundCell =>
                        GetNearCells(groundCell)
                        .Where(x => !(x is Wall))
                        .Count() == 1).ToList()
                );
            LabLevel[cell.X, cell.Y] = new StairsDown(cell.X, cell.Y);

            return LabLevel;
        }

        private void GeneratePathes(int stairsX, int stairsY)
        {
            BreakTheWall((Wall)LabLevel[stairsX, stairsY]);
            while (WallsToDemolish.Any())
            {
                if (ShowLabGeneration)
                {
                    Drawer.DrawLabirinth(LabLevel, true);
                    Thread.Sleep(100);
                }

                var wall = GetRandom(WallsToDemolish);
                if (CanBreakTheWall(wall))
                {
                    BreakTheWall(wall);
                }
                else
                {
                    WallsToDemolish.Remove(wall);
                }
            }

            LabLevel[stairsX, stairsY] = new StairsUp(stairsX, stairsY);
        }

        private void GenerateCoins(int stairsX, int stairsY)
        {
            var grounds = LabLevel.Cells.SelectMany(row => row.Select(c => c).Where(c => c is Ground
                // coin at stairs coordinate it is bad. We want ignore start location
                && (c.X != stairsX || c.Y != stairsY))).ToList();
            for (int i = 0; i < grounds.Count(); i++)
            {
                var cell = grounds[i];
                if (_rand.Next(100) > 100 - ChanseOfCoin)
                {
                    LabLevel[cell.X, cell.Y] = new Coin(cell.X, cell.Y);
                    if (ShowLabGeneration)
                    {
                        Drawer.DrawLabirinth(LabLevel, true);
                        Thread.Sleep(100);
                    }
                }
            }
        }

        private void BreakTheWall(Wall wall)
        {
            var x = wall.X;
            var y = wall.Y;
            LabLevel[x, y] = new Ground(x, y);
            WallsToDemolish.RemoveAll(w => w.X == x && w.Y == y);

            var cell = LabLevel[x - 1, y] as Wall;
            if (CanBreakTheWall(cell))
            {
                WallsToDemolish.Add(cell);
            }
            cell = LabLevel[x + 1, y] as Wall;
            if (CanBreakTheWall(cell))
            {
                WallsToDemolish.Add(cell);
            }
            cell = LabLevel[x, y - 1] as Wall;
            if (CanBreakTheWall(cell))
            {
                WallsToDemolish.Add(cell);
            }
            cell = LabLevel[x, y + 1] as Wall;
            if (CanBreakTheWall(cell))
            {
                WallsToDemolish.Add(cell);
            }
        }

        private bool CanBreakTheWall(Wall wall)
        {
            if (wall == null)
            {
                return false;
            }
            if (GetNearCells(wall).Count(x => !(x is Wall)) > 1)
            {
                return false;
            }

            return true;
        }

        private T GetRandom<T>(List<T> cells)
        {
            var index = _rand.Next(cells.Count);
            return cells[index];
        }

        private List<BaseCellObject> GetNearCells(BaseCellObject cell)
        {
            var near = new List<BaseCellObject>();
            near.Add(LabLevel[cell.X + 1, cell.Y]);
            near.Add(LabLevel[cell.X - 1, cell.Y]);
            near.Add(LabLevel[cell.X, cell.Y + 1]);
            near.Add(LabLevel[cell.X, cell.Y - 1]);
            return near.Where(x => x != null).ToList();
        }
    }
}
