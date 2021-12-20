using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;

namespace ConsoleApp1.src.map.TacticalMovement.Strategies
{
    class StrategyGoDown : IStrategy
    {
        //Реализация шага игрока вниз
        public void DoAlgorithm(ref List<CreatureBuilder> Lcbs)
        {
            foreach (CreatureBuilder cb in Lcbs)
            {
                //Нахождене игрока, изменение его координаты (Далее в коде будет FirstOrDefault по Lcb или ElementAt(0))
                if ((cb.getType() == creatures.types.Type.PLAYER || Lcbs.Count == 1) &&
                    cb.getY() + 1 < CreatedMap.testMap.GetLength(1) && VisualCharacters.FloorPattern.Contains(CreatedMap.testMap[cb.getX(), cb.getY() + 1]) &&
                    VisualCharacters.FloorPattern.Contains(VisualCharacters.mapview[cb.getY()+1].ToCharArray()[cb.getX()]))//CreatedMap.testMap[cb.getX(),cb.getY()] !='b' - изменится с рандомизацией карты
                {
                    cb.RememberLastCoordinates();
                    cb.setY((byte)(cb.getY() + 1));
                }
                //Действия Других существ
                else if(cb.getType() != creatures.types.Type.PLAYER)
                {
                    //Проверка: в погоне ли
                    if (!cb.getChase())
                        //Тут метается из стороны в сторону
                        EnemyMoves.Move(cb, Lcbs[0], Lcbs.ElementAt(0).getX(), Lcbs.ElementAt(0).getY());
                        //Тут бежит по указанному маршруту к игроку (маршрут - наименьшее расстояние,
                        //вычисленное между двумя точками (существо и игрок) в графе), но вычисляется оно далее
                    else
                        EnemyMoves.RunningThreat(cb,Lcbs[0], Lcbs.ElementAt(0).getX(), Lcbs.ElementAt(0).getY());
                }
            }
        }
    }
}
