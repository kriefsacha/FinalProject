using Common;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var t1 = new Task(() =>
            //{
            //    HttpClient httpClient = new HttpClient();
            //    var a = httpClient.GetAsync("http://localhost:63938/api/airport/GetFutureArrivals");
            //    a.Wait();
            //    var b = JsonConvert.DeserializeObject<List<Plane>>(a.Result.Content.ReadAsStringAsync().Result);
            //    Console.WriteLine("Future Arrivals : " + Environment.NewLine);
            //    foreach (var item in b)
            //    {
            //        Console.WriteLine($"{item.ID} will arrive in {item.ActionTime}");
            //    }
            //});

            //var t2 = new Task(() =>
            //{
            //    HttpClient httpClient = new HttpClient();
            //    var a = httpClient.GetAsync("http://localhost:63938/api/airport/GetFutureDepartures");
            //    a.Wait();
            //    var b = JsonConvert.DeserializeObject<List<Plane>>(a.Result.Content.ReadAsStringAsync().Result);
            //    Console.WriteLine(Environment.NewLine + "Future Departures : " + Environment.NewLine);
            //    foreach (var item in b)
            //    {
            //        Console.WriteLine($"{item.ID} will departure in {item.ActionTime}");
            //    }
            //});

            //var t3 = new Task(() =>
            //{
            //    HttpClient httpClient = new HttpClient();
            //    var a = httpClient.GetAsync("http://localhost:63938/api/airport/GetCurrentStationsState");
            //    a.Wait();
            //    var b = JsonConvert.DeserializeObject<List<Station>>(a.Result.Content.ReadAsStringAsync().Result);
            //    Console.WriteLine(Environment.NewLine + "Stations State : " + Environment.NewLine);
            //    foreach (var item in b)
            //    {
            //        Console.WriteLine($"Station {item.Number} , is available : {item.IsAvailable}");
            //    }
            //});

            //t1.Start();
            //t2.Start();
            //t3.Start();

            // Optional Task.WhenAll(t1, t2, t3).ContinueWith((t) => { Console.WriteLine("finish"); });

            HubConnection hubConnection = new HubConnection("http://localhost:63938");
            var proxy = hubConnection.CreateHubProxy("AirportHub");
            proxy.On<Plane>("moved", Moved);
            proxy.On<Plane>("departureOrArrival", DepartureOrArrival);
            hubConnection.Start();
            Console.ReadLine();
        }

        private static void DepartureOrArrival(Plane obj)
        {
            Console.WriteLine($"Plane {obj.Name} was added to {obj.flightState} , he will do it at : {obj.ActionTime} ");
        }

        private static void Moved(Plane obj)
        {
            if (obj.StationNumber != 0)
                Console.WriteLine(obj.Name + " Moved to station " + obj.StationNumber);
            else
                Console.WriteLine(obj.Name + " is out the airport ! ");
        }
    }
}
