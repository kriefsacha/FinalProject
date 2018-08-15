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

    }
}
