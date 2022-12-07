using System;
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
        public static int heating = 0;
        public static List<Pet> AllPets = new List<Pet>();
        public static object ConsoleLock = new object();
        static Player player = new Player();
        static Ball b = new Ball();
        static Medicine m = new Medicine();
        static Crisps c = new Crisps();
        static public int petindex = 0;
        static public int shopindex = 0;
        static public int invindex = 0;
        public static List<Item> items = GetClassType<Item>();
        static bool gamethreads = false;

        public void Run()
        {
            Console.Title = "Obscure Pet Simulator";
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
        }

        public static void loadPet()
        {            
            Console.Clear();

            Thread Game = new Thread(() => GameMenu(petindex, invindex, shopindex));
            Game.Start();

            ConsoleKeyInfo k = Console.ReadKey();

            if (k.Key == ConsoleKey.LeftArrow)
            {
                if (petindex - 1 >= 0)
                {
                    petindex--;
                    loadPet();
                }
            }
            else if (k.Key == ConsoleKey.RightArrow)
            {
                if (petindex + 1 < AllPets.Count)
                {
                    petindex++;
                    loadPet();
                }
            }
            else if (k.Key == ConsoleKey.P)
            {
                Console.WriteLine(@$"You Played with {AllPets[petindex].Name} with a {b.Name}");
                b.invoke(AllPets, petindex);
                Thread.Sleep(1000);
                loadPet();
            }
            else if (k.Key == ConsoleKey.O)
            {
                Console.WriteLine(@$"You Fed {AllPets[petindex].Name} some {c.Name}");
                c.invoke(AllPets, petindex);
                Thread.Sleep(2000);
                loadPet();
            }
            else if (k.Key == ConsoleKey.I)
            {
                Console.WriteLine(@$"You Healed with {AllPets[petindex].Name} for {Medicine.Integer}");
                m.invoke(AllPets, petindex);
                Thread.Sleep(1000);
                loadPet();
            }
            else if (k.Key == ConsoleKey.J)
            {
                Random rnd = new Random();
                int num = rnd.Next(5);
                if (num == 1 || num == 3 || num == 5)
                {
                    AllPets[petindex].Bond += 5;
                    Console.WriteLine($"You Spoke to Kind Words to {AllPets[petindex].Name} You Built Your Bond By 5!");
                    AllPets[petindex].standardsound();
                    Thread.Sleep(2000);
                    loadPet();
                }
                else if (num == 2 || num == 4)
                {
                    AllPets[petindex].Bond -= 3;
                    Console.WriteLine($"You Spoke to {AllPets[petindex].Name} But They Didn't Like It! Your Bond Has Decreased...");
                    Thread.Sleep(2000);
                    loadPet();
                }
            }
            else if (k.Key == ConsoleKey.L)
            {
                Console.WriteLine(@$"You Fussed {AllPets[petindex].Name}");
                AllPets[petindex].Bond += 10;
                Thread.Sleep(1000);
                loadPet();
            }
            if (k.Key == ConsoleKey.M)
            {
                Console.WriteLine("Turning On The Heating...");
                heating = 1;
                Thread.Sleep(2000);
                loadPet();
            }
            else if (k.Key == ConsoleKey.N)
            {
                Console.WriteLine("Turning Off The Heating...");
                heating = 0;
                Thread.Sleep(2000);
                loadPet();
            }

        }

        public static void GameMenu(int index, int invindex, int shopindex)
        {
            for (; ; )
            { 
                lock (ConsoleLock)
                {
                    if (AllPets[petindex].Health != 0)
                    {
                        Console.SetCursorPosition(0, 1);
                        string topbar = $"< {AllPets[index].Name} : {AllPets[index].Type} >";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (topbar.Length / 2)) + "}", topbar));

                        Console.SetCursorPosition(0, 2);
                        string PetName = @$"Pet Name: {AllPets[index].Name}";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (PetName.Length / 2)) + "}", PetName));

                        Console.SetCursorPosition(0, 4);

                        AllPets[index].DisplayPet();

                        if (gamethreads != true)
                        {
                            gamethreads = true;
                            Thread Temp = new Thread(() => Atmosphere.temperaturechange(heating));
                            Thread Hunger = new Thread(AllPets[index].increaseHunger);
                            Thread Health = new Thread(() => AllPets[index].decreaseHealth(Atmosphere.temperature));
                            Thread Mood = new Thread(AllPets[index].decreaseMood);
                            Thread Coins = new Thread(() => player.getCoins());
                            Thread Bond = new Thread(AllPets[index].bondstats);
                            Temp.Start();
                            Health.Start();
                            Hunger.Start();
                            Mood.Start();
                            Coins.Start();
                            Bond.Start();
                        }

                        Console.SetCursorPosition(0, 17);
                        Console.WriteLine($"Temperature: {Math.Round(Atmosphere.temperature, 2)}");
                        Console.SetCursorPosition(0, 18);
                        if (AllPets[index].Health > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            displayStats(AllPets[index].Health, AllPets[index].MaxHealth, "Health ");
                        }
                        Console.SetCursorPosition(0, 19);
                        if (AllPets[index].Hunger > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            displayStats(AllPets[index].Hunger, AllPets[index].MaxHunger, "Hunger ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Hunger ");
                        }
                        Console.SetCursorPosition(0, 20);
                        if (AllPets[index].Health > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            displayStats(AllPets[index].CurrentMood, AllPets[index].MaxMood, "Mood   ");
                        }
                        Console.SetCursorPosition(0, 21);
                        if (AllPets[index].Health > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            displayStats(AllPets[index].Bond, AllPets[index].MaxBond, "Bond   ");
                        }
                        Console.SetCursorPosition(0, 23);
                        if (Console.BackgroundColor == ConsoleColor.White)
                        { 
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else if (Console.BackgroundColor == ConsoleColor.Black)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine($"Total Coins: {player.coins}               ");
                        Console.SetCursorPosition(0, 25);
                        player.Inventory.Add(items[index]);
                        string Controls = "[I] Heal Pet       [O] Feed Pet       [P] Play With Pet";
                        string Controls2 = "[J] Talk to Pet       [K] Interact with Pet       [L] Fuss Pet";
                        string Controls3 = "[N] Decrease Temperature       [M] Increase Temperature";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Controls.Length / 2)) + "}", Controls));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Controls2.Length / 2)) + "}", Controls2));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Controls3.Length / 2)) + "}", Controls3));
                    }
                    else if (AllPets[petindex].Health == 0)
                    {
                        Console.Clear();
                        TextDisplay.gameOver();
                        AllPets[index].deathsound();
                        string Death = $"{AllPets[index].Name} Has Died...";
                        string Condolences = $"We Send Our Condolences, But You're Now Exiting the Simulation...";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Death.Length / 2)) + "}", Death));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Condolences.Length / 2)) + "}", Condolences));
                        Thread.Sleep(10000);
                        exit();
                    }
                }
                Thread.Sleep(100);
            }    
            
        }

        public static void shopMenu(int index)
        {
            string shop = "--- SHOP ---";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (shop.Length / 2)) + "}", shop));
            string shopbar = $"                <-          {items[index].Name} : {items[index].Type} : Cost: {items[index].Cost}         ->                ";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (shopbar.Length / 2)) + "}", shopbar));
        }

        public static void displayInventory(int index)
        {
            string inventory = "--- INVENTORY ---";
            player.Inventory.Add(items[index]);
            string invbar = $"          <-          {player.Inventory[index].Name} : {player.Inventory[index].Type}         ->          ";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (inventory.Length / 2)) + "}", inventory));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (invbar.Length / 2)) + "}", invbar));
        }

        public static void displayStats(int statistic, int maxstatistic, string statname)
        {
            int display = statistic * 20 / maxstatistic;
            if (display < 21)
            {
                Console.Write($"{statname}");
                Console.Write(new string('█', display));
                Console.Write("                ");
            }
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
