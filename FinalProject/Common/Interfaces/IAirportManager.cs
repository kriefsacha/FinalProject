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
        string DepartureMovement(Plane plane);
        string ArrivalMovement(Plane plane);
        List<Plane> GetFutureArrivals();
        List<Plane> GetFutureDepartures();
        List<Station> GetCurrentStationsState();
    }
}
