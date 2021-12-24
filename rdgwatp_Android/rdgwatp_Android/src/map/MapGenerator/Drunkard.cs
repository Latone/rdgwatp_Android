using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;
namespace rdgwatp_Android.src.map.MapGenerator
{
    static class Drunkard
    {
        static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged; //Можно подписываться на него

        public static Level lvl;
        public static void Initialize(bool nextLevel)
        {
            if (lvl == null)
                lvl = new Level();
            if (nextLevel)
            {
                lvl.incWandH();
                lvl.setLvlNum(lvl.getLvlNum() + 1);
                lvl.setNOPTE(lvl.getStandartNOPTE(10));
                NotifyStaticPropertyChanged("nextlevel");
            }
            else
            {
                lvl.setLvlNum(1);
                lvl.setNOPTE(lvl.getStandartNOPTE(0));
            }

            setup();
            walking();
            CreatedMap.testMap = getMap();
        }
        private static char[,] map;
        public static short RstartPointX { get; set; }
        public  static short RstartPointY { get; set; }

        private static short startPointX, startPointY;
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
            
            map = new char[lvl.getSSW(), lvl.getSSH()];
            Enemy_Cords = new Stack<coords>();

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
            while (lvl.getNOPTE() > 0)
            {
                switch (Randomizator.GenerateRandomNumber(4))
                {
                    //left
                    case 0:
                        if (startPointX - 1 > 0 && map[startPointX - 1, startPointY] == VisualCharacters.WallPattern.FirstOrDefault())
                        {
                            if (lvl.getNOPTE() == 1)
                                map[startPointX - 1, startPointY] = VisualCharacters.FloorPattern[1];
                            else
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
                            if (lvl.getNOPTE() == 1)
                                map[startPointX, startPointY - 1] = VisualCharacters.FloorPattern[1];
                            else
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
                            if (lvl.getNOPTE() == 1)
                                map[startPointX + 1, startPointY] = VisualCharacters.FloorPattern[1];
                            else
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
                            if (lvl.getNOPTE() == 1)
                                map[startPointX, startPointY + 1] = VisualCharacters.FloorPattern[1];
                            else
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
                        lvl.setNOPTE(lvl.getNOPTE()+1);
                        break;
                }
                lvl.setNOPTE(lvl.getNOPTE() - 1);
            }
        }
        
    }
    
}
