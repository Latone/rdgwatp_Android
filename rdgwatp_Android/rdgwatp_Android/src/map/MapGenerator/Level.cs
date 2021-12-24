using System;
using System.Collections.Generic;
using System.Text;

namespace rdgwatp_Android.src.map.MapGenerator
{
    class Level
    {

        public int getNOPTE()
        {
            return NumberOfPointsToEmpty;
        }
        public void setNOPTE(int num)
        {
            NumberOfPointsToEmpty = num;
        }
        public int getLvlNum()
        {
            return LevelNumber;
        }
        public void setLvlNum(int num)
        {
            LevelNumber = num;
        }
        public int creatureBuff = 2;
        public void incWandH()
        {
            SizeH += additionalH;
            SizeW += additionalW;
        }
        public int getSSH()
        {
            return SizeH;
        }
        public int getSSW()
        {
            return SizeH;
        }
        public int getStandartNOPTE(int incrBy)
        {
            standartNOPTE += incrBy;
            return standartNOPTE;
        }
        int NumberOfPointsToEmpty = 50;
        int standartNOPTE = 50;
        int SizeH = 10;
        int SizeW = 12;

        int LevelNumber = 1;

        const int additionalH = 2;
        const int additionalW = 2;
    }
}
