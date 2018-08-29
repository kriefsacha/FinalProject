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
        public string ID { get; set; }

        private int _stationNumber;
        private int _previousstationNumber;

        public event PropertyChangedEventHandler PropertyChanged;

        public int PreviousStationNumber
        {
            get { return _previousstationNumber; }
            set
            {
                if (value >= 0 && value < 9)
                    _previousstationNumber = value;
                Notify(nameof(PreviousStationNumber));
            }

        }

        public int StationNumber
        {
            get { return _stationNumber; }
            set
            {
                if (value >= 0 && value < 9)
                    _stationNumber = value;
                Notify(nameof(StationNumber));
            }
        }

       
            public void Notify(string propName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        Random r = new Random();

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
        // now = 0 going out
        // per = 0 not been

    }
}
