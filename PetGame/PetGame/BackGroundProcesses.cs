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
                    SoundPlayer player = new SoundPlayer("backgroundmusic.wav");
                    player.Load();
                    player.PlayLooping();                
                }
                Thread.Sleep(9759);
            }
        }
    }
}