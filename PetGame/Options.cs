using System;
using System.Collections.Generic;

namespace PetGame
{
    //Start of Options Class
	class Options
	{
        //Menu as a Boolean Method
        public static bool Menu()
        {

            //Menu Options for the Usable Options
            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("Mute", () => MuteSound()),  //If Mute Selected Mute Music
                new MenuOption("Unmute", () => UnmuteSound()),  //If Unmute Selected Play Music
                new MenuOption("Return to Title Screen", () => App.titlemenu()),    //Go Back to the Main Menu
            };

            int index = 0;  //Menu Index

            createOptionMenu(options, options[index]);  //Draw Options Menu with the Options Above and the Options Index

            ConsoleKeyInfo key;     //Unassigned ConsoleKeyInfo Variable

            do
            {
                key = Console.ReadKey();    //Assign ReadKey to key

                if (key.Key == ConsoleKey.UpArrow)  //If Up Arrow Key is Pressed
                {
                    if (index - 1 >= 0) //If Menu Index Minus 1 is Greater than Equal to 0
                    {
                        index--;        //Decrement Operator to Decrease the Menu Index Value By 1
                        createOptionMenu(options, options[index]);      //Redraw the Menu with the New Index and Display Cursor In New Place
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)   //If Down Arrow Key is Pressed
                {
                    if (index + 1 < options.Count)  //If Menu Index Plus 1 is Less than the Number of Objects in the Options List
                    {
                        index++;                    //Increment Operator to Increase the Menu Index By 1
                        createOptionMenu(options, options[index]);  //Redraw the Menu with the New Index and Display Cursor in the New Place
                    }
                }
                else if (key.Key == ConsoleKey.Enter)       //If Key Pressed is Enter
                {
                    options[index].selected.Invoke();       //Invoke the Action Listed in the Options Menu
                }
            }
            while (key.Key != ConsoleKey.Escape);           //Continue to do this in a Loop While User Has Not Pressed the Escape Key

            return true;                                    //Menu's a Boolean so needs to Return True to ensure reachable code
        }

        public static void createOptionMenu(List<MenuOption> options, MenuOption selectedOption)
        {
            Console.BackgroundColor = ConsoleColor.Black;       //Set the Background Colour to Black
            Console.ForegroundColor = ConsoleColor.White;       //Set the Foreground Colour to White

            Console.Clear();                                    //Clear the Console Everytime the Menu is Redrawn

            TextDisplay.optionText();                           //Display the Options Menu Text Referenced from the TextDisplay Class

            foreach (MenuOption o in options)                   //For Each Option Object Provided in the List, Iterates through all available objects in the provided list
            {
                if (o == selectedOption)                        //if the specific option is the same as the option currently active in the Option Index
                {
                    Console.BackgroundColor = ConsoleColor.White;       //Make the Background Colour White to Show the Selected Option
                    Console.ForegroundColor = ConsoleColor.Black;       //Make the Text Colour Black to Contrast the White Background appearing like a Cursor
                }
                else
                {                                                       //For All Other Options That Are Not The Selected Index
                    Console.BackgroundColor = ConsoleColor.Black;       //Make the Background of the Option Black
                    Console.ForegroundColor = ConsoleColor.White;       //Make the Text Colour a Contrasting White
                }
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o.option.Length / 2)) + "}", o.option));  //Write the Menu Option to the Line based on the Above Specified Principle
            }                                                                                                                   //String Format Allows the Text to Be Written in the Center of the Screen
        }

        //Method to Mute Sound
        private static void MuteSound()
        {

        }

        //Method to Unmute Sound
        private static void UnmuteSound()
        {

        }
    }
}
