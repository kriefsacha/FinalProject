using Common;
using Common.Interfaces;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class PlaneRepository : IPlaneRepository
    {
        /// <summary>
        /// A plane is moving
        /// </summary>
        /// <param name="Plane">The plane that moves</param>
        public void MoveRequest(Plane Plane)
        {
            using (AirportDataModel context = new AirportDataModel())
            {
                Station previous = null;
                //If it's not his first station
                if (Plane.PreviousStationNumber != 0)
                {
                    previous = context.Stations.Where(s => s.Number == Plane.PreviousStationNumber).FirstOrDefault();

                    if (previous != null)
                    {
                        previous.PlaneId = null;
                        context.Histories.Add(new History() { StationNumber = previous.Number, DateOut = DateTime.Now, DateIn = null, PlaneId = Plane.Name });
                    }
                }

                var next = context.Stations.Where(s => s.Number == Plane.StationNumber).FirstOrDefault();

                if (next != null)
                {
                    next.PlaneId = Plane.Name;
                    context.Histories.Add(new History() { StationNumber = next.Number, DateIn = DateTime.Now, DateOut = null, PlaneId = Plane.Name });
                }

                context.SaveChanges();
            }
        }
    }
}