using System;

namespace PetGame
{
    internal interface IPet
    {
        void DisplayPet();
        void decreaseHealth(double temperature);
        void increaseHunger();
        void decreaseMood();
        void bondstats();
        void standardsound();
        void deathsound();
    }
}