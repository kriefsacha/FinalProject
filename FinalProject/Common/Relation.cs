using Common.Enums;

namespace Common
{
    /// <summary>
    /// A relation between a station number and her next step
    /// </summary>
    public class Relation
    {
        public Relation(int stationId, string NextStepId, FlightState state)
        {
            StationId = stationId;
            this.NextStepId = NextStepId;
            State = state;
        }

        public int StationId { get; set; }

        public string NextStepId { get; set; }

        public FlightState State { get; set; }
    }
}
