using System;

namespace PetGame
{
    class Pet : IPet
    {

        public string Name { get; set; }
        public string Type { get; set; }
        public int Health { get; set; }
        public int Hunger { get; set; }
        public int MaxHealth { get; set; }
        public int MaxHunger { get; set; }
        public int MaxMood { get; set; }
        public int CurrentMood { get; set; }

        public virtual void DisplayPet()
        {

        }

        public virtual void decreaseHealth()
        { 
            
        }

        public virtual void decreaseHunger()
        {

        }

        public virtual void decreaseMood()
        { 
        
        }

        public virtual void standardsound()
        { 
        
        }

    }
}
