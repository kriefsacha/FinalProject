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
            var Name = $"{GetLetter()}{GetLetter()}{GetNumber()}{GetNumber()}{GetNumber()}";
            var ActionTime = GetActionTime();
            var waitingTime = GetWaitingTime();
            FlightState flightState =  FlightState.Arrival;

            switch (rnd.Next(2))
            {
                case 0:
                    flightState = FlightState.Departure;
                    break;
                case 1:
                    flightState = FlightState.Arrival;
                    break;
            }

            Plane plane = new Plane(Name,ActionTime , waitingTime , flightState);
 

            HttpClient httpClient = new HttpClient();



            var json = JsonConvert.SerializeObject(plane);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var t = httpClient.PostAsync("http://localhost:63938/api/airport/DepartureOrArrival", httpContent);
            t.Wait();
            if (t.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                Console.WriteLine("Error : " + t.Result.Content.ReadAsStringAsync().Result);
            else
                Console.WriteLine("Sending plane " + plane.Name + " to " + plane.flightState + " at : " + plane.ActionTime + " who will wait in all stations " + plane.waitingTime / 1000 + " seconds");
        }

        private static string GetLetter()
        {
            int num = rnd.Next(0, 26);
            return ((char)('a' + num)).ToString().ToUpper();
        }

        private static int GetNumber()
        {
            return rnd.Next(0, 10);
        }

        private static DateTime GetActionTime()
        {
            int range = 3;
            return DateTime.Now.AddMinutes(rnd.Next(1, range));
        }

        private static int GetWaitingTime()
        {
            return rnd.Next(4000, 10000);
        }
    }
}
