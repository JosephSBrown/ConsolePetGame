using System;
using System.Threading;
using System.Media;
using System.Collections.Generic;

namespace PetGame
{
    //Start of the Player Class
    class Player
    {
        private static object ConsoleLock = new object();   //Static Object to Prive a Thread Lock in the Method that will be Threaded
        public int coins { get; set; }  //Get Set Field for the Coin Values
        public List<Item> Inventory = new List<Item>(); //List to Initialise Player's Inventory
        
        //Assign Player Properties to Fields
        public Player()
        {
            coins = 0; //Set Player Coins to 0
        }

        //Method to Accumulate Coins Over Time
        public virtual void getCoins()
        {
            if (coins < 0)  //If Coins Is Less Than 0
            {
                coins = 0;  //Coins becomes Coins
            }
            else 
            {
                coins = coins;  //Otherwise Coins is Equal to the Current Coin Value
            }
            for ( ; ; ) //Infinite Loop to Ensure the Thread Runs Infinitely
            { 
                lock (ConsoleLock)  //Thread Lock to Stop Other Threads Accessing it at the Same Time
                {
                    if (coins % 100 == 0)   //If the Coin Value is Divisible by 100 and the Value is Exactly Equal to 0
                    {
                        SoundPlayer player = new SoundPlayer("collectcoin.wav");    //Initialise a New SoundPlayer with the Coin Collect Noise
                        player.Load();      //Load the SoundPlayer and the Sound
                        player.Play();      //Play the Coins Collection Sound
                    }
                    coins += 5;             //Add 5 Coins to the Player Coins Property Everytime the Thread Runs the Method
                }
                Thread.Sleep(3000);         //Pause the Thread for 3 Seconds before Running it Again
            }
                
        }
    }
}