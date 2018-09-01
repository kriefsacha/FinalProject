using Common;
using System;

namespace Server.Interfaces
{
    public interface IHubService
    {
        void Moved(Plane plane);
        void DepartureOrArrival(Plane plane);
        void OnError(Exception exc);
    }
}
