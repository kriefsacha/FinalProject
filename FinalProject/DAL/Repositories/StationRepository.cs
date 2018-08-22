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
        public List<Common.Station> GetCurrentStationsState()
        {
            List<Common.Station> stations = new List<Common.Station>();

            using(AirportDataModel context = new AirportDataModel())
            {
                var DALStations = context.Stations.ToList();

                foreach (var station in DALStations)
                {
                    stations.Add(new Common.Station() { Number = station.Number, Plane = station.Plane });
                }
            }

            return stations;
        }
    }
}
