using ConsoleApp1.src.map.TacticalMovement.LocalVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.TestChambers
{
    //Класс для тестовых помещений
    public static class TestChambers
    {
        public static char[,] Pick_Chamber(int num)
        {
            char w = VisualCharacters.WallPattern.FirstOrDefault();
            char f = VisualCharacters.FloorPattern.FirstOrDefault();
            switch (num) //Карты перевёрнуты: 1ый ряд - 1ый столбец
            {
                case 0:
                    return new char[7,7]{ {f,f,f,f,f,f,f },
                                          { f,f,f,f,f,f,f },
                                          { f,f,f,f,f,f,f },
                                          { f,f,f,f,f,f,f },
                                          { f,f,f,f,f,f,f },
                                          { f,f,f,f,f,f,f },
                                          { f,f,f,f,f,f,f }};
                case 1:
                    return new char[7, 7]{ {f,f,f,f,w,w,f },
                                          { f,f,f,f,f,f,f },
                                          { f,w,f,f,f,w,f },
                                          { f,w,f,f,f,w,f },
                                          { f,w,f,w,f,w,f },
                                          { f,f,f,f,f,f,f },
                                          { f,w,w,w,f,f,f }};
            }
            return null;
        }
    }
}
