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
                //if(value > 0 && value < 9)
                _stationNumber = value;
            }
        }

        private int _previousstationNumber;

        public int PreviousStationNumber
        {
            get { return _previousstationNumber; }
            set
            {
                //if(value > 0 && value < 9)
                _previousstationNumber = value;
            }
        }

        public int waitingTime { get; set; }

        public DateTime ActionTime { get; set; }

        public FlightState flightState { get; set; }

        public void SetStation(int id)
        {
            PreviousStationNumber = StationNumber;
            StationNumber = id;
            Moved?.Invoke(id, EventArgs.Empty);
        }

        public void FinishWay()
        {
            SetStation(0);
        }
        public delegate void MovedHandler (int senderId, EventArgs e);

        public event MovedHandler Moved;

    }
}
