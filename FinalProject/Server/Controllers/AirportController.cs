using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Server.Controllers
{
    public class AirportController : ApiController
    {
        IAirportManager airport;
        public AirportController(IAirportManager airport)
        {
            this.airport = airport;
        }

        [HttpPost]
        public void Arrival(Plane Plane)
        {
            airport.NewDepartureOrArrival(Plane);
        }

        [HttpPost]
        public void Departure(Plane Plane)
        {
            airport.NewDepartureOrArrival(Plane);
        }

        [HttpGet]
        public List<Plane> GetFutureArrivals()
        {
            return airport.GetFutureArrivals();
        }

        [HttpGet]
        public List<Plane> GetFutureDepartures()
        {
            return airport.GetFutureDepartures();
        }

        [HttpGet]
        public List<Station> GetCurrentStationsState()
        {
            return airport.GetCurrentStationsState();
        }
    }
}
