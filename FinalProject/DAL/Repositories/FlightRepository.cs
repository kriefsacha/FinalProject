using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        public void DepartureOrArrival(Plane plane)
        {
            using (AirportDataModel context = new AirportDataModel())
            {
                if (plane.flightState == Common.Enums.FlightState.Departure)
                    context.Departures.Add(new Departure() { PlaneId = plane.Name, DatePlanned = plane.ActionTime , waitingTime = plane.waitingTime});
                else
                    context.Arrivals.Add(new Arrival() { PlaneId = plane.Name, DatePlanned = plane.ActionTime , waitingTime = plane.waitingTime});

                context.SaveChanges();
            }
        }

        public List<Plane> GetFutureDeparturesAndArrivals()
        {
            List<Plane> FutureDeparturesAndArrivals = new List<Plane>();

            using (AirportDataModel context = new AirportDataModel())
            {
                var DalFutureDepartures = context.Departures.Where(a => a.DatePlanned > DateTime.Now).OrderBy(a => a.DatePlanned).ToList();

                foreach (var futureDeparture in DalFutureDepartures)
                {
                    FutureDeparturesAndArrivals.Add(new Plane(futureDeparture.PlaneId , futureDeparture.DatePlanned , futureDeparture.waitingTime , Common.Enums.FlightState.Departure));
                }

                var DalFutureArrivals = context.Arrivals.Where(a => a.DatePlanned > DateTime.Now).OrderBy(a => a.DatePlanned).ToList();

                foreach (var futureArrival in DalFutureArrivals)
                {
                    FutureDeparturesAndArrivals.Add(new Plane(futureArrival.PlaneId , futureArrival.DatePlanned , futureArrival.waitingTime , Common.Enums.FlightState.Arrival));
                }
            }
            return FutureDeparturesAndArrivals;
        }
    }
}
