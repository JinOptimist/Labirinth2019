using Labirinth2019.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Labirinth.Lab
{
    public class LabirinthGenerator
    {
        private Labirinth2019.Lab.Labirinth lab;
        private List<LabSell> BlueCell;
        private List<LabSell> OrangeCell;

        private Random _rand = new Random();

        public LabirinthGenerator(Labirinth2019.Lab.Labirinth labirinth)
        {
            lab = labirinth;
            BlueCell = lab.LabSells
                .SelectMany(row => row.Select(cell => cell)).ToList();
            OrangeCell = new List<LabSell>();
        }

        public void Step(LabSell yellowCell)
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

        private void BrokeTheWall(LabSell cell1, LabSell cell2)
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

        private LabSell GetRandomCell(List<LabSell> cells)
        {
            var index = _rand.Next(cells.Count);
            return cells[index];
        }

        private List<LabSell> GetNearBlueCells(LabSell yellowCell)
        {
            return BlueCell.Where(cell =>
            (cell.X == yellowCell.X && cell.Y == yellowCell.Y + 1)
                || (cell.X == yellowCell.X && cell.Y == yellowCell.Y - 1)
                || (cell.X == yellowCell.X + 1 && cell.Y == yellowCell.Y)
                || (cell.X == yellowCell.X - 1 && cell.Y == yellowCell.Y)
            ).ToList();
        }

        private List<LabSell> GetNearOrangeCells(LabSell yellowCell)
        {
            var nearCells = OrangeCell.Where(cell =>
            (cell.X == yellowCell.X && cell.Y == yellowCell.Y + 1)
                || (cell.X == yellowCell.X && cell.Y == yellowCell.Y - 1)
                || (cell.X == yellowCell.X + 1 && cell.Y == yellowCell.Y)
                || (cell.X == yellowCell.X - 1 && cell.Y == yellowCell.Y)
            ).ToList();

            var result = new List<LabSell>();

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
