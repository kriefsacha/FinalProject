using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IHubService
    {
        void Moved(Plane plane);
        void DepartureOrArrival(Plane plane);
    }
}
