using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IFlightRepository
    {
        void DepartureOrArrival(Plane plane);
        List<Plane> GetFutureDeparturesAndArrivals();
    }
}
