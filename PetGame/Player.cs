using System;
using System.Threading;
using System.Media;

namespace PetGame
{
    class Player
    {
        private static object ConsoleLock = new object();
        public int coins { get; set; }
        

        public Player()
        {
            coins = 0;
        }

        public virtual void getCoins()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            for ( ; ; )
            { 
                lock (ConsoleLock)
                {
                    Console.SetCursorPosition(0, 23);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Console.WriteLine($"Total Coins: {coins}");
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