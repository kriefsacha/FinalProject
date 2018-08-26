using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Services
{
    public class DalService : IDalService
    {
        IFlightRepository flightRepository;
        IPlaneRepository planeRepository;
        IStationRepository stationRepository;

        public DalService(IFlightRepository flightRepository,
            IPlaneRepository planeRepository,
            IStationRepository stationRepository)
        {
            this.flightRepository = flightRepository;
            this.planeRepository = planeRepository;
            this.stationRepository = stationRepository;
        }

        public void Moved(Plane plane)
        {
            planeRepository.MoveRequest(plane);
        }

        public void Arrival(Plane Plane)
        {
            flightRepository.Arrival(Plane);
        }

        public void Departure(Plane Plane)
        {
            flightRepository.Departure(Plane);
        }

        public List<Plane> GetFutureArrivals()
        {
            return flightRepository.GetFutureArrivals();
        }

        public List<Plane> GetFutureDepartures()
        {
            return flightRepository.GetFutureDepartures();
        }

        public List<Station> GetCurrentStationsState()
        {
            return stationRepository.GetCurrentStationsState();
        }
    }
}