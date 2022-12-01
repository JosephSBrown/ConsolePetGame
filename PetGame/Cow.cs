﻿using System;
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
            MaxHealth = 70;
            Health = 60;
            MaxMood = 100;
            CurrentMood = 65;
            MaxHunger = 100;
            Hunger = 65;

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

        public override void decreaseHealth()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    Console.SetCursorPosition(0, 19);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    if (Health >= 70 && Health < 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Health -= 1;
                        Console.WriteLine($"Health: {Health}");
                    }
                    else if (Health > 40 && Health < 70)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Health: {Health}");
                        Health -= 3;
                    }
                    else if (Health - 2 < 0)
                    {
                        Health = 0;
                        Console.WriteLine($"Health: {Health}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Health: {Health}");
                        Health -= 5;
                    }
                }
                Thread.Sleep(1000);
            }


        }

        public override void increaseHunger()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    Console.SetCursorPosition(0, 20);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    if (Hunger >= 70 && Hunger < 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Hunger: {Hunger}"); ;
                        Health -= 5;
                    }
                    else if (Hunger > 40 && Hunger < 70)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Hunger: {Hunger}");
                        Health -= 3;
                    }
                    else if (Hunger + 2 > 100)
                    {
                        Hunger = 100;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Hunger: {Hunger}");
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
                    Console.SetCursorPosition(0, 21);
                    Console.BackgroundColor = ConsoleColor.Black;
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
                    else if (CurrentMood - 2 < 0)
                    {
                        CurrentMood = 0;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Current Mood: {CurrentMood}");
                    }
                    CurrentMood = CurrentMood - 2;
                }
                Thread.Sleep(1000);
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