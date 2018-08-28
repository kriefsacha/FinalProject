using Common;
using Common.Enums;
using Common.Interfaces;
using Moq;
using System.Collections.Generic;

namespace Tests
{
    public class MockService
    {
        internal Mock<IStationRepository> GetStationRepositoryMock()
        {
            var stationmock = new Mock<IStationRepository>();
            stationmock.Setup(s => s.GetRelations()).Returns(new List<Relation>()
            {
                new Relation(0,"S1",FlightState.Arrival) ,
                new Relation(1,"S2",FlightState.Arrival),
                new Relation(2,"S3",FlightState.Arrival),
                new Relation(3,"S4",FlightState.Arrival),
                new Relation(4,"S5",FlightState.Arrival),
                new Relation(5,"S6",FlightState.Arrival),
                new Relation(6,null,FlightState.Arrival),
                new Relation(7,null,FlightState.Arrival),
                new Relation(0,"S6",FlightState.Departure),
                new Relation(4,null,FlightState.Departure),
                new Relation(6,"S7",FlightState.Departure),
                new Relation(7,"S7",FlightState.Departure),
                new Relation(8,"S4",FlightState.Departure)
            });

            stationmock.Setup(s => s.GetCurrentStationsState()).Returns(new List<Station>()
            {
                new Station(1,"S1"),
                new Station(2,"S2"),
                new Station(3,"S3"),
                new Station(4,"S4"),
                new Station(5,"S5"),
                new Station(6,"S6"),
                new Station(7,"S6"),
                new Station(8,"S7"),
        });
            return stationmock;
        }
    }
}
