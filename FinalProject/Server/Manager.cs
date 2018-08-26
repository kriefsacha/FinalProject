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

        public Manager(IAirportManager airport , IHubService hubService , IDalService dalService)
        {
            this.airport = airport;
            this.dalService = dalService;
            this.hubService = hubService;

            airport.planeMoved += Airport_planeMoved;
        }

        private void Airport_planeMoved(object sender, EventArgs e)
        {
            if(sender is Plane plane)
            {
                hubService.Moved(plane);
                dalService.Moved(plane);
            }
        }

        public void Arrival(Plane Plane)
        {
            airport.NewDepartureOrArrival(Plane);
        }

        public void Departure(Plane Plane)
        {
            airport.NewDepartureOrArrival(Plane);
        }

        public List<Plane> GetFutureArrivals()
        {
            return dalService.GetFutureArrivals();
        }

        public List<Plane> GetFutureDepartures()
        {
            return dalService.GetFutureDepartures();
        }

        public List<Station> GetCurrentStationsState()
        {
            return dalService.GetCurrentStationsState();
        }
    }
}