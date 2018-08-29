using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System;

namespace Client.Controls
{
    public sealed partial class FlightsDetails : UserControl
    {
        ObservableCollection<Models.Plane> Flights = new ObservableCollection<Client.Models.Plane>();
        
        public FlightsDetails()
        {
            this.InitializeComponent();

            //new Task(() =>
            //{
            //    HttpClient httpClient = new HttpClient();
            //    var t = httpClient.GetAsync("http://localhost:63938/api/airport/GetFutureDeparturesAndArrivals");
            //    t.Wait();
            //    var result = JsonConvert.DeserializeObject<List<Common.Plane>>(t.Result.Content.ReadAsStringAsync().Result);
            //    foreach (var plane in result)
            //    {
            //        Flights.Add(new Models.Plane(plane.Name, plane.ActionTime, plane.waitingTime, plane.flightState));
            //    }
            //}).Start();

            HubConnection hubConnection = new HubConnection("http://localhost:63938/");
            var proxy = hubConnection.CreateHubProxy("AirportHub");
            proxy.On<Common.Plane>("departureOrArrival", DepartureOrArrival);
            hubConnection.Start();

            InformationLV.ItemsSource = Flights;
        }

        private async void DepartureOrArrival(Common.Plane plane)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
           {
               Flights.Add(new Models.Plane(plane.Name, plane.ActionTime, plane.waitingTime, plane.flightState));
           });
        }
    }
}