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
            //Whole map
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
            //Player's perspective map
            updatePPmap();
        }
        public static void updatePPmap()
        {
            VisualCharacters.PlayerPerspectiveMV.Clear();
            for (int i = -VisualCharacters.playerVisionRange; i<VisualCharacters.playerVisionRange; i++)
            {
                string arrch = "";
                for (int j = -VisualCharacters.playerVisionRange; j<VisualCharacters.playerVisionRange; j++)
                {   if (map.getPlayer().getX() + j >= testMap.GetLength(0) || map.getPlayer().getX() + j< 0 ||
                        map.getPlayer().getY() + i >= testMap.GetLength(1) || map.getPlayer().getY() + i< 0)
                        arrch += "~";
                    else
                    {
                        arrch += testMap[map.getPlayer().getX() + j, map.getPlayer().getY() + i];
                        Console.Write(testMap[map.getPlayer().getX() + j, map.getPlayer().getY() + i]);
                    }
                }
                arrch += "\n";
                VisualCharacters.PlayerPerspectiveMV.Add(arrch);

                Console.WriteLine();
            }
        }
    }
}
