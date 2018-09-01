using Common;
using System.Collections.Generic;

namespace Server.Interfaces
{
    public interface IDalService
    {
        void Moved(Plane plane);
        void DepartureOrArrival(Plane plane);
        List<Plane> GetFutureDeparturesAndArrivals();
        List<Station> GetCurrentStationsState();
    }
}
