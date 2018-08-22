using Common;
using Common.Enums;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Simulator
{
    class Program
    {
        static Random rnd;
        static IHubProxy proxy;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Airport Simulator !");
            Console.WriteLine("--------------------------------------------------------------" + Environment.NewLine);
            rnd = new Random();

            //HubConnection connection = new HubConnection("https://finalprojectsela.azurewebsites.net");
            HubConnection connection = new HubConnection("http://localhost:63938/");
            proxy = connection.CreateHubProxy("AirportHub");
            Console.WriteLine("Connecting .." + Environment.NewLine);
            connection.Start().Wait();
            Console.WriteLine("Connected !" + Environment.NewLine);

            proxy.On<Plane>("departure", Departure);
            proxy.On<Plane>("arrival", Arrival);
            //proxy.On<List<Plane>, List<Plane>, List<Station>>("init", Init);

            //proxy.Invoke("GetInitialState");
            //Timer timer = new Timer(2000);
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void Init(List<Plane> futureDepartures, List<Plane> futureArrivals, List<Station> stationsState)
        {
            Console.WriteLine("Future Departures : " + Environment.NewLine);

            foreach (var departure in futureDepartures)
            {
                Console.WriteLine($"Plane {departure.ID} is planned for departure at {departure.ActionTime}");
            }

            Console.WriteLine(Environment.NewLine + "Future Arrivals : " + Environment.NewLine);

            foreach (var arrival in futureArrivals)
            {
                Console.WriteLine($"Plane {arrival.ID} is planned for arrival at {arrival.ActionTime}");
            }

            Console.WriteLine(Environment.NewLine + "Stations : " + Environment.NewLine);

            foreach (var station in stationsState)
            {
                if (station.IsAvailable)
                    Console.WriteLine($"Station number {station.Number} is available ");
                else
                    Console.WriteLine($"Station number {station.Number} is not available ");
            }
        }

        private static void Arrival(Plane plane)
        {
            Console.WriteLine("Plane " + plane.ID + " is planned to Arrival on : " + plane.ActionTime + Environment.NewLine);
        }

        private static void Departure(Plane plane)
        {
            Console.WriteLine("Plane " + plane.ID + " is planned to Departure on : " + plane.ActionTime + Environment.NewLine);
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Plane plane = new Plane();
            plane.ID = $"{GetLetter()}{GetLetter()}{GetNumber()}{GetNumber()}{GetNumber()}";
            plane.ActionTime = GetActionTime();
            plane.waitingTime = GetWaitingTime();

            switch (rnd.Next(2))
            {
                case 0:
                    plane.flightState = FlightState.Departure;
                    proxy.Invoke("Departure", plane);
                    break;
                case 1:
                    plane.flightState = FlightState.Arrival;
                    proxy.Invoke("Arrival", plane);
                    break;
            }
        }

        public static string GetLetter()
        {
            int num = rnd.Next(0, 26);
            return ((char)('a' + num)).ToString().ToUpper();
        }

        public static int GetNumber()
        {
            return rnd.Next(0, 10);
        }

        public static DateTime GetActionTime()
        {
            int range = 120;
            return DateTime.Now.AddMinutes(rnd.Next(range));
        }

        public static int GetWaitingTime()
        {
            return rnd.Next(5000, 20000);
        }
    }
}
