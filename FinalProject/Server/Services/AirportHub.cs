using Common;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Services
{
    public class AirportHub : Hub
    {
        public void Init()
        {

        }

        public void Departure(Plane Plane)
        {
            Clients.All.departure(Plane.ID);
        }

        public void Arrival(Plane Plane)
        {
            Clients.All.arrival(Plane.ID);
        }

        public void MoveRequest(Plane Plane)
        {

        }
    }
}