using System;

namespace PetGame
{
    class MenuOption
    {

        public String option { get; set; }
        public Action selected { get; set; }

        public MenuOption(String Option, Action Invoke)
        {
            option = Option;
            selected = Invoke;
        }
    }
}