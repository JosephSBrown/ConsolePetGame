using System;

namespace PetGame
{
    class Pet : IPet
    {

        public string Name { get; set; }
        public string Type { get; set; }
        public int MaxHealth { get; set; }
        public int MaxHunger { get; set; }
        public int MaxMood { get; set; }
        public int CurrentHealth { get; set; }
        public int CurrentHunger { get; set; }
        public int CurrentMood { get; set; }

        public virtual void DisplayPet()
        {

        }
    }
}
