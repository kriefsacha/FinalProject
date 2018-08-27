using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    public class Manager : IManager
    {
        IAirportManager airport;
        IHubService hubService;
        IDalService dalService;

        public Manager(IAirportManager airport, IHubService hubService, IDalService dalService)
        {
            this.airport = airport;
            this.dalService = dalService;
            this.hubService = hubService;

            airport.planeMoved += Airport_planeMoved;
        }

        private void Airport_planeMoved(object sender, EventArgs e)
        {
            if (sender is Plane plane)
            {
                hubService.Moved(plane);
                dalService.Moved(plane);
            }
        }

        public void DepartureOrArrival(Plane plane)
        {
            airport.NewDepartureOrArrival(plane);
            hubService.DepartureOrArrival(plane);
            dalService.DepartureOrArrival(plane);
        }

        public List<Plane> GetFutureDeparturesAndArrivals()
        {
            return dalService.GetFutureDeparturesAndArrivals();
        }

        public List<Station> GetCurrentStationsState()
        {
            return dalService.GetCurrentStationsState();
        }

        public void AddStation(Station station)
        {
            airport.AddStation(station);
            dalService.AddStation(station);
        }
    }
}