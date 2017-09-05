using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManhattan
{
    /// <summary>
    /// Class that handles all the input, output  operations and interactions with the user.
    /// </summary>
    class Controller
    {
        /// <summary>
        /// Declaration of properties 
        /// </summary>
        private static int userLocationX;/// <param name="userLocationX">The X Coordinate of the user's current location </param>
        private static int userLocationY;/// <param name="userLocationY">The Y Coordinate of the user's current location </param>

        static string userInputX;/// <param name="userInputX">The X Coordinate of the user's current location input by the user </param>
        static string userInputY;/// <param name="userInputY">The Y Coordinate of the user's current location input by the user </param>

        static bool valid; /// <param name="valid">Validation Parameter </param>

        static Event e = new Event();/// <param name="e">Definition of the object of the class Event </param>
        static void Main(string[] args)
        {

            getInputFromUser();

            /*getInputFromMethod() call gets the inoput from the user and calls the validation method to carry out validation */

            e.createEventInfo(userLocationX, userLocationY);

            /*Method call to the createEventInfo() method in class Event.cs creates the list of events and their details */ 

            e.displayRecommendedEvents();
            /*Method call to the displayRecommendedEvents() in the class Event.cs displays the list of recommended events based on the lowest price and least distance */ 
#if DEBUG
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
#endif
        }


        private static void getInputFromUser()
        {
            int num = -1;/// <param name="num">Validation Parameter to Check if the input entered is Integer  </param>
            valid = false;
            while (!valid)
            {
                Console.WriteLine("\n Enter the User Location interms of X and Y Coordinates");
                userInputX = Console.ReadLine();
                if (!int.TryParse(userInputX, out num))
                {
                    Console.WriteLine("Not an integer");
                    valid = false;
                    break;
                }
                else
                {
                    userLocationX = Int32.Parse(userInputX);
                    valid = validateUserInput(userLocationX);

                }
                userInputY = Console.ReadLine();
                if (!int.TryParse(userInputY, out num))
                {
                    Console.WriteLine("Not an integer");
                    valid = false;
                    break;

                }
                else
                {
                    userLocationY = Int32.Parse(userInputY);
                    valid = validateUserInput(userLocationY);

                }
            }
        }


        private static bool validateUserInput(int userCoordinate)
        {
            if (userCoordinate >= -10 && userCoordinate <= 10)
            {
                return true;
            }
            else
            {
                Console.WriteLine("You are only allowed x and y coordinates from -10 to +10");
                return false;
            }

        }
    }
}
