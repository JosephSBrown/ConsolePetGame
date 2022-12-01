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
            Hunger = 65;
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
                    Console.SetCursorPosition(0, 19);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Health = Health - 2;
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    if (Health >= 70 && Health < 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Health: {Health}"); ;
                    }
                    else if (Health > 40 && Health < 70)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Health: {Health}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Health: {Health}");
                    }
                }
                Thread.Sleep(1000);
            }


        }

        public override void decreaseHunger()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    Console.SetCursorPosition(0, 20);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Hunger = Hunger - 2;
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    if (Hunger >= 70 && Hunger < 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Hunger: {Hunger}"); ;
                    }
                    else if (Hunger > 40 && Hunger < 70)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Hunger: {Hunger}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Hunger: {Hunger}");
                    }
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
                    Console.SetCursorPosition(0, 21);
                    Console.BackgroundColor = ConsoleColor.Black;
                    CurrentMood = CurrentMood - 2;
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    if (CurrentMood >= 70 && CurrentMood < 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Current Mood: {CurrentMood}"); ;
                    }
                    else if (CurrentMood > 40 && CurrentMood < 70)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Current Mood: {CurrentMood}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Current Mood: {CurrentMood}");
                    }
                }
                Thread.Sleep(1000);
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