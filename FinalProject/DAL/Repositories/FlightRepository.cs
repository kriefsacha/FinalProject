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
        public void Departure(Plane plane)
        {
            using (AirportDataModel context = new AirportDataModel())
            {
                context.Departures.Add(new Departure() { PlaneId = plane.ID, DatePlanned = plane.ActionTime });
                context.SaveChanges();
            }
        }

        public void Arrival(Plane plane)
        {
            using (AirportDataModel context = new AirportDataModel())
            {
                context.Arrivals.Add(new Arrival() { PlaneId = plane.ID, DatePlanned = plane.ActionTime });
                context.SaveChanges();
            }
        }

        public List<Plane> GetFutureArrivals()
        {
            List<Plane> FutureArrivals = new List<Plane>();

            using (AirportDataModel context = new AirportDataModel())
            {
                var DalFutureArrivals = context.Arrivals.Where(a => a.DatePlanned > DateTime.Now).OrderBy(a=>a.DatePlanned).ToList();

                foreach (var futureArrival in DalFutureArrivals)
                {
                    FutureArrivals.Add(new Plane { ActionTime = futureArrival.DatePlanned, ID = futureArrival.PlaneId });
                }
            }
            return FutureArrivals;
        }

        public List<Plane> GetFutureDepartures()
        {
            List<Plane> FutureDepartures = new List<Plane>();

            using (AirportDataModel context = new AirportDataModel())
            {
                var DalFutureDepartures = context.Departures.Where(a => a.DatePlanned > DateTime.Now).OrderBy(a => a.DatePlanned).ToList();

                foreach (var futureDeparture in DalFutureDepartures)
                {
                    FutureDepartures.Add(new Plane { ActionTime = futureDeparture.DatePlanned, ID = futureDeparture.PlaneId });
                }
            }
            return FutureDepartures;

        }
    }
}
