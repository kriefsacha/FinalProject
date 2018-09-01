using System.ComponentModel;

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
                _number = value;
                Notify(nameof(Number));
            }
        }

        private string _planeName;

        public string Plane
        {
            get { return _planeName; }
            set
            {
                _planeName = value;
                Notify(nameof(Plane));
            }
        }

        public void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
