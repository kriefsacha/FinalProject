using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IManager
    {
        void Arrival(Plane Plane);
        void Departure(Plane Plane);
        List<Plane> GetFutureArrivals();
        List<Plane> GetFutureDepartures();
        List<Station> GetCurrentStationsState();
    }
}
