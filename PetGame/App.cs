using System;
using System.Collections.Generic;
using System.Threading;

namespace PetGame
{
    class App
    {

        public void titlemenu()
        {


            BackgroundProcesses bgp = new BackgroundProcesses();

            Thread bgm = new Thread(new ThreadStart(bgp.BackgroundMusic));
            bgm.Start();

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
                new MenuOption("New Game", () => Console.WriteLine("Billy")),
                new MenuOption("Load Game", () => Console.WriteLine("Billy")),
                new MenuOption("Options", () => Console.WriteLine("Billy")),
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

            return;

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

        public void exit()
        {
            Environment.Exit(0);
        }
    
    }

}
