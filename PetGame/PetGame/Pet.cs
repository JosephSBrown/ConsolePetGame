using System;
using System.Media;

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
        public int Bond { get; set; }
        public int MaxBond { get; set; }
        public int preferredtemperature { get; set; }

        public virtual void DisplayPet()
        {

        }

        public virtual void decreaseHealth(double temperature)
        { 
            
        }

        public virtual void increaseHunger()
        {

        }

        public virtual void decreaseMood()
        { 
        
        }

        public virtual void bondstats()
        { 
            
        }

        public virtual void standardsound()
        { 
        
        }

        public void deathsound()
        {
            SoundPlayer player = new SoundPlayer("gameover.wav");
            player.Load();
            player.Play();
        }

    }
}
