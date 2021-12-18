using System;
using ConsoleApp1.src.map.TacticalMovement.MinPath;

namespace ConsoleApp1.src.map.TacticalMovement.LocalVars
{
    //Всеобщая на первых стадиях карта
    public static class CreatedMap
    {
        //Размеры комнаты
        public static byte Mwidth = 7,
                           Mheight = 7;
        //Выборка комнаты из доступных
        public static char[,] testMap = TestChambers.TestChambers.Pick_Chamber(1);
        //Заполнение графа по карте
        public static void CreateGraph()
        {
            Graph.fillGraph();
        }
        //Показ, отрисовка карты
        public static void ShowMap()
        {
            for (byte i = 0; i < Mheight; i++)
            {
                string arrch = "";
                for (byte j = 0; j < Mwidth; j++)
                {
                    arrch +=  testMap[j, i];
                    Console.Write(testMap[j, i]);
                }
                arrch += "\n";
                VisualCharacters.mapview.Add(arrch);

                Console.WriteLine();
            }
        }
    }
}
