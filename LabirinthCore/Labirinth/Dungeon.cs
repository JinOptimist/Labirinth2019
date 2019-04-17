using LabirinthCore.Heroes;
using LabirinthCore.Labirinth.CellObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthCore.Labirinth
{
    public class Dungeon
    {
        public LabirinthLevel CurrentLevel { get; private set; }
        public int CurrentLevelNumber { get; private set; } = -1;

        private List<LabirinthLevel> Levels { get; set; }
        private LabirinthGenerator Generator { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public string DescLastAction { get; set; }

        public Dungeon(bool showLabGeneration, int width = 10, int height = 5)
        {
            Width = width;
            Height = height;
            Generator = new LabirinthGenerator(width, height, showLabGeneration: showLabGeneration);
            Levels = new List<LabirinthLevel>();
            GoDown();
        }

        public void GoDown()
        {
            if (CurrentLevelNumber + 1 < Levels.Count)
            {
                CurrentLevel = Levels[++CurrentLevelNumber];
                return;
            }

            var hero = Hero.GetHero;
            CurrentLevel = Generator.GenerateLevel(hero.X, hero.Y, CurrentLevelNumber);
            Levels.Add(CurrentLevel);
            CurrentLevelNumber++;
        }

        public void GoUp()
        {
            if (CurrentLevelNumber == 0)
            {
                return;
            }

            CurrentLevel = Levels[--CurrentLevelNumber];
        }

        /// <summary>
        /// Get direction of hero step. Apply all nessary changes
        /// </summary>
        /// <param name="direction">Direction for hero</param>
        public void HeroDoStep(Direction direction)
        {
            var hero = Hero.GetHero;
            var x = hero.X;
            var y = hero.Y;

            BaseCellObject cellToStep;
            switch (direction)
            {
                case Direction.Up:
                    {
                        cellToStep = CurrentLevel[x, y - 1];
                        break;
                    }
                case Direction.Right:
                    {
                        cellToStep = CurrentLevel[x + 1, y];
                        break;
                    }
                case Direction.Down:
                    {
                        cellToStep = CurrentLevel[x, y + 1];
                        break;
                    }
                case Direction.Left:
                    {
                        cellToStep = CurrentLevel[x - 1, y];
                        break;
                    }
                default:
                    {
                        throw new Exception("New value of Direction enum");
                    }
            }
            ApplyCellAction(cellToStep);
        }

        private void ApplyCellAction(BaseCellObject cellToStep)
        {
            var hero = Hero.GetHero;
            if (cellToStep?.TryToStepHere(this) ?? false)
            {
                hero.X = cellToStep.X;
                hero.Y = cellToStep.Y;
            }

            if (cellToStep?.CallAfterStep != null)
            {
                cellToStep.CallAfterStep();
            }

            DescLastAction = cellToStep?.DescAction;
        }

        public void ReplaceToGround(BaseCellObject cellToStep)
        {
            CurrentLevel[cellToStep.X, cellToStep.Y] = new Ground(cellToStep.X, cellToStep.Y);
        }
    }
}
