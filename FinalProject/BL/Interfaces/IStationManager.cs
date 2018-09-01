using Common;
using System;

namespace BL.Interfaces
{
    interface IStationManager
    {
        void Do(Plane plane);
        event EventHandler TookNewPlane;
        event EventHandler PlaneFinished;
    }
}
