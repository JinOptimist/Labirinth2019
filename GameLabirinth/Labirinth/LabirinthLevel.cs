using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth
{
    public class LabirinthLevel
    {
        public List<List<LabirinthCell>> LabCells { get; set; }
        public List<Coin> Coins { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Hero Hero { get; set; }
        public LabirinthCell this[int x, int y] {
            get
            {
                return LabCells[y][x];
            }
            set
            {
                LabCells[y][x] = value;
            }
        }
        private Random _random = new Random();

        public LabirinthLevel(int width, int height)
        {
            LabCells = new List<List<LabirinthCell>>();
            Width = width;
            Height = height;
            for (int y = 0; y < height; y++) {
                var row = new List<LabirinthCell>();
                for (int x = 0; x < width; x++) {
                    //row.Add(new LabSell(RandowWall(), x, y));
                    row.Add(new LabirinthCell(FullWall(), x, y));
                }
                LabCells.Add(row);
            }

            // Old way to generate random labirinth
            //for (int y = 0; y < height; y++) {
            //    for (int x = 0; x < width; x++) {
            //        BrokeTheWall(x, y);
            //    }
            //}

            Hero = new Hero(0, 0);

            Coins = new List<Coin>();
            for (int i = 0; i < 5; i++) {
                var x = _random.Next(Width);
                var y = _random.Next(Height);
                Coins.Add(new Coin(x, y));
            }
        }

        public bool HeroTryStep(Direction direction)
        {
            var cell = this[Hero.X, Hero.Y];
            var isAvailableDirection = false;
            switch (direction) {
                case Direction.Up: {
                        if (!cell.Wall.HasFlag(Wall.Up)) {
                            Hero.Y--;
                            isAvailableDirection = true;
                        }
                        break;
                    }
                case Direction.Right: {
                        if (!cell.Wall.HasFlag(Wall.Right)) {
                            Hero.X++;
                            isAvailableDirection = true;
                        }
                        break;
                    }
                case Direction.Down: {
                        if (!cell.Wall.HasFlag(Wall.Down)) {
                            Hero.Y++;
                            isAvailableDirection = true;
                        }
                        break;
                    }
                case Direction.Left: {
                        if (!cell.Wall.HasFlag(Wall.Left)) {
                            Hero.X--;
                            isAvailableDirection = true;
                        }
                        break;
                    }
                default: {
                        throw new Exception("New value of Direction enum");
                    }
            }
            var cointsInCell = Coins.Where(coin => coin.X == Hero.X && coin.Y == Hero.Y).ToList();
            Hero.Money += cointsInCell.Count();
            cointsInCell.ForEach(x => Coins.Remove(x));

            return isAvailableDirection;
        }

        private void BrokeTheWall(int x, int y)
        {
            var currentCell = this[x, y];
            var randNumber = _random.Next(10);
            if (randNumber > 3 && y < Height - 1) {
                currentCell.Wall &= ~Wall.Down;
                this[x, y + 1].Wall &= ~Wall.Up;
            }
            if ((randNumber % 2 == 0 || randNumber <= 3)
                && x < Width - 1) {
                currentCell.Wall &= ~Wall.Right;
                this[x + 1, y].Wall &= ~Wall.Left;
            }
        }

        private Wall FullWall()
        {
            return Wall.Up | Wall.Right | Wall.Down | Wall.Left;
        }

        private Wall RandowWall()
        {
            Wall wall = FullWall();
            var randNumber = _random.Next(10);
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
