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
        public static Atmosphere venv = new Atmosphere(21.00);  //Declaring a New Virtual Environment and Setting the Default Temperature

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
                else if (key.Key == ConsoleKey.DownArrow)       //If Down Arrow Key is Pressed
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
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o.option.Length / 2)) + "}", o.option));  //Write the Menu Option to the Line based on the Above
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
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Get(o.GetType()).Length / 2)) + "}", Get(o.GetType()))); //Write the Option to Line based on the Above Using Get Method
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
                    if (index - 1 >= 0)             //If the Index - 1 is More Than or Equal to 0
                    {       
                        index--;                    //Decrement Operator to Take 1 off the Value
                        createPetList(pets, pets[index]);   //Draw the List of Pets using the Pet List and Current Menu Index
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)   //If Keyboard Down Arrow is Pressed
                {
                    if (index + 1 < pets.Count)             //If the Menu Index Plus 1 Is Less than the Number of Pets in the Pet List
                    {
                        index++;                            //Increment Operator to Add 1 to the Menu Index
                        createPetList(pets, pets[index]);   //Draw Pet List with the Cursor to the Current Index of the Menu
                    }
                }
                else if (key.Key == ConsoleKey.Enter)       //If Keyboard Enter is Pressed
                {
                    break;                                  //Break the Loop
                }
            }
            while (key.Key != ConsoleKey.Escape);           //Keep Completing the Above in the Do Loop While User Hasn't Pressed Escape

            Console.Clear();    //Clear the Console of the Menu

            TextDisplay.selectPet();    //Draw the Select Pet title referenced from the Text Display Class

            pets[index].standardsound();    //Play the Sound of the Pet Based on the Index of the Menu , Play the Dedicated Sound in the Child Pets Override Method
            pets[index].DisplayPet();       //Draw the Pet from the same Index Value from the Child Pets Draw Pet Override Method

            string naming = "What Would You Like Your Pet to be Called? ";      //String with User Input Question ready for String Format Centralising
            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (naming.Length / 2)) + "}", naming)); //Write Line Using String Format to Centralise User Input Question and User Input in the Center of the Window
            string name = Console.ReadLine();   //Read Line to get User Input for Pet Name

            AllPets.Add(pets[index]);   //Add the Menu Indexed Pet to the Players Pet List
            AllPets.Last().Name = name; //As each New Pet will be the Last Added to the List, Choose the Last Pet in the List and Assign the Name Property as User Input Name

            loadPet();  //Load the Pet Simulation Game
        }

        //Start of Method to Load the Primary Pet Game
        public static void loadPet()
        {            
            Console.Clear();    //Clear the Console to Start Fresh

            Thread Game = new Thread(() => GameMenu(petindex));    //Load a new Thread with the GameMenu Method Inside
            Game.Start();   //Start the Game Thread

            ConsoleKeyInfo k = Console.ReadKey();   //ConsoleKeyInfo Declaration of Read Key to Get User Input

            if (k.Key == ConsoleKey.LeftArrow)  //If Keyboard key Pressed is Left Arrow
            {
                if (petindex - 1 >= 0)      //if the pet index minus 1 is more than equal to 0
                {
                    petindex--;         //Decrement the Pet Index by 1
                    loadPet();          //Reload the Load Pet Method
                }
            }
            else if (k.Key == ConsoleKey.RightArrow)    //If Keyboard Key pressed if Right Arrow
            {
                if (petindex + 1 < AllPets.Count)   //If Pet Index + 1 is Less than the Number of Pet Objects in the Pet List
                {
                    petindex++; //Increment Pet Index by 1
                    loadPet();  //Reload the Load Pet Method
                }
            }
            else if (k.Key == ConsoleKey.P) //If Keyboard Key Pressed Is P
            {
                Console.WriteLine(@$"You Played with {AllPets[petindex].Name} with a {b.Name}"); //Let the User Know the Action Performed, Playing with Pet and Item Used
                b.invoke(AllPets, petindex);    //Use the Item and Have the Item's Effects on the Current Pet Index
                Thread.Sleep(1000); //Pause for 1 Seconds before Reloading the Load Pet Method
                loadPet();  //Reload the Load Pet Method
            }
            else if (k.Key == ConsoleKey.O) //If Keyboard Key Pressed Is O
            {
                Console.WriteLine(@$"You Fed {AllPets[petindex].Name} some {c.Name}");  //Let the User Know the Action Performed, Feeding Pet and Food Fed
                c.invoke(AllPets, petindex);    //Use the Item and Have the Item's Effects on the Current Pet Index
                AllPets[petindex].eatingsound(); //Play the Eating Sound as The Pet is Eating
                Thread.Sleep(2000); //Pause for 2 Seconds before Reloading the Load Pet Method
                loadPet();  //Reload the Load Pet Method
            }
            else if (k.Key == ConsoleKey.I) //If Keyboard Key Pressed Is I
            {
                Console.WriteLine(@$"You Healed with {AllPets[petindex].Name} for {Medicine.Integer}"); //Let the User Know the Action Performed, Healing Pet and For How Much
                m.invoke(AllPets, petindex);    //Use the Item and Have the Item's Effects on the Current Pet Index
                Thread.Sleep(1000); //Pause for 1 Seconds before Reloading the Load Pet Method
                loadPet();  //Reload the Load Pet Method
            }
            else if (k.Key == ConsoleKey.J) //If Keyboard Key Pressed Is J
            {
                Random rnd = new Random();              //Create a New Instance of Random
                int num = rnd.Next(5);                  //Set the Int to a Random Number in the Range of 5
                if (num == 1 || num == 3 || num == 5)   //If Random Number is One of the 3 Odd Numbers
                {
                    AllPets[petindex].Bond += 5;        //Increase the Bond with Your Pet
                    Console.WriteLine($"You Spoke to Kind Words to {AllPets[petindex].Name} You Built Your Bond By 5!");    //Let the User Know the Action Performed, Talking to Pet and How Much Your Bond has Increased By
                    AllPets[petindex].standardsound();      //As you're talking to the Pet and it likes it Play the Current Pet's Noise so it Speaks Back
                    Thread.Sleep(2000); //Pause for 2 Seconds before Reloading the Load Pet Method
                    loadPet();  //Reload the Load Pet Method
                }
                else if (num == 2 || num == 4)  //If the Random number is an Even Number
                {
                    AllPets[petindex].Bond -= 3;        //Minus from the Player's Bond with the Pet as the Pet didn't enjoy it
                    Console.WriteLine($"You Spoke to {AllPets[petindex].Name} But They Didn't Like It! Your Bond Has Decreased..."); //Let the User Know the Action Performed, And that the pet wasn't liking it and Your Bond has Decreased
                    Thread.Sleep(2000); //Pause for 2 Seconds before Reloading the Load Pet Method
                    loadPet();  //Reload the Load Pet Method
                }
            }
            else if (k.Key == ConsoleKey.K) //If Keyboard Key Pressed Is L
            {
                Console.WriteLine(@$"You Had a Fun Interaction with {AllPets[petindex].Name}"); //Let the User Know the Action Performed, Fussing the Pet
                AllPets[petindex].Bond += 10;   //Interacting with the Pet Grows the Pet at the Current Pet Index's Bond
                AllPets[petindex].Health += 10; //Interacting with the Pet Increase the Pet at the Current Pet Index's Health
                AllPets[petindex].CurrentMood += 15; //Interacting with the Pet Raises the Pet at the Current Pet Index's Mood
                Thread.Sleep(1000); //Pause for 1 Seconds before Reloading the Load Pet Method
                loadPet();  //Reload the Load Pet Method
            }
            else if (k.Key == ConsoleKey.L) //If Keyboard Key Pressed Is L
            {
                Console.WriteLine(@$"You Fussed {AllPets[petindex].Name}"); //Let the User Know the Action Performed, Fussing the Pet
                AllPets[petindex].Bond += 10;   //Fussing the Pet Grows the Pet at the Current Pet Index's Bond
                Thread.Sleep(1000); //Pause for 1 Seconds before Reloading the Load Pet Method
                loadPet();  //Reload the Load Pet Method
            }
            if (k.Key == ConsoleKey.M)  //If Keyboard Key Pressed Is M
            {
                Console.WriteLine("Turning On The Heating..."); //Let the User Know the Action Performed, Turning on the Heating
                heating = true;     //Setting Heating On to True, Effectively Turning On the Heating
                Thread.Sleep(2000); //Pause for 2 Seconds before Reloading the Load Pet Method
                loadPet();  //Reload the Load Pet Method
            }
            else if (k.Key == ConsoleKey.N) //If Keyboard Key Pressed Is N
            {
                Console.WriteLine("Turning Off The Heating...");    //Let the User Know the Action Performed, Turning off the Heating
                heating = false;    //Set Heating On to False, Effectively Turning Off the Heating
                Thread.Sleep(2000); //Pause for 2 Seconds before Reloading the Load Pet Method
                loadPet();  //Reload the Load Pet Method
            }

        }

        //Start Game Menu, the Big Game Thread, Entire Game Basically Runs out of this Method
        public static void GameMenu(int index) //requires the Pet Index Parameter
        {
            for (; ; )  //Infinite For Loop to Allow the Thread to Run Infinitely
            { 
                lock (ConsoleLock)  //Lock the Thread to Ensure No Other Thread Can Access It and The Position inside can't be Changed
                {
                    if (AllPets[petindex].Health != 0)  //If the Pet's Health Is Not 0
                    {
                        Console.SetCursorPosition(0, 1);    //Set the Cursor Position to the First Line
                        string topbar = $"< {AllPets[index].Name} : {AllPets[index].Type} >";   //Create a String Variable for String Formatting With Pet's Name and Type
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (topbar.Length / 2)) + "}", topbar));  //Write the String Variable in the Center of the Window

                        Console.SetCursorPosition(0, 2);    //Set the Cursor Position to the Line 2
                        string PetName = @$"Pet Name: {AllPets[index].Name}";   //Create a String Variable for String Formatting with the Pet's Name
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (PetName.Length / 2)) + "}", PetName));    //Write the String Variable in the Center of the Window

                        Console.SetCursorPosition(0, 4);    //Set the Cursor Position to the Line 4

                        AllPets[index].DisplayPet();    //Draw the Pets ASCII art based on the current pet choice

                        if (gamethreads != true)    //if the gamethreads have not been started yet
                        {
                            gamethreads = true;     //set the gamethread started to true, these will no longer be able to be accessed, stopping the threads from being repeatedly started
                            Thread Temp = new Thread(temperaturechange);    //Create a Thread for Changing the Temperature
                            Thread Hunger = new Thread(AllPets[index].increaseHunger);  //Create a Thread for Increasing the Pet's Hunger
                            Thread Health = new Thread(() => AllPets[index].decreaseHealth(venv.temperature)); //Create a Thread for Decreasing the Pet's Health using the Temperature as a Parameter as This Effects the Pet's Health
                            Thread Mood = new Thread(AllPets[index].decreaseMood);  ////Create a Thread for Decreasing the Pet's Mood Over Time
                            Thread Coins = new Thread(() => player.getCoins());     //Create a Thread for the Player Earning Coins Every Update
                            Thread Bond = new Thread(AllPets[index].bondstats);     //Create a Thread for the Player and Pet Bond to Decrease Overtime
                            Temp.Start();   //Start the Temperature Changing Thread
                            Health.Start(); //Start the Health Decreasing Thread
                            Hunger.Start(); //Start the Hunger Increasing Thread
                            Mood.Start();   //Start the Mood Changing Thread
                            Coins.Start();  //Start the Thread to Earn Players Coins
                            Bond.Start();   //Start the Bond Decreasing Thread
                        }

                        Console.SetCursorPosition(0, 17);   //Set the Console Cursor Position to Line 17
                        Console.WriteLine($"Temperature: {Math.Round(venv.temperature, 2)}        ");   //Write the Temperature to Line At It's Current Variable, Using Math Round to Ensure the Double Keeps it to 2 Decimal Places
                        Console.SetCursorPosition(0, 18);   //Set the Console Cursor Position to Line 18
                        if (AllPets[index].Health > 0)  //If the Pet's Health is Greater than 0
                        {
                            Console.ForegroundColor = ConsoleColor.Red; //Set the Text Colour to Red
                            displayStats(AllPets[index].Health, AllPets[index].MaxHealth, "Health ");//Display Stats as a Bar using Display Stats Method
                        }
                        Console.SetCursorPosition(0, 19);   //Set the Console Cursor Position to Line 19
                        if (AllPets[index].Hunger > 0)  //If the Pets Hunger is Greater than 0
                        {
                            Console.ForegroundColor = ConsoleColor.Green;   //Set the Text Colour to Green
                            displayStats(AllPets[index].Hunger, AllPets[index].MaxHunger, "Hunger ");   //Display Stats as a Bar using Display Stats Method
                        }
                        else
                        {                                                   //Otherwise
                            Console.ForegroundColor = ConsoleColor.Green;   //Set Text as Green
                            Console.WriteLine("Hunger ");                   //Write Hunger with No Bar to Line
                        }
                        Console.SetCursorPosition(0, 20);   //Set the Console Cursor Position to Line 20
                        if (AllPets[index].Health > 0)  //If Health is Greater than 0
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;    //Set the Text Colour to Blue
                            displayStats(AllPets[index].CurrentMood, AllPets[index].MaxMood, "Mood   ");    //Display Stats as a Bar using Display Stats Method
                        }
                        Console.SetCursorPosition(0, 21);   //Set the Console Cursor Position to Line 21
                        if (AllPets[index].Health > 0)  //If Health is Greater than 0
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;    //Set the Text Colour to Dark Cyan
                            displayStats(AllPets[index].Bond, AllPets[index].MaxBond, "Bond   ");   //Display Stats as a Bar using Display Stats Method
                        }
                        Console.SetCursorPosition(0, 23);   //Set the Console Cursor Position to Line 23
                        //When Writing Everything after the Bar Display
                        if (Console.BackgroundColor == ConsoleColor.White)  //If the Current Background Colour is White
                        { 
                            Console.ForegroundColor = ConsoleColor.Black;   //Set the Text Colour to Black
                        }
                        else if (Console.BackgroundColor == ConsoleColor.Black) //If the Current Background Colour is White
                        {
                            Console.ForegroundColor = ConsoleColor.White;   //Set the Text Colour to White
                        }
                        Console.WriteLine($"Total Coins: {player.coins}               ");       //Write Player Coins to Line
                        Console.SetCursorPosition(0, 25);   //Set the Console Cursor Position to Line 25
                        player.Inventory.Add(items[index]); //Add Items at Index to Player's Inventory
                        string Controls = "[I] Heal Pet       [O] Feed Pet       [P] Play With Pet";        //String Variable Detailing the First Line of Controls
                        string Controls2 = "[J] Talk to Pet       [K] Interact with Pet       [L] Fuss Pet";    //String Variable Detailing the Second Line of Controls
                        string Controls3 = "[N] Decrease Temperature       [M] Increase Temperature";   //String Variable Detailing the Third Line of Controls
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Controls.Length / 2)) + "}", Controls));      //Write the First Line of Controls in the Center of the Window
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Controls2.Length / 2)) + "}", Controls2));    //Write the Second Line of Controls in the Center of the Window
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Controls3.Length / 2)) + "}", Controls3));    //Write the Third Line of Controls in the Center of the Window
                    }
                    else if (AllPets[petindex].Health == 0) //If the Pet's Health is Equal to 0
                    {
                        Console.Clear();    //Clear the Console
                        TextDisplay.gameOver();     //Display the GameOver Text Referenced from the Text Display File
                        AllPets[index].deathsound();    //Play the Current Pet's Death Sound
                        string Death = $"{AllPets[index].Name} Has Died..."; //String ready for String Formatting about the death of the pet
                        string Condolences = $"We Send Our Condolences, But You're Now Exiting the Simulation..."; //String ready for String Formatting about the Game Ending
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Death.Length / 2)) + "}", Death));    //Draw the Death String in the Center of the Window
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Condolences.Length / 2)) + "}", Condolences));    //Draw the End of Game String in the Center of the Window
                        Thread.Sleep(10000);    //Pause for 10 Seconds to Allow the Player to Read and the Sound to Play before exiting
                        exit(); //Exit the Game Environment
                    }
                }
                Thread.Sleep(100); //Pause the Thread for 1 Tenth of a Second Before Refreshing Again
            }    
            
        }

        //Reusable Method to Display Statistics as Bars of Health Rather Than Numbers
        public static void displayStats(int statistic, int maxstatistic, string statname)   //Requires the Pet Stat, the Pet's Maximum for This Statistic and Requires a String detailing the Statistic
        {
            int display = statistic * 20 / maxstatistic;    //display calculation is current stat multiplied by 20 divided by Max, All Maxes are 100 So Each Block is Equal to 5 Health
            if (display < 21)   //If the blocks are Less than 21 keep drawing blocks, otherwise stop once 21 is reached
            {
                Console.Write($"{statname}");   //Write the String Detailing the Statistic
                Console.Write(new string('█', display));    //Draw the Blocks to the Amount Display is Currently Equal to
                Console.Write("                ");      //Write a Large Empty Line to Ensure that the previous health blocks before refreshing does not remain on the line during Threading
            }
        }

        //Method for Adjusting the Temperature Based on a Bool that's Changed through User Input
        public static void temperaturechange()
        {
            for (; ; )  //Infinite For Loop to Infinitely Run the Temperature Changing Method
            { 
                if (heating == true)    //If Heating is True, Meaning if the Heating is Turned On
                {
                    venv.temperature += 0.01;   //Raise the Virtual Environment Temperature by 0.01
                }
                else if (heating == false)  //If the Heating is False, Meaning the Heating is Turned Off
                {
                    venv.temperature -= 0.01;   //Lower the Virtual Environment Temperature by 0.01
                }
                Thread.Sleep(1000);     //Pause the Thread for 1 Second Before Running it Again, Update Temperature Every Second
            }
                
        }

        //Single Responsibility Principle, Method to Exit the Program
        public static void exit()
        {
            Environment.Exit(0);    //When Called Sets Environment to Immediately Exit the Code with Reason 0
        }

        //Method for getting a Class Type for Adding it to Lists
        public static List<T> GetClassType<T>() where T : class
        {
            List<T> types = new List<T>();  //Initialise New List of Classes That Are Derivatives of List Parameter T
            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                types.Add((T)Activator.CreateInstance(type));
            }
            return types;
        }

        //Method for Getting a Type and Converting it to a String
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
