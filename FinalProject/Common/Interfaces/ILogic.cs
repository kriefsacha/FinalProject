using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ILogic
    {
        void Init();

        void Departure(Plane Plane);

        void Arrival(Plane Plane);

        void MoveRequest(Plane Plane);
    }
}
