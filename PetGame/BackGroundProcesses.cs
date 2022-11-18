using System;
using System.Media;
using System.Threading;

namespace PetGame
{
    class BackgroundProcesses
    {
        public void BackgroundMusic()
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
