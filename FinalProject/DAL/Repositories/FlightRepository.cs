﻿using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        /// <summary>
        /// New future departure or arrival to add
        /// </summary>
        /// <param name="plane">The new plane</param>
        public void DepartureOrArrival(Common.Plane plane)
        {
            using (AirportDataModel context = new AirportDataModel())
            {
                if (plane.flightState == Common.Enums.FlightState.Departure)
                    context.Departures.Add(new Departure() { PlaneId = plane.Name, DatePlanned = plane.ActionDate, waitingTime = plane.waitingTime});
                else
                    context.Arrivals.Add(new Arrival() { PlaneId = plane.Name, DatePlanned = plane.ActionDate, waitingTime = plane.waitingTime});

                context.SaveChanges();
            }
        }

        public List<Common.Plane> GetFutureDeparturesAndArrivals()
        {
            List<Common.Plane> FutureDeparturesAndArrivals = new List<Common.Plane>();

            using (AirportDataModel context = new AirportDataModel())
            {
                var DalFutureDepartures = context.Departures.Where(a => a.DatePlanned > DateTime.Now).OrderBy(a => a.DatePlanned).ToList();

                foreach (var futureDeparture in DalFutureDepartures)
                {
                    FutureDeparturesAndArrivals.Add(new Common.Plane(futureDeparture.PlaneId, futureDeparture.DatePlanned, futureDeparture.waitingTime, Common.Enums.FlightState.Departure));
                }

                var DalFutureArrivals = context.Arrivals.Where(a => a.DatePlanned > DateTime.Now).OrderBy(a => a.DatePlanned).ToList();

                foreach (var futureArrival in DalFutureArrivals)
                {
                    FutureDeparturesAndArrivals.Add(new Common.Plane(futureArrival.PlaneId, futureArrival.DatePlanned, futureArrival.waitingTime, Common.Enums.FlightState.Arrival));
                }
            }
            return FutureDeparturesAndArrivals;
        }
    }
}
