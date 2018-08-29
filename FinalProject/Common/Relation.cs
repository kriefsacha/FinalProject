using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Relation
    {
        public Relation(int stationId, string stepId, FlightState state)
        {
            StationId = stationId;
            StepId = stepId;
            State = state;
        }

        public int StationId { get; set; }

        public string StepId { get; set; }

        public FlightState State { get; set; }
    }
}
