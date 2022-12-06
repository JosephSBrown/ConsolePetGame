using System;
using System.Collections.Generic;
using System.Threading;

namespace PetGame
{
    class Atmosphere
    {
        static object ConsoleLock = new object();
        public static double temperature = 19.00;

        public static void decreasetemperature()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (temperature > 18.60)
                    {
                        temperature -= 0.01;
                    }
                    
                }
                Thread.Sleep(1000);
            }
        }

    }
}