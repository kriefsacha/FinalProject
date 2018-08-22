using Common;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class AirportManager
    {
        public List<StationManager> Stations { get; set; }
        public List<Plane> waitingList { get; set; }

        public AirportManager()
        {
            for (int i = 0; i < 8; i++)
            {
                Stations.Add(new StationManager() { StationNumber = i + 1, isAvailable = true });
            }

            waitingList = new List<Plane>();
        }

        public void Move(Plane plane)
        {
            if (plane.flightState == FlightState.Departure)
            {
                var nextStationNumbers = DepartureMovement(plane);
                if (nextStationNumbers == null) return;
                if (nextStationNumbers.Item2 == 0)
                {
                    var nextStation = Stations.Where(s => s.StationNumber == nextStationNumbers.Item1).FirstOrDefault();
                    if (nextStation != null)
                    {
                        if (nextStation.isAvailable) nextStation.Do(plane);
                        else
                        {
                            plane.nextStations = nextStationNumbers;
                            waitingList.Add(plane);
                            nextStation.finishedEvent += NextStation_finishedEvent;
                        }
                    }
                    else return;
                }
            }

            else
            {
                var nextStationNumbers = ArrivalMovement(plane);
                if (nextStationNumbers == null) return;
                if (nextStationNumbers.Item2 == 0)
                {
                    var nextStation = Stations.Where(s => s.StationNumber == nextStationNumbers.Item1).FirstOrDefault();
                    if (nextStation != null)
                    {
                        if (nextStation.isAvailable) nextStation.Do(plane);
                        else
                        {
                            plane.nextStations = nextStationNumbers;
                            waitingList.Add(plane);
                            nextStation.finishedEvent += NextStation_finishedEvent;
                        }
                    }
                    else return;
                }
            }
        }

        private void NextStation_finishedEvent(StationManager sender, EventArgs e)
        {
            var waitingPlane = waitingList.FirstOrDefault(plane => plane.nextStations.Item1 == sender.StationNumber
             || plane.nextStations.Item2 == sender.StationNumber);

            if(waitingPlane != null)
            {
                waitingList.Remove(waitingPlane);
                sender.Do(waitingPlane);
            }
        }

        public Tuple<int, int> DepartureMovement(Plane plane)
        {
            switch (plane.StationNumber)
            {
                case 0: return new Tuple<int, int>(6, 7);
                case 4: return new Tuple<int, int>(0, 0);
                case 6: return new Tuple<int, int>(8, 0);
                case 7: return new Tuple<int, int>(8, 0);
                case 8: return new Tuple<int, int>(4, 0);
                default: return null;
            }
        }

        public Tuple<int, int> ArrivalMovement(Plane plane)
        {
            switch (plane.StationNumber)
            {
                case 0: return new Tuple<int, int>(1, 0);
                case 1: return new Tuple<int, int>(2, 0);
                case 2: return new Tuple<int, int>(3, 0);
                case 3: return new Tuple<int, int>(4, 0);
                case 4: return new Tuple<int, int>(5, 0);
                case 5: return new Tuple<int, int>(6, 7);
                case 6: return new Tuple<int, int>(0, 0);
                case 7: return new Tuple<int, int>(0, 0);
                default: return null;
            }
        }

    }
}
