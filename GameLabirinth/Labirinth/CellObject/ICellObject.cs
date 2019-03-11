using GameLabirinth.Heroes;

namespace GameLabirinth.Labirinth.CellObject
{
    public interface ICellObject
    {
        /// <summary>
        /// Change hero if it needed
        /// </summary>
        /// <param name="hero"></param>
        /// <returns>return fales if hero can not step to current object</returns>
        bool TryToStepHere(Hero hero);
    }
}