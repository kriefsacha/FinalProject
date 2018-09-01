using Common.Enums;
using System;
using System.ComponentModel;

namespace Client.Models
{
    public class Plane : INotifyPropertyChanged
    {
        Random r = new Random();

        public Plane(string Name, DateTime ActionDate, int waitingTime, FlightState flightState)
        {
            this.Name = Name;
            this.ActionDate = ActionDate;
            this.waitingTime = waitingTime;
            this.flightState = flightState;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify(nameof(Name));
            }
        }

        public int waitingTime { get; set; }

        public DateTime ActionDate { get; set; }

        public FlightState flightState { get; set; }

        private int _stationNumber;

        public int StationNumber
        {
            get { return _stationNumber; }
            set
            {
                _stationNumber = value;
                Notify(nameof(StationNumber));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private int _randomPicture;

        public int RandomPicture
        {
            get
            {
                int currentNumber = r.Next(1, 4);
                _randomPicture = currentNumber;
                return _randomPicture;
            }
        }
    }
}
