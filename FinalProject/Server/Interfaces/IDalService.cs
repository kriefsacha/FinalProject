using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
