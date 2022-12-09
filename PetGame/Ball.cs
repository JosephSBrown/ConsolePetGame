using System;
using System.Collections.Generic;

namespace PetGame
{
    //Class for Calling Ball Objects
    class Ball : Item   //Inherits the Item Class
    {

        //Properties set overriding the default Get Set inherited from Item
        public Ball()
        {
            Name = "Ball";  //Overriding Default Name to Ball
            Integer = 25;   //Overriding Default Integer to 25
            Type = "Toy";   //Overriding Default Type to Toy
            Cost = 15;      //Overriding Default Cost to 15
        }

    }
}