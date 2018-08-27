using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IManager
    {
        void DepartureOrArrival(Plane plane);
        List<Plane> GetFutureDeparturesAndArrivals();
        List<Station> GetCurrentStationsState();
        void AddStation(Station station);
    }
}
