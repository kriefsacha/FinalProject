using Common.Enums;
using System.Data.Entity;

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

            context.Relations.Add(new Relation() { StationId = 0, NextStepId = "S1", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 1, NextStepId = "S2", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 2, NextStepId = "S3", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 3, NextStepId = "S4", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 4, NextStepId = "S5", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 5, NextStepId = "S6", State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 6, NextStepId = null, State = FlightState.Arrival });
            context.Relations.Add(new Relation() { StationId = 7, NextStepId = null, State = FlightState.Arrival });

            context.Relations.Add(new Relation() { StationId = 0, NextStepId = "S6", State = FlightState.Departure });
            context.Relations.Add(new Relation() { StationId = 4, NextStepId = null, State = FlightState.Departure });
            context.Relations.Add(new Relation() { StationId = 6, NextStepId = "S7", State = FlightState.Departure });
            context.Relations.Add(new Relation() { StationId = 7, NextStepId = "S7", State = FlightState.Departure });
            context.Relations.Add(new Relation() { StationId = 8, NextStepId = "S4", State = FlightState.Departure });

            context.SaveChanges();
        }
    }
}