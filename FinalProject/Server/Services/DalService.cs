using Common;
using Common.Interfaces;
using Server.Interfaces;
using System.Collections.Generic;

namespace Server.Services
{
    internal class DalService : IDalService
    {
        private IFlightRepository flightRepository;
        private IPlaneRepository planeRepository;
        private IStationRepository stationRepository;

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