using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Station : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Station(int number)
        {
            Number = number;
        }

        private int _number;

        public int Number
        {
            get { return _number; }
            set
            {
                Notify(nameof(Number));
                _number = value;
            }
        }

        private Plane plane;

        public Plane Plane
        {
            get { return plane; }
            set
            {
                Notify(nameof(Plane));
                plane = value;
            }
        }

        //public bool IsAvailable
        //{
        //    get { return Plane == null; }
        //}

        public void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
