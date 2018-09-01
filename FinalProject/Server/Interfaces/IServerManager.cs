using Common;
using System.Collections.Generic;

namespace Server.Interfaces
{
    public interface IServerManager
    {
        void DepartureOrArrival(Plane plane);
        List<Plane> GetFutureDeparturesAndArrivals();
        List<Station> GetCurrentStationsState();
    }
}
