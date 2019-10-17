using LabirinthCore.Heroes;
using LabirinthCore.Labirinth.CellObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabirinthCore.Labirinth
{
    public class LabirinthGenerator : ILabirinthGenerator
    {
        private int Width;
        private int Height;
        private int BaseChanseOfCoin;
        private bool ShowLabGeneration;
        private LabirinthLevel LabLevel;
        private List<Wall> WallsToDemolish = new List<Wall>();
        private List<Wall> FinishedWalls = new List<Wall>();

        private Random _rand = new Random();

        public LabirinthGenerator(int width, int height, int chanseOfCoin = 20, bool showLabGeneration = false)
        {
            Width = width;
            Height = height;
            BaseChanseOfCoin = chanseOfCoin;
            ShowLabGeneration = showLabGeneration;
        }

        /// <summary>
        /// Generate new labirinth level
        /// </summary>
        /// <param name="stairsX">X Coordinate for stairs to up</param>
        /// <param name="stairsY">Y Coordinate for stairs to up</param>
        /// <returns></returns>
        public LabirinthLevel GenerateLevel(int stairsX = 0, int stairsY = 0, int levelNumber = 0)
        {
            if (levelNumber % 2 == 0)
            {
                Width++;
            }
            if (levelNumber % 3 == 0)
            {
                Height++;
            }

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

            GenerateCoins(levelNumber);

            GenerateGoldmine(levelNumber);

            var deadEnd = GetRandom(
                    LabLevel.AllCells()
                    .Where(x => x is Ground || x is Coin)
                    .Where(groundCell =>
                        GetNearCells(groundCell)
                        .Where(x => !(x is Wall))
                        .Count() == 1).ToList()
                );
            if (deadEnd == null)
            {
                deadEnd = GetRandom(LabLevel.AllCells().Where(x => x is Ground || x is Coin).ToList());
            }

            LabLevel[deadEnd.X, deadEnd.Y] = new StairsDown(deadEnd.X, deadEnd.Y);

            return LabLevel;
        }

        private void GeneratePathes(int stairsX, int stairsY)
        {
            BreakTheWall((Wall)LabLevel[stairsX, stairsY]);
            while (WallsToDemolish.Any())
            {
                RedrawLevel();

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

        private void GenerateCoins(int levelNumber)
        {
            var grounds = LabLevel.Cells.SelectMany(row => row.Select(c => c).Where(c => c is Ground)).ToList();
            for (int i = 0; i < grounds.Count(); i++)
            {
                var cell = grounds[i];
                if (_rand.Next(100) > 100 - (BaseChanseOfCoin + levelNumber))
                {
                    LabLevel[cell.X, cell.Y] = new Coin(cell.X, cell.Y);
                    RedrawLevel();
                }
            }
        }

        private void GenerateGoldmine(int levelNumber)
        {
            var walls = LabLevel.Cells.SelectMany(row => row.Select(c => c).Where(c => c is Wall)).ToList();
            for (int i = 0; i < walls.Count(); i++)
            {
                var cell = walls[i];
                if (_rand.Next(100) > 100 - ((BaseChanseOfCoin + levelNumber) / 2))
                {
                    LabLevel[cell.X, cell.Y] = new Goldmine(cell.X, cell.Y);
                    RedrawLevel();
                }
            }
        }

        private void RedrawLevel()
        {
            if (ShowLabGeneration)
            {
                //Drawer.DrawLabirinth(LabLevel, true);
                Thread.Sleep(100);
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
            if (cells.Count == 0)
            {
                return default(T);
            }

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
