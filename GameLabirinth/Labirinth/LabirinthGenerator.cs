using GameLabirinth.Heroes;
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
        private LabirinthLevel lab;
        private List<LabirinthCell> BlueCell;
        private List<LabirinthCell> OrangeCell;

        private Random _rand = new Random();

        public LabirinthGenerator(LabirinthLevel labirinth)
        {
            lab = labirinth;
            BlueCell = lab.LabCells
                .SelectMany(row => row.Select(cell => cell)).ToList();
            OrangeCell = new List<LabirinthCell>();
        }

        public void BrokeWallsInTheLabirinth()
        {
            Step(lab[0, 0]);
        }

        private void Step(LabirinthCell yellowCell)
        {
            //Drawer.DrawLab(lab);
            //Thread.Sleep(100);

            var nearBlueCells = GetNearBlueCells(yellowCell);
            if (nearBlueCells.Any())
            {
                var nextCell = GetRandomCell(nearBlueCells);
                BrokeTheWall(yellowCell, nextCell);
                BlueCell.Remove(nextCell);
                OrangeCell.Add(nextCell);
                Step(nextCell);
                return;
            }

            var nearOrangeCells = GetNearOrangeCells(yellowCell);
            if (nearOrangeCells.Any())
            {
                var nextCell = GetRandomCell(nearOrangeCells);
                OrangeCell.Remove(nextCell);
                Step(nextCell);
                return;
            }

            if (OrangeCell.Any())
            {
                var nextCell = GetRandomCell(OrangeCell);
                OrangeCell.Remove(nextCell);
                Step(nextCell);
                return;
            }
        }

        private void BrokeTheWall(LabirinthCell cell1, LabirinthCell cell2)
        {
            if (cell1.X > cell2.X)
            {
                cell1.Wall &= ~Wall.Left;
                cell2.Wall &= ~Wall.Right;
            }
            else if (cell1.X < cell2.X)
            {
                cell1.Wall &= ~Wall.Right;
                cell2.Wall &= ~Wall.Left;
            }
            else
            {
                if (cell1.Y > cell2.Y)
                {
                    cell1.Wall &= ~Wall.Up;
                    cell2.Wall &= ~Wall.Down;
                }
                else
                {
                    cell1.Wall &= ~Wall.Down;
                    cell2.Wall &= ~Wall.Up;
                }
            }
        }

        private LabirinthCell GetRandomCell(List<LabirinthCell> cells)
        {
            var index = _rand.Next(cells.Count);
            return cells[index];
        }

        private List<LabirinthCell> GetNearBlueCells(LabirinthCell yellowCell)
        {
            return BlueCell.Where(cell =>
            (cell.X == yellowCell.X && cell.Y == yellowCell.Y + 1)
                || (cell.X == yellowCell.X && cell.Y == yellowCell.Y - 1)
                || (cell.X == yellowCell.X + 1 && cell.Y == yellowCell.Y)
                || (cell.X == yellowCell.X - 1 && cell.Y == yellowCell.Y)
            ).ToList();
        }

        private List<LabirinthCell> GetNearOrangeCells(LabirinthCell yellowCell)
        {
            var nearCells = OrangeCell.Where(cell =>
            (cell.X == yellowCell.X && cell.Y == yellowCell.Y + 1)
                || (cell.X == yellowCell.X && cell.Y == yellowCell.Y - 1)
                || (cell.X == yellowCell.X + 1 && cell.Y == yellowCell.Y)
                || (cell.X == yellowCell.X - 1 && cell.Y == yellowCell.Y)
            ).ToList();

            var result = new List<LabirinthCell>();

            foreach (var nearCell in nearCells)
            {
                if (nearCell.X > yellowCell.X
                    && !yellowCell.Wall.HasFlag(Wall.Right))
                {
                    result.Add(nearCell);
                }
                else if (nearCell.X < yellowCell.X
                    && !yellowCell.Wall.HasFlag(Wall.Left))
                {
                    result.Add(nearCell);
                }
                else
                {
                    if (nearCell.Y < yellowCell.Y
                        && !yellowCell.Wall.HasFlag(Wall.Up))
                    {
                        result.Add(nearCell);
                    }
                    else if(nearCell.Y > yellowCell.Y
                        && !yellowCell.Wall.HasFlag(Wall.Down))
                    {
                        result.Add(nearCell);
                    }
                }
            }
            return result;
        }
    }
}
