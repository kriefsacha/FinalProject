using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Logic : ILogic
    {
        IFlightRepository flightRepository;
        IPlaneRepository planeRepository;
        IStationRepository stationRepository;

        public Logic(IFlightRepository flightRepository ,
            IPlaneRepository planeRepository ,
            IStationRepository stationRepository)
        {
            this.flightRepository = flightRepository;
            this.planeRepository = planeRepository;
            this.stationRepository = stationRepository;
        }

        public void Arrival(Plane Plane)
        {
            flightRepository.Arrival(Plane);
        }

        public void Departure(Plane Plane)
        {
            flightRepository.Departure(Plane);
        }

        public void Init()
        {
            
        }

        public void MoveRequest(Plane Plane)
        {
           
        }
    }
}
