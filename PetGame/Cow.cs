using System;
using System.Media;
using System.Threading;

namespace PetGame
{
    //Start the Cow Class
    class Cow : Pet
    {
        private static object ConsoleLock = new object();

        //Cow Properties Overridden from Default inherited from Pet
        public Cow()
        {
            Type = "Cow";       //Set Pet Type as Cow
            MaxHealth = 100;    //Set Max Health as 100
            Health = 80;        //Set Health as 80
            MaxMood = 100;      //Set Maximum Mood as 100
            CurrentMood = 20;   //Set Current Mood as 20
            MaxHunger = 100;    //Set Maximum Hunger as 100
            Hunger = 0;         //Set Hunger as 0
            Bond = 0;           //Set Bond as 0
            MaxBond = 100;      //Set Maximum Bond as 100
            preferredtemperature = 20;      //Set the Preferred Temperature as 20

        }

        //Overridden Method to Display Pet with ASCII Art
        public override void DisplayPet()
        {
            //The Following String Variables Contain Each Line of the Pet ASCII ready for String Formatting
            string one   = @$"    \|/          (__)     ";
            string two   = $@"         `\------(oo)     ";
            string three = $@"           ||    (__)     ";
            string four  = $@"           ||w--||     \|/";
            string five  = $@"       \|/                ";

            //The following Write Lines Write the Aforementioned String Variables to Line in a String Format at the Center of the Window, Creating the ASCII Art In Line
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
            Console.Write($@"

");
        }

        //Method to Get Looped in a Thread to Decrease Health
        public override void decreaseHealth(double temperature)
        {
            for (; ; )   //Infinite for Loop to Loop the Method Infinitely in the Thread
            {
                lock (ConsoleLock)      //Locked to Ensure No Other Threads Can Access the Parts of the Method required to make it work
                {
                    //Decrease Health Based on Temperature Outside a Range around the Preferred Temperature
                    if (temperature > preferredtemperature + 1)     //If the Temperature is Greater than Preferred Temperature + 1
                    {
                        Health -= 2;    //Minus 2 From the Current Health Value
                    }
                    else if (temperature < preferredtemperature - 1)    //If Temperature is Less than the Preferred Temperature -1
                    {
                        Health -= 1;        //Current Health Value -1
                    }

                    if (Health == MaxHealth || Health > MaxHealth)      //If Health is Exactly Equal to Max Health OR Health is Greater Than Max Health
                    {
                        Health += 0;    //Current Health + 0
                        Health -= 0;    //Current Health - 0
                        Health = 100;   //Current Health is Equal to 100
                    }
                    else if (Health >= 70 && Health < 100)      //If Health is Greater Than Equal to 70 AND Less Than 100
                    {
                        Health -= 1;                            //Current Health - 1
                    }
                    else if (Health > 40 && Health < 70)        //If Health is Greater Than 40 AND Less than 70
                    {
                        Health -= 3;                            //Current Health - 3
                    }
                    else if (Health > 1 && Health < 40)         //If Health is Greater than 1 AND Health is Less Than 40
                    {
                        Health -= 5;                            //Current Health -5
                    }
                    else if (Health < 0 || Health == 0)         //If Current Health is Less than 0 OR Health is Equal to 0
                    {
                        Health -= 0;                            //Current Health - 0
                        Health = 0;                             //Health = 0
                    }
                }
                Thread.Sleep(3000);                 //Pause the Method for 3 Seconds Before Running the Method Again
            }


        }

        //Overridden Method to Increase Hunger
        public override void increaseHunger()
        {
            for (; ; )      //Infinite For Loop to Create Infinite Iterations in the Thread
            {
                lock (ConsoleLock)  //Locked to Ensure No Other Threads Can Access the Parts of the Method required to make it work
                {
                    Hunger += 2;        //Always Add Hunger + 2
                    if (Hunger == MaxHunger || Hunger > MaxHunger)      // If Hunger is Equal to Max Hunger OR is Greater than Max Hunger
                    {
                        Hunger += 0;        //Hunger has Nothing Added
                        Hunger -= 0;        //Hunger has Nothing Taken Off
                        Hunger = 100;       //Hunger Remains at 100
                    }
                    else if (Hunger >= 70 && Hunger < 99)       //If Hunger is Greater Than Equal To 70 AND Less than 99
                    {
                        Hunger += 3;        //Hunger Adds 3 on Top of the 2 Already Regularly Added
                    }
                    else if (Hunger > 40 && Hunger < 70)        //If Hunger is Greater Than 40 AND Hunger is Less than 70
                    {
                        Hunger += 1;        //Hunger Adds 1 on Top of the 2 Already Regularly Added
                    }
                    else if (Hunger <= 0)                        //If Hunger is Less Than Equal To 0
                    {
                        Hunger -= 0;                            //Hunger Has 0 Take OfF
                        Hunger += 0;                            //Hunger Has 0 Added
                        Hunger = 0;                             //Hunger Is Set to 0
                    }
                }
                Thread.Sleep(3000);     //Pause Thread for 3 Seconds Before Repeating
            }
        }

        //Overridden Method to Decrease Mood
        public override void decreaseMood()
        {
            for (; ; )      //Infinite For Loop to Create Infinite Iterations of the Method in the Thread
            {
                lock (ConsoleLock)  //Locked to Ensure No Other Threads Can Access the Parts of the Method required to make it work
                {
                    if (CurrentMood == MaxMood || CurrentMood > MaxMood)    //If the Current Mood is Equal to Max Mood OR Current Mood is More Than Max Mood
                    {
                        CurrentMood += 0;                                   //Mood has Nothing Added
                        CurrentMood -= 0;                                   //Mood has Nothing Minused
                        CurrentMood = 100;                                  //Current Mood is Set to 100
                    }
                    else if (CurrentMood >= 70 && CurrentMood < 99)         //If Current Mood is Greater Than Equal To 70 AND Less than 99
                    {
                        CurrentMood -= 1;                                   //Current Mood has 1 Minused
                    }
                    else if (CurrentMood > 40 && CurrentMood < 70)          //If Current Mood is Greater than 40 AND Current Mood is Less Than 70
                    {
                        CurrentMood -= 2;                                   //Current Mood has 2 minused
                    }
                    else if (CurrentMood > 1 && CurrentMood < 40)           //If Current Mood is Greater Than 1 AND Current Mood is Less Than 40
                    {
                        CurrentMood -= 3;                                   //Current Mood Has 3 Minused
                    }
                    else if (CurrentMood < 0 || CurrentMood == 0 || CurrentMood - 3 < 0)    //If Current Mood is Less Than Zero OR Current Mood is Equal to 0 OR Current Mood -3 is Less Than 0
                    {
                        CurrentMood += 0;                                   //Current Mood Has Nothing Added
                        CurrentMood -= 0;                                   //Current Mood has Nothing Minuses
                        CurrentMood = 0;                                    //Current Mood = 0
                    }
                }
                Thread.Sleep(5000);     //Pause Thread for 5 Seconds Before Repeating
            }
        }

        //Overridden Method to Decrease Bond Level
        public override void bondstats()
        {
            for (; ; )  //Infinite For Loop to Create Infinite Cycles in the Thread
            {
                lock (ConsoleLock)      //Locked to Ensure No Other Threads Can Access the Parts of the Method required to make it work
                {
                    if (Bond > 15 && Bond <= 100)      //If Bond is Greater than 15
                    {
                        Bond -= 3;      //Minus 3 From the Current Bond Level Until it Reaches 15
                    }
                    else
                    {                   //Otherwise
                        Bond -= 0;      //Bond Has Nothing Minused
                        Bond += 0;      //Bond has Nothing Plussed
                    }
                }
                Thread.Sleep(10000);    //Pause Thread for 10 Seconds Before Repeating
            }
        }

        //Override Method to Play the Pet's Standard Sound
        public override void standardsound()
        {
            SoundPlayer player = new SoundPlayer("cowstandard.wav");
            player.Load();
            player.Play();
        }

    }
}