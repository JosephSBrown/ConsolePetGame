using System;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

namespace PetGame
{
    class App
    {
        bool mute = false;
        public static List<Pet> AllPets = new List<Pet>();

        public void Run()
        {
            BackgroundProcesses bgp = new BackgroundProcesses();

            Thread bgm = new Thread(() => bgp.BackgroundMusic(mute));
            bgm.Start();

            titlemenu();
        }

        public static void titlemenu()
        {
            //Menu Options
            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("New Game", () => newPet()),
                new MenuOption("Load Game", () => Console.WriteLine("Billy")),
                new MenuOption("Options", () => Options.Menu()),
                new MenuOption("Exit", () => exit()),
            };

            int index = 0;

            createMenu(options, options[index]);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        createMenu(options, options[index]);
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        createMenu(options, options[index]);
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

        public static void createMenu(List<MenuOption> options, MenuOption selectedOption)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            TextDisplay.titleText();

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

        public static void createPetList(List<Pet> options, Pet selectedOption)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            TextDisplay.selectPet();

            foreach (Pet o in options)
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
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Get(o.GetType()).Length / 2)) + "}", Get(o.GetType())));
            }
        }


        public static void newPet()
        {
            Console.Clear();

            List<Pet> pets = GetClassType<Pet>();
            int index = 0;

            createPetList(pets, pets[index]);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
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

            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    titlemenu();
                    return;
                default:
                    return;
            }
        }

        public static void loadPet()
        {
            Console.Clear();

            int index = 0;

            foreach (Pet o in AllPets)
            {
                if (o == AllPets[index])
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (o.Name.Length / 2)) + "}", o.Name));
            }
            
            string PetName = $"Pet Name: {AllPets[index].Name}";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (PetName.Length / 2)) + "}", PetName));

            AllPets[index].DisplayPet();
        }

        public static void exit()
        {
            Environment.Exit(0);
        }

        private string infoGet(string info)
        {
            Console.Write(info);
            return Console.ReadLine();
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
