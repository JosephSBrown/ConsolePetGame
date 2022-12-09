using System;
using System.Collections.Generic;

namespace PetGame
{
    //Starting a New Interface for Items
    internal interface IItem
    {
        void invoke(List<Pet> petlist, int index);  //Invoke Method for Using the Item, Empty Body as this is an Interface Method and Directs Its Child Classes
    }
}