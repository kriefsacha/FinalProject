using Common;
using Common.Interfaces;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Services
{
    public class HubService : IHubService
    {
        public void Moved(Plane plane)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<AirportHub>();
            context.Clients.All.moved(plane);
        }
    }
}