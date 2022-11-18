using System;

namespace PetGame
{
    class Pet
    {

        public string Name { get; set; }
        public string PetType  { get; set; }
        public int Health {get; set;}

        public Pet(string name, string pettype)
        {
            Name = name;
            PetType = pettype;
            Health = 100;
        }
    }
}