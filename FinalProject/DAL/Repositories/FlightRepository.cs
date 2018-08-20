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
                context.Departures.Add(new Departure() { PlaneId = plane.ID, DatePlanned = DateTime.Now });
                context.SaveChanges();
            }
        }

        public void Arrival(Plane plane)
        {
            using (AirportDataModel context = new AirportDataModel())
            {
                context.Arrivals.Add(new Arrival() { PlaneId = plane.ID, DatePlanned = DateTime.Now });
                context.SaveChanges();
            }
        }
    }
}
