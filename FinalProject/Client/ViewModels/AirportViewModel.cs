using Client.Models;
using Microsoft.AspNet.SignalR.Client;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Markup;
using System;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Common;

namespace Client.ViewModels
{
    public class AirportViewModel
    {
        private ObservableCollection<Models.Plane> planes;

        public ObservableCollection<Models.Plane> Planes
        {
            get { return planes; }
            set { planes = value; }
        }

        public ObservableCollection<Models.Station> Stations = new ObservableCollection<Models.Station>() { new Models.Station(1), new Models.Station(3) };


        public AirportViewModel()
        {

            Planes = new ObservableCollection<Models.Plane>();

            //Stations = new ObservableCollection<Models.Station>();

            //new Task(() =>
            //{
            //    HttpClient httpClient = new HttpClient();
            //    var t = httpClient.GetAsync("http://localhost:63938/api/airport/GetCurrentStationsState");
            //    t.Wait();
            //    var result = JsonConvert.DeserializeObject<List<Common.Station>>(t.Result.Content.ReadAsStringAsync().Result);
            //    foreach (var station in result)
            //    {
            //        if (station.Plane != null)
            //        {
            //            Planes.Add(new Models.Plane(station.Plane.Name, DateTime.Now, 0, Common.Enums.FlightState.Arrival));

            //        }

            //        Stations.Add(new Models.Station(station.Number) { Plane = station.Plane });

            //    }
            //}).Start();

            HubConnection hubConnection = new HubConnection("http://localhost:63938/");
            var proxy = hubConnection.CreateHubProxy("AirportHub");
            proxy.On<Common.Plane>("moved", Moved);
            proxy.On<string>("onerror", OnError);
            hubConnection.Start();

        }

        private async void Moved(Common.Plane plane)
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                var pl = planes.Where(p => p.Name == plane.Name).FirstOrDefault();
                if (pl == null)
                    planes.Add(new Models.Plane(plane.Name, plane.ActionTime, plane.waitingTime, plane.flightState) { StationNumber = plane.StationNumber });
                else
                    pl.StationNumber = plane.StationNumber;

                var nextstation = Stations.Where(s => s.Number == plane.StationNumber).FirstOrDefault();
                if (nextstation != null)
                {
                    nextstation.Plane = new Client.Models.Plane(plane.Name , plane.ActionTime , plane.waitingTime , plane.flightState);
                }

                var previous = Stations.Where(s => s.Number == plane.PreviousStationNumber).FirstOrDefault();
                if (previous != null)
                {
                    previous.Plane = null;
                }
            });
        }

        private void OnError(string errorMessage)
        {
            //TO DO POP UP ERROR MESSAGE
        }
    }
}
