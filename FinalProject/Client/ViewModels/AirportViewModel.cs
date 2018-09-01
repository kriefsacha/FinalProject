using Microsoft.AspNet.SignalR.Client;
using System.Collections.ObjectModel;
using System;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Client.ViewModels
{
    public class AirportViewModel
    {
        public ObservableCollection<Models.Plane> Planes { get; set; }

        public ObservableCollection<Models.Station> Stations { get; set; }

        public ObservableCollection<Models.Plane> FutureFlights { get; set; }

        public AirportViewModel()
        {
            Planes = new ObservableCollection<Models.Plane>();
            Stations = new ObservableCollection<Models.Station>();
            FutureFlights = new ObservableCollection<Models.Plane>();

            Init();
        }

        /// <summary>
        /// Initialize the view model
        /// </summary>
        private void Init()
        {
            new Task(async () =>
            {
                HttpClient httpClient = new HttpClient();
                var t = httpClient.GetAsync("http://localhost:63938/api/airport/GetCurrentStationsState");
                t.Wait();
                if (t.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    OnError(t.Result.Content.ReadAsStringAsync().Result);
                else
                {
                    var result = JsonConvert.DeserializeObject<List<Common.Station>>(t.Result.Content.ReadAsStringAsync().Result);
                    foreach (var station in result)
                    {
                        await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                        {
                            if (station.Plane != null)
                            {
                                Planes.Add(new Models.Plane(station.Plane.Name, DateTime.Now, 0, Common.Enums.FlightState.Arrival));
                                Stations.Add(new Models.Station(station.Number) { Plane = station.Plane.Name });
                            }

                            else
                                Stations.Add(new Models.Station(station.Number));
                        });
                    }
                }
            }).Start();

            new Task(() =>
            {
                HttpClient httpClient = new HttpClient();
                var t = httpClient.GetAsync("http://localhost:63938/api/airport/GetFutureDeparturesAndArrivals");
                t.Wait();
                if (t.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    OnError(t.Result.Content.ReadAsStringAsync().Result);
                else
                {
                    var result = JsonConvert.DeserializeObject<List<Common.Plane>>(t.Result.Content.ReadAsStringAsync().Result);
                    foreach (var plane in result)
                    {
                        FutureFlights.Add(new Models.Plane(plane.Name, plane.ActionDate, plane.waitingTime, plane.flightState));
                    }
                }
            }).Start();

            HubConnection hubConnection = new HubConnection("http://localhost:63938/");
            var proxy = hubConnection.CreateHubProxy("AirportHub");
            proxy.On<Common.Plane>("departureOrArrival", DepartureOrArrival);
            proxy.On<Common.Plane>("moved", Moved);
            proxy.On<string>("onerror", OnError);
            hubConnection.Start();
        }

        /// <summary>
        /// New departure/arrival planed
        /// </summary>
        /// <param name="plane">The new plane</param>
        private async void DepartureOrArrival(Common.Plane plane)
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                FutureFlights.Add(new Models.Plane(plane.Name, plane.ActionDate, plane.waitingTime, plane.flightState));
                var sortedFlights = FutureFlights.OrderBy(f => f.ActionDate).ToList();
                FutureFlights.Clear();

                foreach (var flight in sortedFlights)
                    FutureFlights.Add(flight);
            });
        }

        /// <summary>
        /// Plane is moving
        /// </summary>
        /// <param name="plane"></param>
        private async void Moved(Common.Plane plane)
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                //If it's not his last station
                if (plane.StationNumber != 0)
                {
                    var pl = Planes.Where(p => p.Name == plane.Name).FirstOrDefault();
                    if (pl == null)
                        Planes.Add(new Models.Plane(plane.Name, plane.ActionDate, plane.waitingTime, plane.flightState) { StationNumber = plane.StationNumber });
                    else
                        pl.StationNumber = plane.StationNumber;
                }
                else Planes.Remove(Planes.Where(p => p.Name == plane.Name).First());


                var previous = Stations.Where(s => s.Number == plane.PreviousStationNumber).FirstOrDefault();
                if (previous != null && previous.Plane != null)
                {
                    previous.Plane = null;
                }

                var nextstation = Stations.Where(s => s.Number == plane.StationNumber).FirstOrDefault();
                if (nextstation != null)
                {
                    nextstation.Plane = plane.Name;
                }

                //if it's his first station
                if (plane.PreviousStationNumber == 0)
                {
                    var flight = FutureFlights.Where(f => f.Name == plane.Name).FirstOrDefault();
                    if (flight != null)
                    {
                        FutureFlights.Remove(flight);
                    }
                }
            });
        }

        private void OnError(string errorMessage)
        {
            //TO DO POP UP ERROR MESSAGE
        }
    }
}