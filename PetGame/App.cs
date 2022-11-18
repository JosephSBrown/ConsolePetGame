using System;
using System.Collections.Generic;
using System.Threading;

namespace PetGame
{
    class App
    {
        bool mute = false;

        public void Run()
        {
            BackgroundProcesses bgp = new BackgroundProcesses();

            Thread bgm = new Thread(() => bgp.BackgroundMusic(mute));
            bgm.Start();

            titlemenu();
        }

        public bool titlemenu()
        {
            string TitleGraphic = $@"
                                   ____  __                                 ____       __ 
                                  / __ \/ /_  ____________  __________     / __ \___  / /_
                                 / / / / __ \/ ___/ ___/ / / / ___/ _ \   / /_/ / _ \/ __/
                                / /_/ / /_/ (__  ) /__/ /_/ / /  /  __/  / ____/  __/ /_  
                                \____/_.___/____/\___/\__,_/_/   \___/  /_/    \___/\__/  

                                     _____ _                 __      __            
                                    / ___/(_)___ ___  __  __/ /___ _/ /_____  _____
                                    \__ \/ / __ `__ \/ / / / / __ `/ __/ __ \/ ___/
                                   ___/ / / / / / / / /_/ / / /_/ / /_/ /_/ / /    
                                  /____/_/_/ /_/ /_/\__,_/_/\__,_/\__/\____/_/     
                                                 



";

            //Menu Options
            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("New Game", () => newPet()),
                new MenuOption("Load Game", () => Console.WriteLine("Billy")),
                new MenuOption("Options", () => optionMenu()),
                new MenuOption("Exit", () => exit()),
            };

            int index = 0;

            createMenu(options, options[index], TitleGraphic);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        createMenu(options, options[index], TitleGraphic);
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        createMenu(options, options[index], TitleGraphic);
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    options[index].selected.Invoke();
                }
            }
            while (key.Key != ConsoleKey.Escape);

            return true;

        }

        public static void createMenu(List<MenuOption> options, MenuOption selectedOption, string graphic)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.Write(graphic);

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


        public void newPet()
        {
            Console.Clear();

            string name = infoGet("Insert Your Pet's Name...");

            Pet pet = new Axolotl();
            pet.Name = name;
            pet.DisplayPet();

            Console.WriteLine($"Here is your new pet, {pet.Name} the {pet.Type}");

            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    titlemenu();
                    return;
                default:
                    return;
            }
        }


        public void exit()
        {
            Environment.Exit(0);
        }

        private string infoGet(string info)
        {
            Console.Write(info);
            return Console.ReadLine();
        }

        public bool optionMenu()
        {
            Console.Clear();

            Console.Write(@"

                                                 ____        __  _                 
                                                / __ \____  / /_(_)___  ____  _____
                                               / / / / __ \/ __/ / __ \/ __ \/ ___/
                                              / /_/ / /_/ / /_/ / /_/ / / / (__  ) 
                                              \____/ .___/\__/_/\____/_/ /_/____/  
                                                  /_/                              

    ");

            Console.WriteLine("Press Escape to Return");

            var key = Console.ReadKey().Key;

            bool space = false;

            switch (key)
            {
                case ConsoleKey.Spacebar:
                    if (!space)
                    {
                        mute = true;
                        space = true;
                        Console.Write("ahhhh");
                    }
                    else
                    {
                        mute = false;
                        space = true;
                    }
                    return true;
                case ConsoleKey.Escape:
                    titlemenu();
                    return true;
                default:
                    return true;
            }

        }
    }
}
