using Common;
using Common.Interfaces;
using Microsoft.AspNet.SignalR;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Services
{
    internal class HubService : IHubService
    {
        public void DepartureOrArrival(Plane plane)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<AirportHub>();
            context.Clients.All.departureOrArrival(plane);
        }

        public void Moved(Plane plane)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<AirportHub>();
            context.Clients.All.moved(plane);
        }

        public void OnError(Exception exc)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<AirportHub>();
            context.Clients.All.onerror(exc.Message);
        }
    }
}