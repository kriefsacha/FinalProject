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
        public void MoveRequest(Common.Plane Plane)
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
                        previous.Plane = null;
                        context.Histories.Add(new History() { StationNumber = previous.Number, DateOut = DateTime.Now, DateIn = null, PlaneId = Plane.Name });
                    }
                }

                var next = context.Stations.Where(s => s.Number == Plane.StationNumber).FirstOrDefault();

                if (next != null)
                {
                    DAL.Plane DalPlane = context.Planes.Where(p=>p.Name == Plane.Name).FirstOrDefault();
                    if(DalPlane == null)
                    {
                        DalPlane = new Plane() { Name = Plane.Name, ActionDate = Plane.ActionDate, flightState = Plane.flightState, PreviousStationNumber = Plane.PreviousStationNumber, StationNumber = Plane.StationNumber, waitingTime = Plane.waitingTime };
                        context.Planes.Add(DalPlane);
                    }

                    DalPlane.PreviousStationNumber = Plane.StationNumber;
                    DalPlane.StationNumber = Plane.StationNumber;

                    next.PlaneId = DalPlane.Id;
                    next.Plane = DalPlane;
                    context.Histories.Add(new History() { StationNumber = next.Number, DateIn = DateTime.Now, DateOut = null, PlaneId = Plane.Name });
                }

                context.SaveChanges();
            }
        }
    }
}