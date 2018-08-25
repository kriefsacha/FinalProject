using Common;
using Common.Enums;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Simulator
{
    class Program
    {
        static Random rnd;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Airport Simulator !");
            Console.WriteLine("--------------------------------------------------------------" + Environment.NewLine);
            rnd = new Random();

            Timer timer = new Timer(2000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Plane plane = new Plane();
            plane.ID = $"{GetLetter()}{GetLetter()}{GetNumber()}{GetNumber()}{GetNumber()}";
            plane.ActionTime = GetActionTime();
            plane.waitingTime = GetWaitingTime();

            HttpClient httpClient = new HttpClient();
            string json = JsonConvert.SerializeObject(plane);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            switch (rnd.Next(2))
            {
                case 0:
                    plane.flightState = FlightState.Departure;
                    httpClient.PostAsync("http://localhost:63938/api/airport/Departure",httpContent).Wait();
                    break;
                case 1:
                    plane.flightState = FlightState.Arrival;
                    httpClient.PostAsync("http://localhost:63938/api/airport/Arrival", httpContent).Wait();
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
