using System;
using System.Threading;
using System.Media;
using System.Collections.Generic;

namespace PetGame
{
    class Player
    {
        private static object ConsoleLock = new object();
        public int coins { get; set; }
        public List<Item> Inventory = new List<Item>();
        

        public Player()
        {
            coins = 0;
        }

        public virtual void getCoins()
        {
            if (coins < 0)
            {
                coins = 0;
            }
            else 
            {
                coins = coins;
            }
            for ( ; ; )
            { 
                lock (ConsoleLock)
                {
                    if (coins % 100 == 0) 
                    {
                        SoundPlayer player = new SoundPlayer("collectcoin.wav");
                        player.Load();
                        player.Play();
                    }
                    coins += 5;
                }
                Thread.Sleep(3000);
            }
                
        }
    }
}