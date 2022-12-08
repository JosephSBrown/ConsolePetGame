using System;
using System.Threading;

namespace PetGame
{
    class Program
    {
        //Main Program Method to Initialise Application
        static void Main(string[] args)
        {
            //New Instance of the App Class
            App app = new App();

            //Call the Beginning of the Application
            app.Run();
        }
    }
}
