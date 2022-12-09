using System;
using System.Collections.Generic;

namespace PetGame
{
    //Starting the Item Class
    class Item : IItem  //Uses the Item Interface for Direction
    {

        public string Name { get; set; }    //String Name Variable, Will Get the Value and Set it as a Property
        public static int Integer { get; set; } //Integer Variable, Will Get the Value of the Integer and Set it as a Property
        public string Type { get; set; }    //Type Variable, Will Get the Value of the Type and Set it as a Property
        public int Cost { get; set; }       //Cost Value, Will Get the Cost of the Item and Set It as a Cost Property

        //Method Required to Use the Items, Will Be Inherited By Child Classes
        public void invoke(List<Pet> petlist, int index)    //Takes the List of Pets and the Pet Index to Know Which Pet to Apply Applications to
        {
            if (Type == "Food")     //If the Type Property is Equal to Food Complete the Following
            {
                int moodaffected = Integer / petlist[index].Hunger * petlist[index].CurrentMood; //The Mood Affected is A Calculation to Make the Affected Hunger Change Dependent on Mood
                if (moodaffected < 5) //If the Mood Affected is less than 5 Change this to 5 So Hunger Still Takes a Reasonable Effect
                {
                    moodaffected = 5;   //Set Mood Affected as 5
                }
                else if (moodaffected > Integer)    //If the Mood Affected is More than the declared Integer Property Change this to Integer as it doesn't need to be more than initially intended
                {
                    moodaffected = Integer;     //Sets moodaffected as Integer Property
                }
                petlist[index].Hunger = petlist[index].Hunger - moodaffected;   //Takes the Pet's Hunger Level and Removes the Mood Affected Int to Change Hunger Level
            }
            else if (Type == "Toy")      //If the Type Property is Equal to Toy Complete the Following
            {
                petlist[index].CurrentMood += Integer;  //Take the Current Pet's Mood and Increase it By the Integer Property
            }
            else if (Type == "Medicine")     //If the Type Property is Equal to Medicine Complete the Following
            {
                petlist[index].Health += Integer;   //Add to the Current Pet's Health the Integer Property
                petlist[index].Hunger -= 8;         //Take 8 Away from the Pet's Hunger Level as it Replenishes Part of the Pet's Hunger Too
            }
        }

    }
}