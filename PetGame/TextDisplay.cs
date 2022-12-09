using System;

namespace PetGame
{
    //Start of the Text Display Class, Each Method Dsiplays a Central Text Image through ASCII
    class TextDisplay
    {
        //Method to Display the Title Screen Text
        public static void titleText()
        {
            //The Following Strings Represent ASCII for the Obscure Pet Section of the Title Menu 
            string one =   @$"           ____  __                                 ____       __          ";
            string two   = @$"          / __ \/ /_  ____________  __________     / __ \___  / /_         ";
            string three = @$"         / / / / __ \/ ___/ ___/ / / / ___/ _ \   / /_/ / _ \/ __/         ";
            string four  = @$"        / /_/ / /_/ (__  ) /__/ /_/ / /  /  __/  / ____/  __/ /_           ";
            string five  = $@"        \____/_.___/____/\___/\__,_/_/   \___/  /_/    \___/\__/           ";

            //The Following Strings Represent ASCII for the Simulator Section of the Title Menu 
            string six   = $@"       _____ _                 __      __                  ";
            string seven = @$"      / ___/(_)___ ___  __  __/ /___ _/ /_____  _____      ";
            string eight = @$"      \__ \/ / __ `__ \/ / / / / __ `/ __/ __ \/ ___/      ";
            string nine  = @$"     ___/ / / / / / / / /_/ / / /_/ / /_/ /_/ / /          ";
            string ten   = $@"    /____/_/_/ /_/ /_/\__,_/_/\__,_/\__/\____/_/           ";
                     
            //The Following Write Lines Use the String Format to Centralise the Above String in the Center of the Window
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (six.Length / 2)) + "}", six));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (seven.Length / 2)) + "}", seven));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (eight.Length / 2)) + "}", eight));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (nine.Length / 2)) + "}", nine));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (ten.Length / 2)) + "}", ten));
            Console.WriteLine($@"


");
        }

        public static void optionText()
        {
            //The Following Strings Represent ASCII for the Options Title of the Options Menu 
            string one   = @$"       ____        __  _                     ";
            string two   = @$"      / __ \____  / /_(_)___  ____  _____    ";
            string three = @$"     / / / / __ \/ __/ / __ \/ __ \/ ___/    ";
            string four  = @$"    / /_/ / /_/ / /_/ / /_/ / / / (__  )     ";
            string five  = @$"    \____/ .___/\__/_/\____/_/ /_/____/      ";
            string six   = @$"        /_/                                  ";

            //The Following Write Lines Use the String Format to Centralise the Above String in the Center of the Window
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (six.Length / 2)) + "}", six));
            Console.Write($@"


");
        }

        public static void selectPet()
        {
            //The Following Strings Represent ASCII for the Select a Pet Title of the New Pet Menu 
            string one =    $@"   _____      __          __              ____       __  ";
            string two =    $@"  / ___/___  / /__  _____/ /_   ____ _   / __ \___  / /_ ";
            string three =  $@"  \__ \/ _ \/ / _ \/ ___/ __/  / __ `/  / /_/ / _ \/ __/ ";
            string four =   $@" __ / /  __/ /  __/ /__/ /_   / /_/ /  / ____/  __/ /_   ";
            string five =   $@"/____/\___/_/\___/\___/\__/   \__,_/  /_/    \___/\__/   ";

            //The Following Write Lines Use the String Format to Centralise the Above String in the Center of the Window
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
            Console.Write($@"


");
        }

        public static void gameOver()
        {
            //The Following Strings Represent ASCII for the Game Over Title of the Pet Death Screen 
            string one =   $"     ______                        ____                    ";
            string two =   $"    / ____/___ _____ ___  ___     / __ |_   _____  _____   ";
            string three = $"   / / __/ __ `/ __ `__ |/ _ |   / / / / | / / _ |/ ___/   ";
            string four =  $"  / /_/ / /_/ / / / / / /  __/  / /_/ /| |/ /  __/ /       ";
            string five =  $"  |____/|__,_/_/ /_/ /_/|___/   |____/ |___/|___/_/        ";

            //The Following Write Lines Use the String Format to Centralise the Above String in the Center of the Window
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
            Console.Write($@"


");
        }

    }
}
