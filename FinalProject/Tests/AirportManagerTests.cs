using System;
using BL;
using BL.Storage;
using Common;
using Common.Enums;
using Xunit;

namespace Tests
{
    public class AirportManagerTests
    {

        //Xunit is a nuget package that will run all tests at the same time (quicker) ,
        //and help us like in this test insted of doing copy paste of the same thing,
        //just send him new parameters and he will count it like each one is a new test

        [Theory]
        [InlineData(0, "S6")]
        [InlineData(4, null)]
        [InlineData(6, "S7")]
        [InlineData(7, "S7")]
        [InlineData(8, "S4")]
        public void DepartureMovementTest(int stationNumber, string expectedResult)
        {
            var plane = new Plane("FR123", DateTime.Now, 19, FlightState.Departure) { StationNumber = stationNumber };

            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new ControlTour(new QueueService(), stationmock.Object);

            var actualResult = airport.GetNextMove(plane);

            Assert.Equal(actualResult, expectedResult);
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
        public void ArrivalMovementTest(int stationNumber, string expectedResult)
        {
            var plane = new Plane("FR123", DateTime.Now, 19, FlightState.Arrival) { StationNumber = stationNumber };

            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new ControlTour(new QueueService(), stationmock.Object);

            var actualResult = airport.GetNextMove(plane);

            Assert.Equal(actualResult, expectedResult);
        }

        //Fact is saying that there is just one possibility , no like the other tests up
        [Fact]
        public void NewDepartureOrArrival_ArrivalTest()
        {
            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new ControlTour(new QueueService(), stationmock.Object);

            var plane = new Plane("FR123", DateTime.Now.AddSeconds(1), 6000000, FlightState.Arrival);

            airport.NewDepartureOrArrival(plane);

            Assert.Contains(plane, airport.waitingList);
        }

        [Fact]
        public void NewDepartureOrArrival_DepartureTest()
        {
            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new ControlTour(new QueueService(), stationmock.Object);

            var plane = new Plane("HE123", DateTime.Now.AddSeconds(1), 6000000, FlightState.Departure);

            airport.NewDepartureOrArrival(plane);

            Assert.Contains(plane, airport.waitingList);
        }

        [Fact]
        public void WaitingTimerTickFunctionTest()
        {
            var stationmock = new MockService().GetStationRepositoryMock();

            var airport = new ControlTour(new QueueService(), stationmock.Object);

            var plane = new Plane("HE123", DateTime.Now, 6000, FlightState.Departure);

            airport.waitingList.Add(plane);

            //null because we don't need it
            airport.WaitingTimer_Elapsed(null, null);

            Assert.DoesNotContain(plane, airport.waitingList);
        }
    }
}
