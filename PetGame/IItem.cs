using System;
using System.Collections.Generic;

namespace PetGame
{
    internal interface IItem
    {
        void invoke(List<Pet> petlist, int index);
    }
}