﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;

namespace PetGame
{
    class App
    {
        bool mute = false;
        public static List<Pet> AllPets = new List<Pet>();
        public static object ConsoleLock = new object();
        static Player player = new Player();
        static public int petindex = 0;
        static public int shopindex = 0;
        static public int invindex = 0;
        public static List<Item> items = GetClassType<Item>();

        public void Run()
        {
            BackgroundProcesses bgp = new BackgroundProcesses();

            Thread bgm = new Thread(() => bgp.BackgroundMusic(mute));
            bgm.Start();

            titlemenu();
        }

        public static void titlemenu()
        {
            int index = 0;
            //Menu Options
            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("New Game", () => newPet()),
                new MenuOption("Load Game", () => createPetList(AllPets, AllPets[index])),
                new MenuOption("Options", () => Options.Menu()),
                new MenuOption("Exit", () => exit()),
            };

            createMenu(options, options[index]);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        createMenu(options, options[index]);
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        createMenu(options, options[index]);
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    options[index].selected.Invoke();
                }
            }
            while (key.Key != ConsoleKey.Escape);

            return;

        }

        public static void createMenu(List<MenuOption> options, MenuOption selectedOption)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            TextDisplay.titleText();

            foreach (MenuOption o in options)
            {
                if (o == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o.option.Length / 2)) + "}", o.option));
            }
        }

        public static void createPetList(List<Pet> options, Pet selectedOption)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            TextDisplay.selectPet();

            foreach (Pet o in options)
            {
                if (o == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Get(o.GetType()).Length / 2)) + "}", Get(o.GetType())));
            }
        }


        public static void newPet()
        {
            Console.Clear();

            List<Pet> pets = GetClassType<Pet>();
            int index = 0;

            createPetList(pets, pets[index]);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        createPetList(pets, pets[index]);
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < pets.Count)
                    {
                        index++;
                        createPetList(pets, pets[index]);
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            while (key.Key != ConsoleKey.Escape);

            Console.Clear();

            TextDisplay.selectPet();

            pets[index].standardsound();
            pets[index].DisplayPet();

            string naming = "What Would You Like Your Pet to be Called? ";
            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (naming.Length / 2)) + "}", naming));
            string name = Console.ReadLine();

            AllPets.Add(pets[index]);
            AllPets.Last().Name = name;

            loadPet();

            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    titlemenu();
                    return;
                default:
                    return;
            }
        }

        public static void loadPet()
        {
            Console.Clear();

            Console.SetCursorPosition(0, 0);
            Thread Game = new Thread(() => GameMenu(petindex, invindex, shopindex));
            Game.Start();

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (petindex - 1 >= 0)
                    {
                        petindex--;
                        loadPet();
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (petindex + 1 < AllPets.Count)
                    {
                        petindex++;
                        loadPet();
                    }
                }
                else if (key.Key == ConsoleKey.A)
                {
                    if (invindex - 1 >= 0)
                    {
                        invindex--;
                        loadPet();
                    }
                }
                else if (key.Key == ConsoleKey.D)
                {
                    if (invindex + 1 < items.Count)
                    {
                        invindex++;
                        loadPet();
                    }
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    if (shopindex - 1 >= 0)
                    {
                        shopindex--;
                        loadPet();
                    }
                }
                else if (key.Key == ConsoleKey.E)
                {
                    if (shopindex + 1 < items.Count)
                    {
                        shopindex++;
                        loadPet();
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    titlemenu();
                }
            }
            while (key.Key != ConsoleKey.Escape);

        }

        public static void GameMenu(int index, int invindex, int shopindex)
        {

            for (; ; )
            { 
                lock (ConsoleLock)
                {

                    Console.SetCursorPosition(0, 1);
                    string topbar = $"< {AllPets[index].Name} : {AllPets[index].Type} >";
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (topbar.Length / 2)) + "}", topbar));

                    Console.SetCursorPosition(0, 2);
                    string PetName = @$"Pet Name: {AllPets[index].Name}";
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (PetName.Length / 2)) + "}", PetName));

                    Console.SetCursorPosition(0, 4);
                    AllPets[index].DisplayPet();

                    
                    Thread Hunger = new Thread(AllPets[index].increaseHunger);
                    Thread Health = new Thread(AllPets[index].decreaseHealth);
                    Thread Mood = new Thread(AllPets[index].decreaseMood);
                    Thread Coins = new Thread(() => player.getCoins());
                    Console.SetCursorPosition(0, 18);
                    Console.WriteLine($"Health: {AllPets[index].Health}");
                    Health.Start();
                    Console.SetCursorPosition(0, 19);
                    Console.WriteLine($"Hunger: {AllPets[index].Hunger}");
                    Hunger.Start();
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine($"Mood: {AllPets[index].CurrentMood}");
                    Mood.Start();
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine($"Total Coins: {player.coins}");
                    Coins.Start();
                    Console.SetCursorPosition(0, 23);
                    shopMenu(shopindex);
                    Console.SetCursorPosition(0, 26);
                    displayInventory(invindex);
                }
                Thread.Sleep(1000);
            }
                
            
        }

        public static void shopMenu(int index)
        {
            string shop = "--- SHOP ---";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (shop.Length / 2)) + "}", shop));
            string shopbar = $"<-     {items[index].Name} : {items[index].Type}    ->";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (shopbar.Length / 2)) + "}", shopbar));
        }

        public static void displayInventory(int index)
        {
            string inventory = "--- INVENTORY ---";
            List<Item> inv = player.Inventory;
            inv = GetClassType<Item>();
            string invbar = $"<-     {inv[index].Name} : {inv[index].Type}    ->";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (inventory.Length / 2)) + "}", inventory));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (invbar.Length / 2)) + "}", invbar));
        }

        public static void exit()
        {
            Environment.Exit(0);
        }

        public static List<T> GetClassType<T>() where T : class
        {
            List<T> types = new List<T>();
            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                types.Add((T)Activator.CreateInstance(type));
            }
            return types;
        }

        public static string Get(Type input)
        {
            string[] words = input.ToString().Split('.');
            string lastWord = words[^1];
            string[] splitWord = Regex.Split(lastWord, @"(?<!^)(?=[A-Z])");
            string convertedWord = string.Join(" ", splitWord);
            return convertedWord;
        }


    }
}
