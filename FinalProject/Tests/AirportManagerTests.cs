using System;
using System.Linq;
using System.Threading.Tasks;
using BL;
using BL.Storage;
using Common;
using Common.Enums;
using Xunit;

namespace Tests
{
    public class AirportManagerTests
    {
        [Theory]
        [InlineData(0, "S6")]
        [InlineData(4, null)]
        [InlineData(6, "S7")]
        [InlineData(7, "S7")]
        [InlineData(8, "S4")]
        public void DepartureMovementTest(int stationNumber, string result)
        {
            var plane = new Plane("FR123", DateTime.Now, 19, FlightState.Departure) { StationNumber = stationNumber };

            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new AirportManager(new QueueService(), stationmock.Object);

            var res = airport.GetNextMove(plane);

            Assert.Equal(res, result);
        }

        [Theory]
        [InlineData(0, "S1")]
        [InlineData(1, "S2")]
        [InlineData(2, "S3")]
        [InlineData(3, "S4")]
        [InlineData(4, "S5")]
        [InlineData(5, "S6")]
        [InlineData(6, null)]
        [InlineData(7, null)]
        public void ArrivalMovementTest(int stationNumber, string result)
        {
            var plane = new Plane("FR123", DateTime.Now, 19, FlightState.Arrival) { StationNumber = stationNumber };

            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new AirportManager(new QueueService(), stationmock.Object);

            var res = airport.GetNextMove(plane);

            Assert.Equal(res, result);
        }

        [Fact]
        public void NewDepartureOrArrival_ArrivalTest()
        {
            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new AirportManager(new QueueService(), stationmock.Object);

            var plane = new Plane("FR123", DateTime.Now.AddSeconds(10), 6000000, FlightState.Arrival);

            airport.NewDepartureOrArrival(plane);

            Task.Delay(15000).Wait();

            Assert.NotNull(airport.Stations.Where(s => s.plane == plane).FirstOrDefault());
        }

        [Fact]
        public void NewDepartureOrArrival_DepartureTest()
        {
            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new AirportManager(new QueueService(), stationmock.Object);

            var plane = new Plane("HE123", DateTime.Now.AddSeconds(10), 6000000, FlightState.Departure);

            airport.NewDepartureOrArrival(plane);

            Task.Delay(15000).Wait();

            //Assert.NotNull(airport.Stations.Where(s => s.plane == plane).FirstOrDefault());
            Assert.True(airport.Stations[5].plane != null || airport.Stations[6].plane != null);
        }
    }
}
