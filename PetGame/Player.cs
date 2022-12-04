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

        public virtual void playerInventory()
        { 
            
        }
    }
}