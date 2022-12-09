using System;

namespace PetGame
{
    //Start Menu Option Class
    class MenuOption
    {
        public String option { get; set; }  //Get Set Field for Menu List of Options
        public Action selected { get; set; }    //Get Set Field for the Selected Index of the Menu

        //Method for Setting the Properties
        public MenuOption(String Option, Action Invoke) //Takes the Menu Options as a Parameter as well as the Menu Index
        {
            option = Option;    //Set option Property as Option parameter
            selected = Invoke;  //Set the Selected Property as the Index Parameter
        }
    }
}