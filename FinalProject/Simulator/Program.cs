using Common;
using Common.Enums;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Timers;

namespace Simulator
{
    class Program
    {
        static Random rnd;
        static Timer timer;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Airport Simulator !");
            Console.WriteLine("--------------------------------------------------------------" + Environment.NewLine);
            rnd = new Random();

            timer = new Timer(15000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Create new plane and planned it to departure/arrival
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var Name = $"{GetLetter()}{GetLetter()}{GetNumber()}{GetNumber()}{GetNumber()}";
            var ActionDate = GetActionDate();
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

            Plane plane = new Plane(Name,ActionDate , waitingTime , flightState);

            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(plane);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            //IMPORTANT !!
            //If the link above doesn't work anymore , it's because the free azure account doesn't work anymore , so you need to do it locally
            var t = httpClient.PostAsync("https://finalprojectsela.azurewebsites.net/api/airport/DepartureOrArrival", httpContent);
            t.Wait();

            if (t.Result.StatusCode == System.Net.HttpStatusCode.BadRequest || t.Result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                Console.WriteLine("Error : " + t.Result.Content.ReadAsStringAsync().Result);
                timer.Stop();
            }
            else
                Console.WriteLine("Sending plane " + plane.Name + " to " + plane.flightState + " at : " + plane.ActionDate + " who will wait in all stations " + plane.waitingTime / 1000 + " seconds");
        }

        /// <summary>
        /// Get a letter from a to z
        /// </summary>
        /// <returns>The new letter</returns>
        private static string GetLetter()
        {
            int num = rnd.Next(0, 26);
            return ((char)('a' + num)).ToString().ToUpper();
        }

        /// <summary>
        /// Get a number betwen 0 and 10
        /// </summary>
        /// <returns>The new number</returns>
        private static int GetNumber()
        {
            return rnd.Next(0, 10);
        }

        /// <summary>
        /// Get a new action date ( date that the plane departure/arrive )
        /// </summary>
        /// <returns></returns>
        private static DateTime GetActionDate()
        {
            return DateTime.Now.AddMinutes(rnd.Next(1,3));
        }

        private static int GetWaitingTime()
        {
            return rnd.Next(4000, 10000);
        }
    }
}
