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

        public void Init()
        {

        }

        public void Departure(Plane Plane)
        {
            logic.Departure(Plane);
            Clients.All.departure(Plane.ID);
        }

        public void Arrival(Plane Plane)
        {
            logic.Arrival(Plane);
            Clients.All.arrival(Plane.ID);
        }

        public void MoveRequest(Plane Plane)
        {

        }
    }
}