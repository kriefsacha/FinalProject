using Common;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class AirportInitializer : DropCreateDatabaseAlways<AirportDataModel>
    {
        protected override void Seed(AirportDataModel context)
        {
            context.Stations.Add(new Station() { Number = 1, stepKey = "S1" });
            context.Stations.Add(new Station() { Number = 2, stepKey = "S2" });
            context.Stations.Add(new Station() { Number = 3, stepKey = "S3" });
            context.Stations.Add(new Station() { Number = 4, stepKey = "S4" });
            context.Stations.Add(new Station() { Number = 5, stepKey = "S5" });
            context.Stations.Add(new Station() { Number = 6, stepKey = "S6" });
            context.Stations.Add(new Station() { Number = 7, stepKey = "S6" });
            context.Stations.Add(new Station() { Number = 8, stepKey = "S7" });

            context.Relations.Add(new Relation() { StationId = 0, StepId = "S1", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 1, StepId = "S2", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 2, StepId = "S3", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 3, StepId = "S4", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 4, StepId = "S5", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 5, StepId = "S6", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 6, StepId = null, State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 7, StepId = null, State = FlightState.Arrival });

            context.Relations.Add(new Relation() { StationId = 0, StepId = "S6", State = FlightState.Departure });
            context.Relations.Add(new Relation() { StationId = 4, StepId = null, State = FlightState.Departure });
            context.Relations.Add(new Relation() { StationId = 6, StepId = "S7", State = FlightState.Departure });
            context.Relations.Add(new Relation() { StationId = 7, StepId = "S7", State = FlightState.Departure });
            context.Relations.Add(new Relation() { StationId = 8, StepId = "S4", State = FlightState.Departure });

            context.SaveChanges();
        }
    }
}