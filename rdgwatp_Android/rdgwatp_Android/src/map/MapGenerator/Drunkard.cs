using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;
namespace rdgwatp_Android.src.map.MapGenerator
{
    static class Drunkard
    {
        public static void Initialize()
        {
            setup();
            walking();
            CreatedMap.testMap = getMap();
        }
        private static char[,] map;
        public static short RstartPointX { get; set; }
        public  static short RstartPointY { get; set; }

        private static short startPointX, startPointY;
        private static int NumberOfPointsToEmpty;
        public struct coords {public short x, y; }
        public static Stack<coords> Enemy_Cords;
        static void Populate<T>(this T[,] arr, T value)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i,j] = value;
                }
            }
        }
        static void setup()
        {
            map = new char[20, 20];
            Enemy_Cords = new Stack<coords>();
            NumberOfPointsToEmpty = 175;
            Populate<char>(map, VisualCharacters.WallPattern.FirstOrDefault());
            RstartPointX = (short)Randomizator.GenerateRandomNumber(map.GetLength(0));
            RstartPointY = (short)Randomizator.GenerateRandomNumber(map.GetLength(1));
            startPointX = RstartPointX;
            startPointY = RstartPointY;
            map[RstartPointX, RstartPointY] = VisualCharacters.FloorPattern.FirstOrDefault();
        }
        public static char[,] getMap()
        {
            return map;
        }
        static void walking()
        {
            while (NumberOfPointsToEmpty > 0)
            {
                switch (Randomizator.GenerateRandomNumber(4))
                {
                    //left
                    case 0:
                        if (startPointX - 1 > 0 && map[startPointX - 1, startPointY] == VisualCharacters.WallPattern.FirstOrDefault())
                        {
                            map[startPointX - 1, startPointY] = VisualCharacters.FloorPattern.FirstOrDefault();
                            startPointX--;
                            if (Randomizator.GenerateRandomNumber(1000) > 950)
                                Enemy_Cords.Push(new coords { x=startPointX, y=startPointY });
                        }
                        else
                            goto default;

                        break;
                    //up
                    case 1:
                        if (startPointY - 1 > 0 && map[startPointX, startPointY-1] == VisualCharacters.WallPattern.FirstOrDefault())
                        { 
                            map[startPointX, startPointY - 1] = VisualCharacters.FloorPattern.FirstOrDefault();
                            startPointY--;
                            if (Randomizator.GenerateRandomNumber(1000) < 70)
                                Enemy_Cords.Push(new coords { x = startPointX, y = startPointY });
                        }
                        else
                            goto default;
                        break;
                    //right
                    case 2:
                        if (startPointX + 1 < map.GetLength(0) && map[startPointX + 1, startPointY] == VisualCharacters.WallPattern.FirstOrDefault())
                        {
                            map[startPointX + 1, startPointY] = VisualCharacters.FloorPattern.FirstOrDefault();
                            startPointX++;
                            if (Randomizator.GenerateRandomNumber(1000) > 950)
                                Enemy_Cords.Push(new coords { x = startPointX, y = startPointY });
                        }
                        else
                            goto default;
                        break;
                    //down
                    case 3:
                        if (startPointY + 1 < map.GetLength(1) && map[startPointX, startPointY + 1] == VisualCharacters.WallPattern.FirstOrDefault())
                        { 
                            map[startPointX, startPointY + 1] = VisualCharacters.FloorPattern.FirstOrDefault();
                            startPointY++;
                            if (Randomizator.GenerateRandomNumber(1000) < 70)
                                Enemy_Cords.Push(new coords { x = startPointX, y = startPointY });
                        }
                        else
                         goto default;
                        break;
                   default:
                        switch (Randomizator.GenerateRandomNumber(4))
                        {
                            //left
                            case 0:
                                if (startPointX - 1 > 0 && 
                                    map[startPointX - 1, startPointY] == VisualCharacters.FloorPattern.FirstOrDefault())
                                        startPointX--;
                                break;
                            //up
                            case 1:
                                if (startPointY - 1 > 0 &&
                                    map[startPointX, startPointY - 1] == VisualCharacters.FloorPattern.FirstOrDefault())
                                        startPointY--;
                                break;
                            //right
                            case 2:
                                if (startPointX + 1 < map.GetLength(0) &&
                                    map[startPointX + 1, startPointY] == VisualCharacters.FloorPattern.FirstOrDefault())
                                        startPointX++;
                                break;
                            //down
                            case 3:
                                if (startPointY + 1 < map.GetLength(1) &&
                                    map[startPointX, startPointY + 1] == VisualCharacters.FloorPattern.FirstOrDefault())
                                        startPointY++;
                                break;
                        }
                        NumberOfPointsToEmpty++;
                        break;
                }
                NumberOfPointsToEmpty--;
            }
        }
        
    }
    
}
