using System;
using ConsoleApp1.src.map.TacticalMovement.MinPath;
using rdgwatp_Android.src.map.MapGenerator;

namespace ConsoleApp1.src.map.TacticalMovement.LocalVars
{
    //Всеобщая на первых стадиях карта
    public static class CreatedMap
    {
        //Выборка комнаты из доступных
        public static char[,] testMap;
        //Заполнение графа по карте
        public static void CreateGraph()
        {
            Graph.fillGraph();
        }
        //Показ, отрисовка карты
        public static void ShowMap()
        {
            for (byte i = 0; i < testMap.GetLength(1); i++)
            {
                string arrch = "";
                for (byte j = 0; j < testMap.GetLength(0); j++)
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
