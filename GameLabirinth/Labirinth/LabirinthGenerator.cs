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
        private LabirinthLevel Lab;
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

        public LabirinthLevel Generate(Hero hero = null)
        {
            if (hero == null) {
                hero = new Hero(0, 0);
            }

            Lab = new LabirinthLevel(Width, Height, hero);
            for (int y = 0; y < Lab.Height; y++) {
                var row = new List<BaseCellObject>();
                for (int x = 0; x < Lab.Width; x++) {
                    var wall = new Wall(x, y);
                    row.Add(wall);
                }
                Lab.Cells.Add(row);
            }

            GeneratePathes();

            GenerateCoins();

            return Lab;
        }

        private void GeneratePathes()
        {
            BreakTheWall((Wall)Lab[0, 0]);
            while (WallsToDemolish.Any()) {
                if (ShowLabGeneration) {
                    Drawer.DrawLab(Lab);
                    Thread.Sleep(100);
                }
                

                var wall = GetRandomWall(WallsToDemolish);
                if (CanBreakTheWall(wall)) {
                    BreakTheWall(wall);
                } else {
                    WallsToDemolish.Remove(wall);
                }
            }
        }

        private void GenerateCoins()
        {
            var grounds = Lab.Cells.SelectMany(row => row.Select(c => c).Where(c => c is Ground
                // coin at [0,0] coordinate it is bad. We want ignore start location
                && (c.X != 0 || c.Y != 0))).ToList();
            for (int i = 0; i < grounds.Count(); i++) {
                var cell = grounds[i];
                if (_rand.Next(100) > 100 - ChanseOfCoin) {
                    Lab[cell.X, cell.Y] = new Coin(cell.X, cell.Y);
                    if (ShowLabGeneration) {
                        Drawer.DrawLab(Lab);
                        Thread.Sleep(100);
                    }
                }
            }
        }

        private void BreakTheWall(Wall wall)
        {
            var x = wall.X;
            var y = wall.Y;
            Lab[x, y] = new Ground(x, y);
            WallsToDemolish.RemoveAll(w => w.X == x && w.Y == y);

            var cell = Lab[x - 1, y] as Wall;
            if (CanBreakTheWall(cell)) {
                WallsToDemolish.Add(cell);
            }
            cell = Lab[x + 1, y] as Wall;
            if (CanBreakTheWall(cell)) {
                WallsToDemolish.Add(cell);
            }
            cell = Lab[x, y - 1] as Wall;
            if (CanBreakTheWall(cell)) {
                WallsToDemolish.Add(cell);
            }
            cell = Lab[x, y + 1] as Wall;
            if (CanBreakTheWall(cell)) {
                WallsToDemolish.Add(cell);
            }
        }

        private bool CanBreakTheWall(Wall wall)
        {
            if (wall == null) {
                return false;
            }
            if (GetNearCells(wall).Count(x => !(x is Wall)) > 1) {
                return false;
            }

            return true;
        }

        private Wall GetRandomWall(List<Wall> cells)
        {
            var index = _rand.Next(cells.Count);
            return cells[index];
        }

        private List<BaseCellObject> GetNearCells(BaseCellObject cell)
        {
            var near = new List<BaseCellObject>();
            near.Add(Lab[cell.X + 1, cell.Y]);
            near.Add(Lab[cell.X - 1, cell.Y]);
            near.Add(Lab[cell.X, cell.Y + 1]);
            near.Add(Lab[cell.X, cell.Y - 1]);
            return near.Where(x => x != null).ToList();
        }

    }
}
