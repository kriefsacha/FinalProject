using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Plane : INotifyPropertyChanged
    {

        public Plane(string Name, DateTime ActionTime, int waitingTime, FlightState flightState)
        {
            this.Name = Name;
            this.ActionTime = ActionTime;
            this.waitingTime = waitingTime;
            this.flightState = flightState;
        }

        public string Name { get; set; }

        public int waitingTime { get; set; }

        public DateTime ActionTime { get; set; }

        public FlightState flightState { get; set; }

        private int _stationNumber;
        Random r = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

        public int StationNumber
        {
            get { return _stationNumber; }
            set
            {
                //if (value >= 0 && value < 9)
                    _stationNumber = value;
                Notify(nameof(StationNumber));
            }
        }

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
