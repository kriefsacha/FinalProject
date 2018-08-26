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

            Timer timer = new Timer(30000);
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

            switch (rnd.Next(2))
            {
                case 0:
                    Console.WriteLine("Sending plane " + plane.ID + " to departure in : " + plane.ActionTime + " who will wait in all stations " + plane.waitingTime / 1000 + " seconds");
                    plane.flightState = FlightState.Departure;
                    string jsond = JsonConvert.SerializeObject(plane);
                    var httpContentd = new StringContent(jsond, Encoding.UTF8, "application/json");
                    httpClient.PostAsync("http://localhost:63938/api/airport/Departure",httpContentd).Wait();
                    break;
                case 1:
                    Console.WriteLine("Sending plane " + plane.ID + " to arrival in : " + plane.ActionTime + " who will wait in all stations " + plane.waitingTime / 1000 + " seconds");
                    plane.flightState = FlightState.Arrival;
                    string jsona = JsonConvert.SerializeObject(plane);
                    var httpContenta = new StringContent(jsona, Encoding.UTF8, "application/json");
                    httpClient.PostAsync("http://localhost:63938/api/airport/Arrival", httpContenta).Wait();
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
            int range = 3;
            return DateTime.Now.AddMinutes(rnd.Next(1,range));
        }

        public static int GetWaitingTime()
        {
            return rnd.Next(4000, 10000);
        }
    }
}
