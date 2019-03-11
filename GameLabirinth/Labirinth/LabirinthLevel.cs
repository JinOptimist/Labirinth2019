using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLabirinth.Heroes;
using GameLabirinth.Labirinth.CellObject;

namespace GameLabirinth.Labirinth
{
    public class LabirinthLevel
    {
        public List<List<BaseCellObject>> Cells { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Hero Hero { get; set; }
        public BaseCellObject this[int x, int y] {
            get
            {
                if (x < 0 || y < 0
                    || x > Width - 1 || y > Height - 1) {
                    return null;
                }

                return Cells[y][x];
            }
            set
            {
                Cells[y][x] = value;
            }
        }

        private Random _random = new Random();

        public LabirinthLevel(int width, int height, Hero hero)
        {
            Cells = new List<List<BaseCellObject>>();
            Width = width;
            Height = height;
            Hero = hero;
        }

        public void HeroDoStep(Direction direction)
        {
            var x = Hero.X;
            var y = Hero.Y;

            BaseCellObject cellToStep;
            switch (direction) {
                case Direction.Up: {
                        cellToStep = this[x, y - 1];
                        break;
                    }
                case Direction.Right: {
                        cellToStep = this[x + 1, y];
                        break;
                    }
                case Direction.Down: {
                        cellToStep = this[x, y + 1];
                        break;
                    }
                case Direction.Left: {
                        cellToStep = this[x - 1, y];
                        break;
                    }
                default: {
                        throw new Exception("New value of Direction enum");
                    }
            }
            if (cellToStep?.TryToStepHere(Hero) ?? false) {
                Hero.X = cellToStep.X;
                Hero.Y = cellToStep.Y;

                if (cellToStep is Coin) {
                    this[cellToStep.X, cellToStep.Y] = new Ground(cellToStep.X, cellToStep.Y);
                }
            }
        }
    }
}
