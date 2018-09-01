using Common.Enums;
using System;

namespace Common
{
    public class Plane
    {
        public Plane(string Name, DateTime ActionDate, int waitingTime, FlightState flightState)
        {
            this.Name = Name;
            this.ActionDate = ActionDate;
            this.waitingTime = waitingTime;
            this.flightState = flightState;
        }

        public string Name { get; set; }

        public int StationNumber { get; set; }

        public int PreviousStationNumber { get; set; }

        /// <summary>
        /// Time that the plane waits in every station
        /// </summary>
        public int waitingTime { get; set; }

        /// <summary>
        /// Date that the plane will arrive/departure
        /// </summary>
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// Arrive/Departure State
        /// </summary>
        public FlightState flightState { get; set; }

        /// <summary>
        /// Set the new station of the plane
        /// </summary>
        /// <param name="stationId">Number of the station</param>
        public void SetStation(int stationId)
        {
            PreviousStationNumber = StationNumber;
            StationNumber = stationId;
            Moved?.Invoke(stationId, EventArgs.Empty);
        }

        /// <summary>
        /// When the plane finished all his way
        /// </summary>
        public void FinishWay()
        {
            SetStation(0);
        }

        public delegate void MovedHandler(int senderId, EventArgs e);

        /// <summary>
        /// Fires when SetStation is called in the new station for the previous station to be released and can continue to work
        /// </summary>
        public event MovedHandler Moved;
    }
}