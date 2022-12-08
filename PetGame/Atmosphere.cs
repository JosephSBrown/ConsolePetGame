using System;
using System.Collections.Generic;
using System.Threading;

namespace PetGame
{
    //Class for Environments Temperature
    class Atmosphere
    {
        public double temperature { get; set; }

        public Atmosphere(double T)
        {
            temperature = T;
        }

    }
}