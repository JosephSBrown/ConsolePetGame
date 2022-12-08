using System;
using System.Media;
using System.Threading;

namespace PetGame
{
    class Cow : Pet
    {
        private static object ConsoleLock = new object();
        public Cow()
        {
            Type = "Cow";
            MaxHealth = 100;
            Health = 80;
            MaxMood = 100;
            CurrentMood = 20;
            MaxHunger = 100;
            Hunger = 0;
            Bond = 0;
            MaxBond = 100;
            preferredtemperature = 20;

        }

        public override void DisplayPet()
        {
            string one   = @$"    \|/          (__)     ";
            string two   = $@"         `\------(oo)     ";
            string three = $@"           ||    (__)     ";
            string four  = $@"           ||w--||     \|/";
            string five  = $@"       \|/                ";

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
            Console.Write($@"


");
        }

        public override void decreaseHealth(double temperature)
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (temperature > preferredtemperature + 1)
                    {
                        Health -= 2;
                    }
                    else if (temperature < preferredtemperature - 1)
                    {
                        Health -= 1;
                    }

                    if (Health == MaxHealth || Health > MaxHealth)
                    {
                        Health += 0;
                        Health -= 0;
                        Health = 100;
                    }
                    else if (Health + 2 > MaxHealth)
                    {
                        Health = 100;
                    }
                    else if (Health >= 70 && Health < 100)
                    {
                        Health -= 1;
                    }
                    else if (Health > 40 && Health < 70)
                    {
                        Health -= 3;
                    }
                    else if (Health > 1 && Health < 40)
                    {
                        Health -= 5;
                    }
                    else if (Health < 0 || Health == 0)
                    {
                        Health -= 0;
                        Health = 0;
                    }
                }
                Thread.Sleep(3000);
            }


        }

        public override void increaseHunger()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    Hunger += 2;
                    if (Hunger == MaxHunger || Hunger > MaxHunger)
                    {
                        Hunger += 0;
                        Hunger -= 0;
                        Hunger = 100;
                    }
                    else if (Hunger + 2 > MaxHunger)
                    {
                        Hunger = 100;
                    }
                    else if (Hunger >= 70 && Hunger < 99)
                    {
                        Hunger += 5;
                    }
                    else if (Hunger > 40 && Hunger < 70)
                    {
                        Hunger += 3;
                    }
                    else if (Hunger < 0)
                    {
                        Hunger -= 0;
                        Hunger += 0;
                        Hunger = 0;
                    }
                }
                Thread.Sleep(3000);
            }
        }

        public override void decreaseMood()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (CurrentMood == MaxMood || CurrentMood > MaxMood)
                    {
                        CurrentMood += 0;
                        CurrentMood -= 0;
                        CurrentMood = 100;
                    }
                    else if (CurrentMood + 2 > MaxMood)
                    {
                        CurrentMood = 100;
                    }
                    else if (CurrentMood >= 70 && CurrentMood < 99)
                    {
                        CurrentMood -= 1;
                    }
                    else if (CurrentMood > 40 && CurrentMood < 70)
                    {
                        CurrentMood -= 2;
                    }
                    else if (CurrentMood > 1 && CurrentMood < 40)
                    {
                        CurrentMood -= 3;
                    }
                    else if (CurrentMood < 0 || CurrentMood == 0 || CurrentMood - 3 < 0)
                    {
                        CurrentMood += 0;
                        CurrentMood -= 0;
                        CurrentMood = 0;
                    }
                }
                Thread.Sleep(5000);
            }
        }

        public override void bondstats()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (Bond > 15)
                    {
                        Bond -= 3;
                    }
                    else
                    {
                        Bond -= 0;
                        Bond += 0;
                    }
                }
                Thread.Sleep(10000);
            }
        }

        public override void standardsound()
        {
            SoundPlayer player = new SoundPlayer("cowstandard.wav");
            player.Load();
            player.Play();
        }

    }
}