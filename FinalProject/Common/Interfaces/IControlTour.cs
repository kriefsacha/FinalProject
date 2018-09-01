using System;

namespace Common.Interfaces
{
    public interface IControlTour
    {
        void NewDepartureOrArrival(Plane Plane);
        string GetNextMove(Plane plane);
        event EventHandler onPlaneMoved;
        event EventHandler onError;
    }
}
