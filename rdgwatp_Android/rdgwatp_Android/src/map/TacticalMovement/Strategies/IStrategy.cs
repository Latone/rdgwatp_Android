using System.Collections.Generic;
using ConsoleApp1.src.creatures.builders;

namespace ConsoleApp1.src.map.TacticalMovement.Strategies
{
    interface IStrategy
    {
        //Соответственно интерфейс для каждой из стратегии
        void DoAlgorithm(ref List<CreatureBuilder> Lcbs);
    }
}
