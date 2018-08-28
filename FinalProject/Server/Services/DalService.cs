using Common;
using Common.Interfaces;
using Server.Interfaces;
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

       public void DepartureOrArrival(Plane plane)
        {
            flightRepository.DepartureOrArrival(plane);
        }

        public List<Plane> GetFutureDeparturesAndArrivals()
        {
            return flightRepository.GetFutureDeparturesAndArrivals();
        }

        public List<Station> GetCurrentStationsState()
        {
            return stationRepository.GetCurrentStationsState();
        }
    }
}