using System;
using System.Collections.Generic;

namespace PetGame
{
    //Start the Ramune Class
    class Ramune : Item //Inherits Values and Method from the Item Class
    {

        //Set the Properties of Ramune based off the Default Fields inherited from Item
        public Ramune()
        {
            Name = "Ramune Soda";   //Set Name Property as Ramune Soda
            Integer = 10;           //Set Integer Property as 10
            Type = "Food";          //Set Type Property as Food
            Cost = 15;              //Set Cost Property as 15
        }

    }
}
