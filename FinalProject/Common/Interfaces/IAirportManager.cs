using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IAirportManager
    {
        void NewDepartureOrArrival(Plane Plane);
        string GetNextMove(Plane plane);
        event EventHandler onPlaneMoved;
        event EventHandler onError;
    }
}
