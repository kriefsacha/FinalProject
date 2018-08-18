using Common;
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

            HubConnection connection = new HubConnection("https://finalprojectsela.azurewebsites.net");
            proxy = connection.CreateHubProxy("AirportHub");
            connection.Start().Wait();
            proxy.On<string>("departure", Departure);
            proxy.On<string>("arrival", Arrival);

            Timer timer = new Timer(1500);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void Arrival(string planeID)
        {
            Console.WriteLine("Plane " + planeID + " Entered to first Arrivals Station !" + Environment.NewLine);
        }

        private static void Departure(string planeID)
        {
            Console.WriteLine("Plane " + planeID + " Entered to first Departures Station !" + Environment.NewLine);
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Plane plane = new Plane();
            plane.ID = $"{GetLetter()}{GetLetter()}{GetNumber()}{GetNumber()}{GetNumber()}";

            switch (rnd.Next(0, 2))
            {
                case 0:
                    proxy.Invoke("Departure", plane);
                    break;
                case 1:
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
    }
}
