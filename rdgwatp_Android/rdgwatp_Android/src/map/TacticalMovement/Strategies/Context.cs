using System.Collections.Generic;
using ConsoleApp1.src.creatures.builders;

namespace ConsoleApp1.src.map.TacticalMovement.Strategies
{
    //Context - для создания стратегии
    class Context
    {
        private IStrategy strategy;

        public Context() { } //Пустой конструктор

        public Context(IStrategy strategy) //Конструктор со стратегией
        {
            this.strategy = strategy;
        }
        public void SetStrategy(IStrategy strategy) //Изменить стратегию
        {
            this.strategy = strategy;
        }
        //Имплементировать бизнесс-логику
        public void DoSomeBusinessLogic(ref List<CreatureBuilder> Lcbs)
        {
            this.strategy.DoAlgorithm(ref Lcbs);
        }
    }
}
