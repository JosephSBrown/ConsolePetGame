using System;
using System.Collections.Generic;

namespace PetGame
{
    //Starting the Crisp Class
    class Crisps : Item     //Crips inherits Item Class
    {

        //Setting the Properties of the Class Overriding the Default Properties Inherited
        public Crisps()
        {
            Name = "BBQ Chicken Crisps";    //Overriding Default Name
            Integer = 50;                   //Overriding Default Integer
            Type = "Food";                  //Overriding Default Type
            Cost = 10;                      //Overriding Default Cost
        }

    }
}