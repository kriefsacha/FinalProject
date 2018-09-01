namespace Common
{
    public class Station
    {
        public Station(int number, string StepKey)
        {
            Number = number;
            this.StepKey = StepKey;
        }

        public int Number { get; set; }

        public Plane Plane { get; set; }

        public bool IsAvailable
        {
            get { return Plane == null; }
        }

        private string _stepKey;

        public string StepKey
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
