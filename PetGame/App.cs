using System;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;

namespace PetGame
{
    //Beginning of App Class
    class App
    {
        //Open Variables for the Class Accessible for All Methods within the Class
        bool mute = false;  
        public static bool heating; //Boolean for Setting the Heating On or Off
        public static List<Pet> AllPets = new List<Pet>();  //Create List to Add New Pets Too via Pet Objects
        public static object ConsoleLock = new object();    //Used to Create Thread Lock and Lock Positional Values within the Thread
        static Player player = new Player();    //Initialising New Player Instance
        static Ball b = new Ball(); //Initialising New Ball Instance
        static Medicine m = new Medicine(); //Initialising New Medicine Instance
        static Crisps c = new Crisps(); //Initialising New Crips Instance
        static public int petindex = 0; //Setting the Pet Index for When Accessing Pets and Accessing Pet Values and Statistics
        static public int shopindex = 0; //Setting the Shop Index for When Browsing the Shop
        static public int invindex = 0; //Setting the Inventory Index for When Accessing the Inventory
        public static List<Item> items = GetClassType<Item>();  //Items List, All Items deriving of the Item Class, Inheritance Included, Ready for the Shop
        static bool gamethreads = false;    //Declaring whether the Threads have already been run or not
        public static Atmosphere venv = new Atmosphere(21.00);

        public void Run()
        {
            Console.Title = "Obscure Pet Simulator";    //Assign Game Title to the Top of the Window Bar
            BackgroundProcesses bgp = new BackgroundProcesses();    //Create New Instance of Background Processes ready for the Thread

            Thread bgm = new Thread(() => bgp.BackgroundMusic(mute));   //Create a New Thread for the Background Music
            bgm.Start();    //Start the Background Music Thread


            titlemenu();    //Once the Background Music Starts Run the Title Screen
        }

        //Method to Create the Title Screen
        public static void titlemenu()
        {
            int index = 0;  //Menu Index, Referenced in createMenu(), Required to Show Where in the List the Cursor is

            // Create the Main Menu Options
            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("New Game", () => newPet()), //Start A New Game and load into the New Pet Menu
                new MenuOption("Load Game", () => createPetList(AllPets, AllPets[petindex])), //Load a Game From Saved Objects
                new MenuOption("Options", () => Options.Menu()),    //Load the Options Menu
                new MenuOption("Exit", () => exit()),   //Exit the Application
            };

            createMenu(options, options[index]); //Create the Main Menu, using the Index and Declared Options

            ConsoleKeyInfo key; //Open Variable for the Read Key in Do Loop

            do
            {
                key = Console.ReadKey(); //Assigning Read Key to Open ConsoleKeyInfo Variable

                if (key.Key == ConsoleKey.UpArrow)  //If Up Arrow Key is Pressed
                {
                    if (index - 1 >= 0)                         //If Menu Index Minus 1 is Greater than Equal to 0
                    {
                        index--;                                //Decrement Operator to Decrease the Menu Index Value By 1
                        createMenu(options, options[index]);    //Redraw the Menu with the New Index and Display Cursor In New Place
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)       //If Up Arrow Key is Pressed
                {
                    if (index + 1 < options.Count)              //If Menu Index Plus 1 is Less than the Number of Objects in the Options List
                    {
                        index++;                                //Increment Operator to Increase the Menu Index By 1
                        createMenu(options, options[index]);    //Redraw the Menu with the New Index and Display Cursor in the New Place
                    }
                }
                else if (key.Key == ConsoleKey.Enter)           //If Key Pressed is Enter
                {
                    options[index].selected.Invoke();           //Invoke the Action Listed in the Options Menu
                }
            }
            while (key.Key != ConsoleKey.Escape);               //Continue to do this in a Loop While User Has Not Pressed the Escape Key
        }

        //Method to Draw the Menu When Called
        public static void createMenu(List<MenuOption> options, MenuOption selectedOption)  //Requires a List of Options and an Option Index in order to Draw the Menu
        {
            Console.BackgroundColor = ConsoleColor.Black;   //Set the Background Colour to Black
            Console.ForegroundColor = ConsoleColor.White;   //Set the Foreground Colour to White

            Console.Clear();    //Clear the Console Everytime the Menu is Redrawn

            TextDisplay.titleText();    //Display the Title Menu Text Referenced from the TextDisplay Class

            foreach (MenuOption o in options)   //For Each Option Object Provided in the List, Iterates through all available objects in the provided list
            {
                //Open and Closed Principle
                if (o == selectedOption)        //if the specific option is the same as the option currently active in the Option Index
                {
                    Console.BackgroundColor = ConsoleColor.White;   //Make the Background Colour White to Show the Selected Option
                    Console.ForegroundColor = ConsoleColor.Black;   //Make the Text Colour Black to Contrast the White Background appearing like a Cursor
                }
                else
                {                                                   //For All Other Options That Are Not The Selected Index
                    Console.BackgroundColor = ConsoleColor.Black;   //Make the Background of the Option Black
                    Console.ForegroundColor = ConsoleColor.White;   //Make the Text Colour a Contrasting White
                }
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o.option.Length / 2)) + "}", o.option));  //Write the Menu Option to the Line based on the Above Specified Principle
                                                                                                                                //String Format Allows the Text to Be Written in the Center of the Screen
            }
        }

        //Exactly the Same as the Create Menu But Required for Making the Pets This Time Instead
        public static void createPetList(List<Pet> options, Pet selectedOption) //Requires the Pets to be passed through as the Menu Options and the Pet Index to Select the Correct Pet
        {
            Console.BackgroundColor = ConsoleColor.Black;   //Set Background as Black
            Console.ForegroundColor = ConsoleColor.White;   //Set Text Colour as White

            Console.Clear();    //Clear the Console Everytime the Method is Called

            TextDisplay.selectPet();    //Write the Pet Select Text to Screen Referenced from TextDisplay Class

            foreach (Pet o in options)  //For Each Pet Object in the Parameterised List
            {
                //Open and Closed Principle
                if (o == selectedOption)                            //If the Pet Index is Equal to the Pet Written in Console
                {
                    Console.BackgroundColor = ConsoleColor.White;   //Set Background of the Line to White
                    Console.ForegroundColor = ConsoleColor.Black;   //Set the Text Colour of the Line to Black
                }
                else
                {                                                   //For All Other Options that Don't Match Pet Index
                    Console.BackgroundColor = ConsoleColor.Black;   //Ensure Background Stays Black
                    Console.ForegroundColor = ConsoleColor.White;   //Ensure Text Colour Remains White
                }
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Get(o.GetType()).Length / 2)) + "}", Get(o.GetType()))); //Write the Option to Line based on the Above Specified Principle
            }                                                                                                                                  //String Format Applies an Equation to Ensure the Line Sits Central to the Window
        }

        //Method for Creating a New Pet
        public static void newPet()
        {
            Console.Clear();    //Clear the Console to Redraw the New Pet Menu

            List<Pet> pets = GetClassType<Pet>();   //Using a Self Made Function get Class Type Where the Type is Pet and Add them all to a list of Pets
            int index = 0;  //Select a Pet Menu Index

            createPetList(pets, pets[index]);   //Draw the Pet Menu with the List of All Pets from Pet Type and assigning the Pet Index

            ConsoleKeyInfo key; //Open ConsoleKeyInfo Variable ready for the Do Loop

            do
            {
                key = Console.ReadKey();    //Assigning Read Key to Open ConsoleKeyInfo Variable

                if (key.Key == ConsoleKey.UpArrow)  //If Keyboard Key Up Arrow is Pressed
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
                heating = true;
                Thread.Sleep(2000);
                loadPet();
            }
            else if (k.Key == ConsoleKey.N)
            {
                Console.WriteLine("Turning Off The Heating...");
                heating = false;
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
                            Thread Temp = new Thread(temperaturechange);
                            Thread Hunger = new Thread(AllPets[index].increaseHunger);
                            Thread Health = new Thread(() => AllPets[index].decreaseHealth(venv.temperature));
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
                        Console.WriteLine($"Temperature: {Math.Round(venv.temperature, 2)}        ");
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

        public static void temperaturechange()
        {
            for (; ; )
            { 
                if (heating == true)
                {
                    venv.temperature += 0.01;
                }
                else if (heating == false)
                {
                    venv.temperature -= 0.01;
                }
                Thread.Sleep(1000);
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
