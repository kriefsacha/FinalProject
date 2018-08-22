using Common;
using Common.Interfaces;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Services
{
    public class AirportHub : Hub
    {
        ILogic logic;

        public AirportHub(ILogic logic)
        {
            this.logic = logic;
        }


        public void GetInitialState()
        {
            var futureDepartures = logic.GetFutureDepartures();
            var futureArrivals = logic.GetFutureArrivals();
            var stationsState = logic.GetCurrentStationsState();

            Clients.Caller.init(futureDepartures, futureArrivals , stationsState);
        }

        public void Departure(Plane Plane)
        {
            logic.Departure(Plane);
            Clients.All.departure(Plane);
        }

        public void Arrival(Plane Plane)
        {
            logic.Arrival(Plane);
            Clients.All.arrival(Plane);
        }

        public void MoveRequest(Plane Plane)
        {

        }
    }
}