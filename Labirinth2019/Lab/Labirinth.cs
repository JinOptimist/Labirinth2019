using System;
using System.Collections.Generic;
using System.Text;
using Labirinth2019.Heroes;

namespace Labirinth2019.Lab
{
    public class Labirinth
    {
        public List<List<LabSell>> LabSells { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Hero Hero { get; set; }
        public LabSell this[int x, int y] {
            get
            {
                return LabSells[y][x];
            }
            set
            {
                LabSells[y][x] = value;
            }
        }

        public Labirinth(int width, int height)
        {
            LabSells = new List<List<LabSell>>();
            Width = width;
            Height = height;
            for (int y = 0; y < height; y++) {
                var row = new List<LabSell>();
                for (int x = 0; x < width; x++) {
                    row.Add(new LabSell(RandowWall(), x, y));
                }
                LabSells.Add(row);
            }

            Hero = new Hero(0, 0);
        }

        public bool HeroTryStep(Direction direction)
        {
            var cell = this[Hero.X, Hero.Y];
            switch (direction) {
                case Direction.Up: {
                        if (!cell.Wall.HasFlag(Wall.Up)) {
                            Hero.Y--;
                            return true;
                        }
                        return false;
                    }
                case Direction.Right: {
                        if (!cell.Wall.HasFlag(Wall.Right)) {
                            Hero.X++;
                            return true;
                        }
                        return false;
                    }
                case Direction.Down: {
                        if (!cell.Wall.HasFlag(Wall.Down)) {
                            Hero.Y++;
                            return true;
                        }
                        return false;
                    }
                case Direction.Left: {
                        if (!cell.Wall.HasFlag(Wall.Left)) {
                            Hero.X--;
                            return true;
                        }
                        return false;
                    }
            }

            throw new Exception("New value of Direction enum");
        }

        private Random _rand = new Random();

        private Wall FullWall()
        {
            return Wall.Up | Wall.Right | Wall.Down | Wall.Left;
        }

        private Wall RandowWall()
        {
            Wall wall = FullWall();
            var randNumber = _rand.Next(10);
            if (randNumber > 3) {
                wall &= ~Wall.Down;
            }
            if (randNumber % 2 == 0
                || randNumber <= 3) {
                wall &= ~Wall.Right;
            }

            return wall;
        }
    }
}
