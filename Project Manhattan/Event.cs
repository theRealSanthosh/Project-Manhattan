using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManhattan
{
    /// <summary>
    /// Class that handles all the operations related to Events (Random Event Location Creation, Event Info Creation, Recommending Best Events Based on Location and Price)  
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Setting Default Values During Initialization 
        /// </summary>
        int NumberOfEvents = 7;/// <param name="NumberOfEvents">The total number of events </param>
        int NumberOfTicketClasses = 5;/// <param name="NumberOfTicketClasses">The number of classes of tickets </param>
        int NumberOfEventResults = 4;/// <param name="NumberOfEventResults">The number of recommended event results to be displayed </param>
        double basePriceOfTicket = 1.00;/// <param name="basePriceOfTicket">The base price of ticket for the event </param>
        double highestPriceOfTicket = 1000.00;/// <param name="highestPriceOfTicket">The highest price of ticket for the event </param>

        /* The user can change the above values to increase or decrease the number of events, or ticket classes or event results, or the base and highest price of the ticket */

        /// <summary>
        /// Assumed Universe Values 
        /// </summary>
        public int lowerLimit = -10;/// <param name="lowerLimit">The lower limit of the range for the area to be considered </param>

        public int upperLimit = 10;/// <param name="upperLimit">The upper limit of the range for the area to be considered </param>

        /* The user can change the above values to increase or decrease the range of the area to be considered for the event search around the user's location  */

        /* The above mentioned range is considered as the range for valid user input for user location coordinates */

        private int[] EventID = new int[] { };/// <param name="EventID">Array that contains the EventID of all the events </param>
        private int[] EventXLocation = new int[] { };/// <param name="EventXLocation">Array that contains X Coordinate value of location of all the events </param>
        private int[] EventYLocation = new int[] { };/// <param name="EventYLocation">Array that contains Y Coordinate value of location of all the events </param>
        private int[,] EventXYLocation = new int[,] { };/// <param name="EventXYLocation">Array that contains X and Y Coordinates value of location of all the events </param>
        private int[] EventDistanceFromUser = new int[] { };/// <param name="EventDistanceFromUser">Array that contains the distance of all the events from the user location </param>

        private double[] EventTicketClass = new double[] { };/// <param name="EventTicketClass">Array that contains the ticket class of all the events </param>
        private double[] EventTicketSortedByPrice = new double[] { };/// <param name="EventTicketSortedByPrice">Array that contains the tickets of all the events sorted by price </param>



        string recommendedEventID;/// <param name="recommendedEventID">Event ID of the recommended event </param>
        string recommendedEventTicket; /// <param name="recommendedEventTicket"> Recommended event ticket by price </param>
        string[] eventTicketPrice = new string[2]; /// <param name="eventTicketPrice"> Recommended event ticket by price </param>
        string totalPriceOfEventTicket = "";/// <param name="totalPriceOfEventTicket"> Total price of the recommended event in US Dollars and in Cents </param>
        string dollar = "";/// <param name="dollar"> Parameter to split the dollar value of the price of the recommended event </param>
        string cent = "";/// <param name="cent"> Parameter to split the cent value of the price of the recommended event </param>

        Random r = new Random();/// <param name="r">Object of Class Random to create Random Seed Values </param>

        public Event()
        {
            setEventConditions();
            /*Method to assign the legnths of the arrays and generate random values of x and y coordinates of events */
        }


        /// <summary>
        /// Method to set the lengths of the arrys , and also generate random values of X and Y coordinates of the event locations.  
        /// </summary>
        public void setEventConditions()
        {
            EventXLocation = new int[NumberOfEvents];
            EventYLocation = new int[NumberOfEvents];
            EventXYLocation = new int[NumberOfEvents, NumberOfEvents];
            EventDistanceFromUser = new int[NumberOfEvents];
            EventID = new int[NumberOfEvents];
            EventTicketClass = new double[NumberOfTicketClasses];
            EventTicketSortedByPrice = new double[NumberOfEvents];

            EventTicketClass = new double[NumberOfTicketClasses];

            /* Generation of random X and Y location coordinates of events */

            EventXLocation = generateRandomLocationCoodrinates(EventXLocation, lowerLimit, upperLimit);
            /*EventXLocation now has values of X coordinates of events that are found between (-10, 10)*/

            EventYLocation = generateRandomLocationCoodrinates(EventYLocation, lowerLimit, upperLimit);
            /*EventYLocation now has values of Y coordinates of events that are found between (-10, 10)*/




        }


        /// <summary>
        /// Generate Random Location Coordinates for Events.
        /// </summary>
        /// <param name="eventLocationCoordinates">Array to store the generated location coordinates of events</param>
        /// <param name="lowerLimit">The lower limit of the range between which the random numbers are generated.</param>
        /// <param name="upperLimit">The upper limit of the range between which the random numbers are generated.<</param>
        /// <returns>The randomly generated location coordinates of the events </returns>
        public int[] generateRandomLocationCoodrinates(int[] eventLocationCoordinates, int lowerLimit, int upperLimit)
        {
            eventLocationCoordinates = Enumerable.Repeat(0, eventLocationCoordinates.Length).Select(i => r.Next(lowerLimit, upperLimit)).ToArray();
            return eventLocationCoordinates;
        }

        /// <summary>
        /// Generate Random Ticket Prices for Events.
        /// </summary>
        /// <param name="lowestPrice">The lower limit of the range of price of tickets between which the random prices are generated.</param>
        /// <param name="highestPrice">The upper limit of the range of price of tickets between which the random prices are generated.<</param>
        /// <returns>The randomly generated prices of tickets for the events </returns>
        public double generateRandomEventTicketPrice(double lowestPrice, double highestPrice)
        {
            double ticketPrice = 0;
            var randomNumber = r.NextDouble();
            ticketPrice = lowestPrice + (randomNumber * (highestPrice - lowestPrice));
            return ticketPrice;
        }


        /// <summary>
        /// Create Event Info (Tickets, Prices and also create the list of Recommended Events)
        /// </summary>
        /// <param name="userXLocation">The X Coordinate of the user's location.</param>
        /// <param name="userYLocation">The Y Coordinate of the user's location.<</param>
        public void createEventInfo(int userXLocation, int userYLocation)
        {
            try
            {
                if ((EventXLocation != null) && (EventYLocation != null))
                {
                    for (int index = 0; index <= NumberOfEvents - 1; index++)
                    {
                        EventID[index] = index + 1;
                        EventXYLocation[index, 0] = EventXLocation[index];
                        EventXYLocation[index, 1] = EventYLocation[index];

                        /* The EventXYLocation array contains the random coordinates of events which are assigned from the EventXLocation and EventYLocation arrays */
                        
                        int currentEventXLocation = EventXYLocation[index, 0];
                        int currentEventYLocation = EventXYLocation[index, 1];
                        EventDistanceFromUser[index] = calcualteManhattanDistance(currentEventXLocation, currentEventYLocation, userXLocation, userYLocation);
                        
                        Console.WriteLine("|" +      "Event ID:" + EventID[index] +       "Event Distance:" + EventDistanceFromUser[index]       +           "|");
                        /*Calcualting the Manhattan Distance between X and Y coordinates of events from the EventXYLocation array and the X and Y Coordinates of the user's location */

                        for (int ticketIndex = 0; ticketIndex <= NumberOfTicketClasses - 1; ticketIndex++)
                        {
                            EventTicketClass[ticketIndex] = generateRandomEventTicketPrice(basePriceOfTicket, highestPriceOfTicket);
                            /*The random ticket prices are generated by the above method call based on the basePriceOfTicket and the highestPriceOfTicket */

                        }
                        EventTicketSortedByPrice[index] = orderTicketByLowestPrice(EventTicketClass);
                        /*The prices of the tickets are sorter using bubble sort by the method orderTicketByLowestPrice */



                    }

                    selectEventsBasedOnDistanceAndPrice(EventID, EventTicketSortedByPrice, EventDistanceFromUser);

                    /*From the above method call , the EventID, EventTicketSortedByPrice and EventDistanceFromUser are passed to compute the best events for the user based on location and price */
                }
            }

            catch (Exception exp)
            {
                Console.WriteLine("Location Coordinates Array Empty", exp);
            }



        }

        /// <summary>
        /// Calculates the Manhattan distance between the Coordinates of the Event and the Coordinates of User's Location.
        /// </summary>
        /// <param name="eventLocationX">The x coordinate of Event Location</param>
        /// <param name="eventLocationY">The y coordinate of the Event.</param>
        /// <param name="y1">The first y coordinate.</param>
        /// <param name="y2">The second y coordinate.</param>
        /// <returns>The Manhattan distance between (x1, y1) and (x2, y2)</returns>
        public int calcualteManhattanDistance(int eventLocationX, int eventLocationY, int userLocationX, int userLocationY)
        {
            int manhattanDistanceBetweenPoints = 0;
            manhattanDistanceBetweenPoints = Math.Abs(((eventLocationX - userLocationX) + (eventLocationY - userLocationY)));
            return manhattanDistanceBetweenPoints;
        }

        /// <summary>
        /// Orders Tickets By Lowest Price 
        /// </summary>
        /// <param name="tickets"> An array of tickest is passed as a parameter</param>
        /// <returns>Returns the lowest priced ticket</returns>
        public static double orderTicketByLowestPrice(double[] tickets)
        {
            double tempTicket = 0;
            bool sorted = false;

            while (!sorted)
            {
                sorted = true;

                for (int ticketIndex = 0; ticketIndex < tickets.Length - 1; ticketIndex++)
                {
                    if (tickets[ticketIndex] > tickets[ticketIndex + 1])
                    {
                        tempTicket = tickets[ticketIndex + 1];
                        tickets[ticketIndex + 1] = tickets[ticketIndex];
                        tickets[ticketIndex] = tempTicket;
                        sorted = false;
                    }
                }
            }
            return tickets[0];
        }

        /// <summary>
        /// Select Events Based On the Distance Between the Event and the UserLocation and the Price of the Ticket
        /// </summary>
        /// <param name="eventID"> Array containing the Event Identifier values of events</param>
        /// <param name="eventTicket"> Array containing the tickets of the events</param>
        /// <param name="eventDistance"> Array containing the distance of the event from the user's location</param>
        /// <returns>Returns the events based on the lowest order of price and distance from the user's location</returns>
        public static void selectEventsBasedOnDistanceAndPrice(int[] eventID, double[] eventTicket, int[] eventDistance)
        {
            int tempEventID = 0;
            double tempEventTicket = 0;
            int tempEventDistance = 0;
            bool sorted = false;

            while (!sorted)
            {
                sorted = true;

                for (int index = 0; index < eventDistance.Length - 1; index++)
                {
                    if (eventDistance[index] > eventDistance[index + 1])
                    {
                        tempEventID = eventID[index + 1];
                        eventID[index + 1] = eventID[index];
                        eventID[index] = tempEventID;

                        tempEventTicket = eventTicket[index + 1];
                        eventTicket[index + 1] = eventTicket[index];
                        eventTicket[index] = tempEventTicket;

                        tempEventDistance = eventDistance[index + 1];
                        eventDistance[index + 1] = eventDistance[index];
                        eventDistance[index] = tempEventDistance;

                        sorted = false;
                    }
                }
            }
        }

        /// <summary>
        /// Display the List of Selected Events
        /// </summary>
        /// <returns>Displays the list of recommended events ordered based on lowest price of tickets and nearest from the user's location</returns>
        public void displayRecommendedEvents()
        {
            for (int index = 0; index <= NumberOfEventResults; index++)
            {
                recommendedEventID = EventID[index].ToString();
                recommendedEventTicket = (Math.Round(EventTicketSortedByPrice[index], 2)).ToString();
                eventTicketPrice = recommendedEventTicket.Split('.');
                dollar = eventTicketPrice[0].PadLeft(2, '0');
                cent = eventTicketPrice[1].PadRight(2, '0');
                totalPriceOfEventTicket = dollar + "." + cent;
                Console.WriteLine("|" + "Event " + recommendedEventID.PadLeft(3, '0') +    "|"    + " - $" + totalPriceOfEventTicket +   "|" +   " Distance " + EventDistanceFromUser[index] +   "|");
            }
        }

    }
}
