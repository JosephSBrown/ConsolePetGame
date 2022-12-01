using System;

namespace PetGame
{
    internal interface IPet
    {
        void DisplayPet();
        void decreaseHealth();
        void increaseHunger();
        void decreaseMood();
        void standardsound();
    }
}