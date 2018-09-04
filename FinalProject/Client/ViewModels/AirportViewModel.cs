using Microsoft.AspNet.SignalR.Client;
using System.Collections.ObjectModel;
using System;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Client.ViewModels
{
    public class AirportViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Plane> Planes { get; set; }

        public ObservableCollection<Models.Station> Stations { get; set; }

        public ObservableCollection<Models.Plane> FutureFlights { get; set; }


        private string _messages;

        public string Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                Notify(nameof(Messages));
            }
        }


        public AirportViewModel()
        {
            Messages = "";
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

            Log("Initializing ..");

            //First task who gets the stations state
            var t1 = new Task(async () =>
            {
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                Task<HttpResponseMessage> t = null;
                try
                {
                    //IMPORTANT !!
                    //If the link above doesn't work anymore , it's because the free azure account doesn't work anymore , so you need to do it locally
                    t = httpClient.GetAsync("https://finalprojectsela.azurewebsites.net/api/airport/GetCurrentStationsState");
                    t.Wait();
                }

                catch (Exception exc)
                {
                    OnError(exc.Message);
                }
                if (t.Result.StatusCode == System.Net.HttpStatusCode.BadRequest || t.Result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
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
                                Planes.Add(new Models.Plane(station.Plane.Name, station.Plane.ActionDate, station.Plane.waitingTime, station.Plane.flightState));
                                Stations.Add(new Models.Station(station.Number) { Plane = station.Plane.Name });
                            }

                            else
                                Stations.Add(new Models.Station(station.Number));
                        });
                    }
                }
            });

            //Second Task who get the future departures and arrivals
            var t2 = new Task(async () =>
            {
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                Task<HttpResponseMessage> t = null;
                try
                {
                    //IMPORTANT !!
                    //If the link above doesn't work anymore , it's because the free azure account doesn't work anymore , so you need to do it locally
                    t = httpClient.GetAsync("https://finalprojectsela.azurewebsites.net/api/airport/GetFutureDeparturesAndArrivals");
                    t.Wait();
                }
                catch (Exception exc)
                {
                    OnError(exc.Message);
                }

                if (t.Result.StatusCode == System.Net.HttpStatusCode.BadRequest || t.Result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    OnError(t.Result.Content.ReadAsStringAsync().Result);
                else
                {
                    var result = JsonConvert.DeserializeObject<List<Common.Plane>>(t.Result.Content.ReadAsStringAsync().Result);
                    foreach (var plane in result)
                    {
                        await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                        {
                            FutureFlights.Add(new Models.Plane(plane.Name, plane.ActionDate, plane.waitingTime, plane.flightState));
                        });
                    }
                }
            });

            //Third task who open the hub connection
            var t3 = new Task(() =>
            {
                try
                {
                    //IMPORTANT !!
                    //If the link above doesn't work anymore , it's because the free azure account doesn't work anymore , so you need to do it locally
                    HubConnection hubConnection = new HubConnection("https://finalprojectsela.azurewebsites.net");
                    var proxy = hubConnection.CreateHubProxy("AirportHub");
                    proxy.On<Common.Plane>("departureOrArrival", DepartureOrArrival);
                    proxy.On<Common.Plane>("moved", Moved);
                    proxy.On<string>("onerror", OnError);
                    hubConnection.Start();
                }
                catch (Exception exc)
                {
                    OnError(exc.Message);
                    throw;
                }


            });

            t1.Start();
            t2.Start();
            t3.Start();

            Task.WhenAll(t1, t2, t3).ContinueWith((t) => { Log("Finished initialization ! "); });
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
            Log(errorMessage);
        }

        private void Log(string message)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss") + " -> " + message + Environment.NewLine);
            str.AppendLine(Messages);
            Messages = str.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Notify(string propName)
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            });
        }
    }
}