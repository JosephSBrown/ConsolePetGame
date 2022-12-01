using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;

namespace PetGame
{
    class BackgroundProcesses
    {
        public void BackgroundMusic(bool muted)
        {
            bool loop = true;

            while (loop)
            {
                if (!muted)
                { 
                    SoundPlayer player = new SoundPlayer("kawaiifuturebass.wav");
                    player.Load();
                    player.Play();
                    Thread.Sleep(12500);
                }
            }
        }
    }
}