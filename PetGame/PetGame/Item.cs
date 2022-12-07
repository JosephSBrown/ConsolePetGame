using System;
using System.Collections.Generic;

namespace PetGame
{
    class Item : IItem
    {

        public string Name { get; set; }
        public static int Integer { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }

        public void invoke(List<Pet> petlist, int index)
        {
            if (Type == "Food")
            {
                int moodaffected = Integer / petlist[index].Hunger * petlist[index].CurrentMood;
                if (moodaffected < 5)
                {
                    moodaffected = 5;
                }
                else if (moodaffected > Integer)
                {
                    moodaffected = Integer;
                }
                petlist[index].Hunger = petlist[index].Hunger - moodaffected;
                Console.WriteLine(moodaffected);
            }
            else if (Type == "Toy")
            {
                petlist[index].CurrentMood += Integer;
            }
            else if (Type == "Medicine")
            {
                petlist[index].Health += Integer;
                petlist[index].Hunger -= 8;
            }
        }

    }
}