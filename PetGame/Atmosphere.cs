using System;
using System.Collections.Generic;
using System.Threading;

namespace PetGame
{
    //Class for Environment Object
    class Atmosphere
    {
        public double temperature { get; set; } //double value property for the temperature, will get the value and set it as this variable

        //variable assignment, call creating a new Atmosphere with parameter required for setting temperature value
        public Atmosphere(double T) //double T is receiving the temperature when called as a new object
        {
            temperature = T;    //Temperature Get Set Variable set from the Parameter
        }

    }
}