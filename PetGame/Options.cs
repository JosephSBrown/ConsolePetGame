using System;
using System.Collections.Generic;

namespace PetGame
{
	class Options
	{
        public static bool Menu()
        {

            //Menu Options
            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("Mute", () => MuteSound()),
                new MenuOption("Unmute", () => UnmuteSound()),
                new MenuOption("Return to Title Screen", () => App.titlemenu()),
            };

            int index = 0;

            createOptionMenu(options, options[index]);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        createOptionMenu(options, options[index]);
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        createOptionMenu(options, options[index]);
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

        public static void createOptionMenu(List<MenuOption> options, MenuOption selectedOption)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            TextDisplay.optionText();

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

        private static void MuteSound()
        {

        }

        private static void UnmuteSound()
        {

        }
    }
}
