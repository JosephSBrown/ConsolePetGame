using System;
using System.Collections.Generic;
using System.Threading;

namespace PetGame
{
    class Atmosphere
    {
        static object ConsoleLock = new object();
        public static double temperature = 21.00;

        public static void temperaturechange(int heating)
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (temperature > 18.60 || temperature < 25.00)
                    {
                        if (heating == 0)
                        {
                            temperature -= 0.01;
                        }
                        else if (heating == 1)
                        {
                            temperature += 0.01;
                        }
                    }                    
                }
                Thread.Sleep(1000);
            }
        }

    }
}