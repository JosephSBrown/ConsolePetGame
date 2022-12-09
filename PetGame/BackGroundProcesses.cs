using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;

namespace PetGame
{
    //Starting the Background Process Class
    class BackgroundProcesses
    {
        //Method for Starting the Background Music Loop
        public void BackgroundMusic(bool muted)
        {
            for (; ; )  //Infinite Loop so the Thread Loops Infinitely
            { 
                if (!muted) //If Muted is Not True, Play the Background Music
                {
                    SoundPlayer player = new SoundPlayer("backgroundmusic.wav");    //Initialise a New Soundplayer with the Background Music File as a Parameter
                    player.Load();  //Load the New SoundPlayer
                    player.Play();  //Play the Sound               
                }
                Thread.Sleep(9759); //Sleep the Method for Roughly 16 Bars before restarting the track again
            }                       //This also stops the Method being called instantly on repeat when the Thread Calls the Method
                
        }
    }
}