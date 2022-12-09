using System;
using System.Collections.Generic;

namespace PetGame
{
    //Start the Medicine Class
    class Medicine : Item   //Inherits Methods from the Item Class
    {

        //Medicine Properties Overriding from the Default Values Gained from Item
        public Medicine()
        {
            Name = "Small Medicine";    //Set Name Property to Small Medicine
            Integer = 15;               //Set Integer Property to 15
            Type = "Medicine";          //Set Type Property as Medicine
            Cost = 35;                  //Set Cost Property as 35
        }

    }
}