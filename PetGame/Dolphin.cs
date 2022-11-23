using System;
using System.Media;
using System.Threading;

namespace PetGame
{
    class Dolphin : Pet
    {

        public override void DisplayPet()
        {
            string one   = @$"               ,-.              ";
            string two    = $@"             / ('             ";
            string three  = $@"     * _.--'!   '--._         ";
            string four   = $@"    ,'              ''.       ";
            string five   = $@"   |!                   \     ";
            string six    = $@" _.'  O      ___       ! \    ";
            string seven  = $@"(_.- ^, __..-'  ''''--.   )   ";
            string eight  = $@"    /, '        '    _.' /    ";
            string nine   = $@" '         *    .-''    |     ";
            string ten    = $@"              (..--^.  '      ";
            string eleven = $@"                    | /       ";
            string twelve = $@"                     '        ";

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
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (eleven.Length / 2)) + "}", eleven));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (twelve.Length / 2)) + "}", twelve));
            Console.Write($@"


");
        }

        public override void standardsound()
        {
            SoundPlayer player = new SoundPlayer("dolphinstandard.wav");
            player.Load();
            player.Play();
        }

    }
}