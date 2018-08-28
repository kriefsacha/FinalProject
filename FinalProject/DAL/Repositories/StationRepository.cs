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

            using (AirportDataModel context = new AirportDataModel())
            {
                var DALStations = context.Stations.ToList();

                foreach (var station in DALStations)
                {
                    if (!station.IsAvailable && station.PlaneId != null && station.PlaneId != "")
                        stations.Add(new Common.Station(station.Number,station.stepKey ) { Plane = new Plane() { Name = station.PlaneId } });
                    else
                        stations.Add(new Common.Station(station.Number, station.stepKey) );
                }
            }
            return stations;
        }

        public List<Common.Relation> GetRelations()
        {
            List<Common.Relation> relations = new List<Common.Relation>();
            
            using(AirportDataModel context = new AirportDataModel())
            {
                var DALRelations = context.Relations.ToList();

                foreach (var relation in DALRelations)
                {
                    relations.Add(new Common.Relation(relation.StationId, relation.StepId, relation.State));
                }
            }

            return relations;
        }
    }
}
