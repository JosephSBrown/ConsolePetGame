using System;

namespace PetGame
{
    //Starting a New Interface for Pets
    internal interface IPet
    {
        void DisplayPet();  //Empty Method for Directing Child Classes on Displaying Pet ASCII
        void decreaseHealth(double temperature);    //Empty Method for Directing Child Classes on Decreasing Health, will take the temperature parameter as Temperature affects Pet's Health
        void increaseHunger();  //Empty Method for Directing Child Classes on Increase Hunger
        void decreaseMood();    //Empty Method for Directing Child Classes on Decreasing Mood
        void bondstats();       //Empty Method for Directing Child Method on the Way Bond Stats Works
        void standardsound();   //Empty Method for Directing Child Classes on Making the Pet Sound
        void deathsound();      //Empty Method for Directing Child Classes on the Sound Played on Death
        void eatingsound();      //Empty Method for Directing Child Classes on the Sound Played While Eating
    }
}