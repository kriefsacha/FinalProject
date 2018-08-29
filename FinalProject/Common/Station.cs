namespace Common
{
    public class Station
    {
        public Station(int number , string stepkey)
        {
            Number = number;
            stepKey = stepkey;
        }

        public int Number { get; set; }

        public Plane Plane { get; set; }

        public bool IsAvailable
        {
            get { return Plane == null; }
        }

        private string _stepKey;

        public string stepKey
        {
            get
            {
                return _stepKey;
            }
            set
            {
                if (value.StartsWith("S"))
                {
                    _stepKey = value;
                }
            }
        }
    }
}
