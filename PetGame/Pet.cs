using System;
using System.Media;

namespace PetGame
{
    //Start the Pet Class
    class Pet : IPet    //Inherits the iPet Interface
    {

        public string Name { get; set; }    //Get Set the Name Field
        public string Type { get; set; }    //Get Set the Type Field
        public int Health { get; set; }     //Get Set the Health Field
        public int Hunger { get; set; }     //Get Set the Hunger Field
        public int MaxHealth { get; set; }  //Get Set the Max Health
        public int MaxHunger { get; set; }  //Get Set the Max Hunger
        public int MaxMood { get; set; }    //Get Set the Max Amount of Mood Level
        public int CurrentMood { get; set; }    //Get Set the Current Mood Level
        public int Bond { get; set; }       //Get Set the Bond Level
        public int MaxBond { get; set; }    //Get Set the Max Bond Level
        public int preferredtemperature { get; set; }   //Get Set the Pet's Preferred Temperature

        //Empty Method to Override in the Child Class, Virtual Method to be easily Overridden
        public virtual void DisplayPet()
        {

        }

        //Empty Method to Override in the Child Class, Virtual Method to be Easily Overridden
        public virtual void decreaseHealth(double temperature)
        { 
            
        }

        //Empty Method to Override in the Child Class, Virtual Method to be Easily Overridden
        public virtual void increaseHunger()
        {

        }

        //Empty Method to Override in the Child Class, Virtual Method to be Easily Overridden
        public virtual void decreaseMood()
        { 
        
        }

        //Empty Method to Override in the Child Class, Virtual Method to be Easily Overridden
        public virtual void bondstats()
        { 
            
        }

        //Empty Method to Override in the Child Class, Virtual Method to be Easily Overridden
        public virtual void standardsound()
        { 
        
        }

        //Method to be Inherited by the Child Class to Play the Sound on Death
        public void deathsound()
        {
            SoundPlayer player = new SoundPlayer("gameover.wav");
            player.Load();
            player.Play();
        }

        //Method to be Inherited by the Child Class to Play the Sound while Eating
        public void eatingsound()
        {
            SoundPlayer player = new SoundPlayer("eating.wav");
            player.Load();
            player.Play();
        }

    }
}
