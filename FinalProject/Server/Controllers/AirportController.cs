using Common;
using Common.Interfaces;
using Server.Interfaces;
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
        public void DepartureOrArrival(Plane Plane)
        {
            try
            {
                manager.DepartureOrArrival(Plane);
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
        public List<Plane> GetFutureDeparturesAndArrivals()
        {
            try
            {
                return manager.GetFutureDeparturesAndArrivals();
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
