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
        IManager manager;
        public AirportController(IManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        public void Arrival(Plane Plane)
        {
            manager.Departure(Plane);
        }

        [HttpPost]
        public void Departure(Plane Plane)
        {
            manager.Arrival(Plane);
        }

        [HttpGet]
        public List<Plane> GetFutureArrivals()
        {
            try
            {
                return manager.GetFutureArrivals();
            }
            catch (Exception exc)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exc.Message)
                };

                throw new HttpResponseException(resp);
            }
        }

        [HttpGet]
        public List<Plane> GetFutureDepartures()
        {
            try
            {
                return manager.GetFutureDepartures();
            }
            catch (Exception exc)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exc.Message)
                };

                throw new HttpResponseException(resp);
            }
        }

        [HttpGet]
        public List<Station> GetCurrentStationsState()
        {
            try
            {
                return manager.GetCurrentStationsState();
            }
            catch (Exception exc)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exc.Message)
                };

                throw new HttpResponseException(resp);
            }
        }
    }
}
