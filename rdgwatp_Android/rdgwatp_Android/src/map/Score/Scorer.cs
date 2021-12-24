using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace rdgwatp_Android.src.map.Score
{
    static class Scorer
    {
        static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged; //Можно подписываться на него

        public static void addToScore(int val)
        {
            FullScore += val;
            NotifyStaticPropertyChanged("ScoreUpdate");
        }
        public static void dropScore()
        {
            FullScore = 0;
        } 
        static public int getFullScore()
        {
            return FullScore;
        }
        static int FullScore = 0;
    }
}
