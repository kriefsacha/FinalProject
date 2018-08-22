using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Plane
    {
        public string ID { get; set; }

        private int _stationNumber;

        public int StationNumber
        {
            get { return _stationNumber; }
            set {
                if(value > 0 && value < 9)
                _stationNumber = value;
            }
        }

        public int waitingTime { get; set; }

        public DateTime ActionTime { get; set; }

        public FlightState flightState { get; set; }

        public Tuple<int,int> nextStations { get; set; }

    }
}
