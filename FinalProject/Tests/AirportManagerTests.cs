﻿using System;
using System.Linq;
using System.Threading.Tasks;
using BL;
using BL.Interfaces;
using BL.Storage;
using Common;
using Common.Enums;
using Common.Interfaces;
using DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace Tests
{
    public class AirportManagerTests
    {
        [Theory]
        [InlineData(0,"S6")]
        [InlineData(4,null)]
        [InlineData(6, "S7")]
        [InlineData(7, "S7")]
        [InlineData(8, "S4")]
        public void DepartureMovementTest(int stationNumber , string result)
        {
            var plane = new Plane() { StationNumber = stationNumber , flightState = FlightState.Departure};

            var queuemock = new Mock<IQueueService>();
            var stationmock = new Mock<IStationRepository>();
            //var airport = new AirportManager(new QueueService());

            var airport = new AirportManager(new QueueService(), new StationRepository());

            var res = airport.GetNextMove(plane);
            //var mock = new Mock<IAirportManager>();
            //mock.Setup(x => x.DepartureMovement(plane)).Returns("S6");
            //var res = mock.Object.DepartureMovement(plane);
            //Xunit.Assert.Equal(res, "S6");
            Xunit.Assert.Equal(res, result);
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
            var plane = new Plane() { StationNumber = stationNumber , flightState = FlightState.Arrival };

            var queuemock = new Mock<IQueueService>();
            var stationmock = new Mock<IStationRepository>();
            //var airport = new AirportManager(new QueueService());

            var airport = new AirportManager(queuemock.Object, stationmock.Object);

            var res = airport.GetNextMove(plane);

            Xunit.Assert.Equal(res, result);
        }

        [Fact]
        public void NewDepartureOrArrival_ArrivalTest()
        {
            var stationmock = new Mock<IStationRepository>();
            //var airport = new AirportManager(new QueueService());

            var airport = new AirportManager(new QueueService(), stationmock.Object);

            var plane = new Plane()
            {
                flightState = FlightState.Arrival,
                ActionTime = DateTime.Now.AddSeconds(4),
                waitingTime = 600000
            };

            airport.NewDepartureOrArrival(plane);

            Task.Delay(30000).Wait();

            Xunit.Assert.Equal(airport.Stations[0].plane, plane);
        }

        [Fact]
        public void NewDepartureOrArrival_DepartureTest()
        {
            var stationmock = new Mock<IStationRepository>();
            //var airport = new AirportManager(new QueueService());

            var airport = new AirportManager(new QueueService(), stationmock.Object);

            var plane = new Plane()
            {
                flightState = FlightState.Departure,
                ActionTime = DateTime.Now.AddSeconds(4),
                waitingTime = 600000
            };

            airport.NewDepartureOrArrival(plane);

            Task.Delay(30000).Wait();

            Xunit.Assert.True(airport.Stations[5].plane == plane || airport.Stations[6].plane == plane);
        }

        //[Fact]
        //public void T()
        //{
        //    var mock1 = new Mock<IFlightRepository>();
        //    var mock2 = new Mock<IPlaneRepository>();
        //    var mock3 = new Mock<IStationRepository>();

        //    var airport = new AirportManager(mock1.Object, mock2.Object, mock3.Object, new QueueService());

        //    var plane = new Plane()
        //    {
        //        flightState = FlightState.Arrival,
        //        ActionTime = DateTime.Now.AddSeconds(5),
        //        waitingTime = 2000
        //    };

        //    airport.NewDepartureOrArrival(plane);

        //    while (true)
        //    {
        //        Task.Delay(2000).Wait();
        //    }
        //}
    }
}