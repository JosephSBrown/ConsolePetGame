using System;
using System.Media;
using System.Threading;

namespace PetGame
{
    class Dolphin : Pet
    {
        private static object ConsoleLock = new object();
        public Dolphin()
        {
            Type = "Dolphin";
            MaxHealth = 100;
            Health = 60;
            MaxMood = 100;
            CurrentMood = 65;
            MaxHunger = 100;
            Hunger = 0;
        }

        public override void DisplayPet()
        {
            string one    = @$"              ,-.             ";
            string two    = $@"             / ('             ";
            string three  = $@"     * _.--'!   '--._         ";
            string four   = $@"    ,'              ''.       ";
            string five   = $@"   |!                   \     ";
            string six    = $@" _.'  O      ___       ! \    ";
            string seven  = $@"(_.- ^, __..-'  ''''--.   )   ";
            string eight  = $@"    /, '        '    _.' /    ";
            string nine   = $@" '         *    .-''    |     ";
            string ten    = $@"              (..--^.  '      ";
            string eleven = $@"                    | /       ";
            string twelve = $@"                     '        ";

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (six.Length / 2)) + "}", six));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (seven.Length / 2)) + "}", seven));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (eight.Length / 2)) + "}", eight));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (nine.Length / 2)) + "}", nine));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (ten.Length / 2)) + "}", ten));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (eleven.Length / 2)) + "}", eleven));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (twelve.Length / 2)) + "}", twelve));
            Console.Write($@"


");
        }

        public override void decreaseHealth()
        {
            for (; ;)
            {
                lock (ConsoleLock)
                {
                    if (Health >= 70 && Health < 100)
                    {
                        Health -= 1;
                    }
                    else if (Health > 40 && Health < 70)
                    {
                        Health -= 3;
                    }
                    else if (Health - 2 < 0)
                    {
                        Health = 0;
                    }
                    else
                    {
                        Health -= 5;
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
                    if (Hunger >= 70 && Hunger < 100)
                    {
                        Health -= 5;
                    }
                    else if (Hunger > 40 && Hunger < 70)
                    {
                        Health -= 3;
                    }
                    else if (Hunger + 2 > 100)
                    {
                        Hunger = 100;
                    }
                    Hunger = Hunger + 2;
                }
                Thread.Sleep(1000);    
            }
        }

        public override void decreaseMood()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (CurrentMood >= 70 && CurrentMood < 100)
                    {
                        //Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (CurrentMood > 40 && CurrentMood < 70)
                    {
                        //Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (CurrentMood - 2 < 0)
                    {
                        CurrentMood = 0;
                    }
                    else
                    {
                        //Console.ForegroundColor = ConsoleColor.Red;
                    }
                    CurrentMood = CurrentMood - 2;
                }
                Thread.Sleep(5000);
            }
        }

        public override void standardsound()
        {
            SoundPlayer player = new SoundPlayer("dolphinstandard.wav");
            player.Load();
            player.Play();
        }

    }
}