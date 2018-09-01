using Common;
using Microsoft.AspNet.SignalR;
using Server.Interfaces;
using System;

namespace Server.Services
{
    internal class HubService : IHubService
    {
        /// <summary>
        /// New departure/arrival planned
        /// </summary>
        /// <param name="plane">The new plane</param>
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