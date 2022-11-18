using System;
using System.Collections.Generic;
using System.Threading;

namespace PetGame
{
    class App
    {

        public void Run()
        {
            BackgroundProcesses bgp = new BackgroundProcesses();

            Thread bgm = new Thread(new ThreadStart(bgp.BackgroundMusic));
            bgm.Start();

            titlemenu();
        }

        public bool titlemenu()
        {
            string TitleGraphic = $@"

  _____     _      _____                      
 |  __ \   | |    / ____|                     
 | |__) |__| |_  | |  __  __ _ _ __ ___   ___ 
 |  ___/ _ \ __| | | |_ |/ _` | '_ ` _ \ / _ \
 | |  |  __/ |_  | |__| | (_| | | | | | |  __/
 |_|   \___|\__|  \_____|\__,_|_| |_| |_|\___|
                                              
                                              

";

            //Menu Options
            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("New Game", () => Display()),
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
                Console.WriteLine(o.option);
            }
        }


        public void Display()
        {
            Console.Clear();
            Console.Write($@"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠞⠙⢦⠈⠀⠙⣆⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣀⣿⠀⠀⢸⠀⠀⢠⡇⠀⠳⢦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⠁⠘⣿⡀⠀⣼⠀⡴⠋⠻⡏⠀⠘⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣤⣤⣤⣤⣤⣀⣸⣇⠀⢸⣷⠞⢷⣾⡅⠀⢠⡗⠀⡼⠛⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⣤⡀⠀⣀⠀⠀⠀⠀⠀⣀⣴⠖⠋⠉⠁⠀⠀⠀⠀⠀⠀⠉⠛⢦⣿⡏⣷⣸⣿⠂⢀⣼⡷⠎⠀⢀⡾⠁⠀⠀⠀⠀⠀⠀⠀⠀
⠀⢠⡞⠳⣄⡇⠀⢱⡄⠉⢷⡀⠀⣴⠞⠋⠀⠀⠀⠀⠀⠀⠀⠀⢀⡀⠀⠀⠀⠀⣿⣧⣾⣧⣄⣦⣾⢿⣳⣶⣺⣍⣀⣀⠀⣀⣀⠀⢀⠀⠀⠀
⠀⠸⣇⠀⠘⣧⡀⠀⣧⠀⠀⣧⡾⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣷⣄⠀⠀⠀⠹⠿⢿⣿⣟⣻⡾⠁⠀⣽⠋⠀⢸⠋⠁⠀⠀⠀⠀⠀⠂
⢠⡶⠾⠧⣄⣈⣷⣷⣈⣷⣤⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣏⣻⣿⣄⠐⠖⣶⣿⣷⣿⡿⠁⢤⣾⠃⢀⡴⠿⢾⠉⠉⡿⠁⠀⡠⠂
⠈⢧⡤⢤⣤⡖⠻⣯⡟⢿⣿⣲⣷⠀⠀⠀⢀⣤⣤⣤⠤⠾⠛⠀⠀⠀⠉⠅⠘⠀⠀⠀⠈⠙⣛⣿⣷⣾⡿⠿⠍⢁⣀⣠⠏⠀⡾⠁⠀⡄⠀⠀
⠀⠘⣇⠀⠙⢷⡠⠾⣗⢸⣿⡿⢿⡆⠠⠴⠞⠷⠿⠿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣉⠘⠙⢷⣄⠐⣾⠀⠀⠀⢰⠃⠀⠀⡁⠀⠀
⠀⡤⠞⠳⠦⠈⣳⣄⣹⣾⣿⠻⠾⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡞⣿⠳⢦⣄⣀⠀⢀⡾⠃⠙⡷⢶⠾⠉⠉⠁⠀⠀⠀⢸⡀⠀⠀⣇⠀⠀
⠀⠳⣄⣀⣠⠼⠻⣯⡛⢻⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣧⡈⠇⠀⠈⠛⠛⠟⠓⠒⠺⢷⣤⡀⠀⠀⠀⠀⠀⠀⠈⣇⠀⠀⢸⠀⠀
⠀⠀⢰⠎⠻⢦⣄⣈⣹⣿⣿⢿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⢦⡄⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠓⠒⠶⢤⡀⠀⠀⣿⠀⠀⢸⡇⠀
⠀⠀⠈⠓⠢⡔⠋⢹⠋⢀⣉⣮⡏⠛⣶⣤⣄⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣬⠇⠀⣸⠇⠀⠀⣸⠁⠀
⠀⠀⠀⠀⠀⠁⠀⠘⠶⠞⠉⠋⠀⠸⣯⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⠞⢉⣠⡴⠚⠉⠀⠀⣰⠇⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠒⠦⠤⠤⠤⠤⢤⣤⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⡏⠉⠉⠉⠀⠀⠀⠀⣠⡼⠁⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠳⢤⣤⣀⠀⠀⠀⠀⠀⠀⣋⣀⣙⣷⠦⠤⠤⠴⠶⠛⠁⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠛⠲⠶⠖⠚⠋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
");
            return;
        }

        public void exit()
        {
            Environment.Exit(0);
        }

        public bool optionMenu()
        {
            Console.Clear();

            Console.Write(@"

       ____  _____ _______ _____ ____  _   _  _____ 
      / __ \|  __ \__   __|_   _/ __ \| \ | |/ ____|
     | |  | | |__) | | |    | || |  | |  \| | (___  
     | |  | |  ___/  | |    | || |  | | . ` |\___ \ 
     | |__| | |      | |   _| || |__| | |\  |____) |
      \____/|_|      |_|  |_____\____/|_| \_|_____/ 
                                                
                                                

");

            Console.WriteLine("Press Escape to Return");

            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.Escape:
                    return true;
                default:
                    return true;
            }

        }
    }
    
}
