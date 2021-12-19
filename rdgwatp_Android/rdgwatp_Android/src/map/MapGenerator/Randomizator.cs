using System;
using System.Collections.Generic;
using System.Text;

namespace rdgwatp_Android.src.map.MapGenerator
{
    public class Randomizator
    {
        private static Random random;
        private static object syncObj = new object();
        private static void InitRandomNumber(int seed)
        {
            random = new Random(seed);
        }
        public static int GenerateRandomNumber(int max)
        {
            lock (new object())
            {
                if (random == null)
                    random = new Random(); // Or exception...
                return random.Next(max);
            }
        }
    }
}
