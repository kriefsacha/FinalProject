using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StationRepository : IStationRepository
    {
        public void AddStation(Common.Station station)
        {
            using(AirportDataModel context = new AirportDataModel())
            {
                context.Stations.Add(new Station() { stepKey = station.stepKey, Number = station.Number, PlaneId = station.Plane.Name });
                context.SaveChanges();
            }
        }

        public List<Common.Station> GetCurrentStationsState()
        {
            List<Common.Station> stations = new List<Common.Station>();

            using (AirportDataModel context = new AirportDataModel())
            {
                var DALStations = context.Stations.ToList();

                foreach (var station in DALStations)
                {
                    if (station.IsAvailable && station.PlaneId != null && station.PlaneId != "")
                        stations.Add(new Common.Station(station.Number,station.stepKey ) {  Plane = new Plane() { Name = station.PlaneId } });
                    else
                        stations.Add(new Common.Station(station.Number, station.stepKey) { Number = station.Number });
                }
            }

            return stations;
        }
    }
}
