using System;

namespace PetGame
{
    class Item : IItem
    {

        public string Name { get; set; }
        public static int Integer { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }

        public virtual void invoke()
        { 
        
        }

    }
}