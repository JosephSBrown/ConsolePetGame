using System;
using System.Threading;
using System.Media;

namespace PetGame
{
    class Axolotl : Pet
    {

        private static object ConsoleLock = new object();
        public Axolotl()
        {
            Type = "Axolotl";
            MaxHealth = 100;
            Health = 80;
            MaxMood = 100;
            CurrentMood = 20;
            MaxHunger = 100;
            Hunger = 0;
            Bond = 0;
            MaxBond = 100;
            preferredtemperature = 20;
        }

        public override void DisplayPet()
        {
            string one =      $@"                          #***(                                                   ";
            string two =      $@"                          #****#                                                  ";
            string three =    $@"                           #**(# ***#                                             ";
            string four =     $@"                  #.......   ...#(((  ***(                                        ";
            string five =     $@"                #............... ...#  (((#                   .  #####            ";
            string six =      $@"     #*,*#     #.......................#(#  *,*              ### ...#(            ";
            string seven =    $@"     #*****(###..........................*#(**# ....       # ....#,#              ";
            string eight =    $@"       #((((##...................####,...#        .   # # .....#,#                ";
            string nine =     $@"            ##,......# ..................#   #####/## .......#,,                  ";
            string ten =      $@"       #****( ,,.....###...#...........#.....   ..........,,#, .                  ";
            string eleven =   $@"             #(,,,..,,,............,#...................#..#                      ";
            string twelve =   $@"           ,**(#  ##,,,,,,,,,,,##,,,..,,.#..............,.....#                   ";
            string thirteen = $@"                     #,,,.###.........#,.##....,.,...##,,,,#,,#                   ";
            string fourteen = $@"                      #,.#...#.........,,#,......,,,,##,,,,,#                     ";
            string fifteen =  $@"                              .#/,,,,,,,,,,,,,,,,,#.. ..                          ";
            string sixteen =  $@"                                   ......#####,....                               ";

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
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (thirteen.Length / 2)) + "}", thirteen));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (fourteen.Length / 2)) + "}", fourteen));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (fifteen.Length / 2)) + "}", fifteen));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (sixteen.Length / 2)) + "}", sixteen));
        }

        public override void decreaseHealth(double temperature)
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (temperature > preferredtemperature + 1)
                    {
                        Health -= 2;
                    }
                    else if (temperature < preferredtemperature - 1)
                    {
                        Health -= 1;
                    }

                    if (Health == MaxHealth || Health > MaxHealth)
                    {
                        Health += 0;
                        Health -= 0;
                        Health = 100;
                    }
                    else if (Health + 2 > MaxHealth)
                    {
                        Health = 100;
                    }
                    else if (Health >= 70 && Health < 100)
                    {
                        Health -= 1;
                    }
                    else if (Health > 40 && Health < 70)
                    {
                        Health -= 3;
                    }
                    else if (Health > 1 && Health < 40)
                    {
                        Health -= 5;
                    }
                    else if (Health < 0 || Health == 0)
                    {
                        Health -= 0;
                        Health = 0;
                    }
                }
                Thread.Sleep(3000);
            }


        }

        public override void increaseHunger()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    Hunger += 2;
                    if (Hunger == MaxHunger || Hunger > MaxHunger)
                    {
                        Hunger += 0;
                        Hunger -= 0;
                        Hunger = 100;
                    }
                    else if (Hunger + 2 > MaxHunger)
                    {
                        Hunger = 100;
                    }
                    else if (Hunger >= 70 && Hunger < 99)
                    {
                        Hunger += 5;
                    }
                    else if (Hunger > 40 && Hunger < 70)
                    {
                        Hunger += 3;
                    }
                    else if (Hunger < 0)
                    {
                        Hunger -= 0;
                        Hunger += 0;
                        Hunger = 0;
                    }
                }
                Thread.Sleep(3000);
            }
        }

        public override void decreaseMood()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (CurrentMood == MaxMood || CurrentMood > MaxMood)
                    {
                        CurrentMood += 0;
                        CurrentMood -= 0;
                        CurrentMood = 100;
                    }
                    else if (CurrentMood + 2 > MaxMood)
                    {
                        CurrentMood = 100;
                    }
                    else if (CurrentMood >= 70 && CurrentMood < 99)
                    {
                        CurrentMood -= 1;
                    }
                    else if (CurrentMood > 40 && CurrentMood < 70)
                    {
                        CurrentMood -= 2;
                    }
                    else if (CurrentMood > 1 && CurrentMood < 40)
                    {
                        CurrentMood -= 3;
                    }
                    else if (CurrentMood < 0 || CurrentMood == 0 || CurrentMood - 3 < 0)
                    {
                        CurrentMood += 0;
                        CurrentMood -= 0;
                        CurrentMood = 0;
                    }
                }
                Thread.Sleep(5000);
            }
        }

        public override void bondstats()
        {
            for (; ; )
            {
                lock (ConsoleLock)
                {
                    if (Bond > 15)
                    {
                        Bond -= 3;
                    }
                    else
                    {
                        Bond -= 0;
                        Bond += 0;
                    }
                }
                Thread.Sleep(10000);
            }
        }

        public override void standardsound()
        {
            SoundPlayer player = new SoundPlayer("dolphinstandard.wav");
            player.Load();
            player.Play();
        }
    }
}
